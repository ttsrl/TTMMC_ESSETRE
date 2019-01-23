using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using TTMMC.Utils;

namespace TTMMC.Services
{
    public class Utilities
    {
        private IConfiguration configuration;

        public Utilities(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public bool ExistConfiguration(string key)
        {
            return configuration.GetSection(key).Exists();
        }

        public T GetConfiguration<T>(string settingName)
        {
            if (ExistConfiguration(settingName))
            {
                object value = configuration.GetValue<T>(settingName);
                return (T)Convert.ChangeType(value, typeof(T));
            }
            return (T)Convert.ChangeType(null, typeof(T));
        }

        public List<T> GetConfigurationElementsList<T>(string node)
        {
            if (ExistConfiguration(node))
            {
                var v = configuration.GetSection(node).Get<List<T>>();
                return (List<T>)Convert.ChangeType(v, typeof(List<T>));
            }
            return new List<T>();
        }

        public string CreateNewEan13(List<string> exclude = null)
        {
            exclude = exclude ?? new List<string>();
            var rnd = new Random();
            string barcode;
            while (true)
            {
                barcode = "";
                for (var i = 0; i < 12; i++) barcode += rnd.Next(10).ToString();
                barcode += EanValidate(barcode);
                if (!exclude.Contains(barcode)) break;
            }
            return barcode;
        }

        private static string EanValidate(string barcode)
        {
            var sum = 0;
            for (var i = 7; i > 0; i--)
            {
                var tmp = (i % 2 * 2 + 1) * barcode[i - 1].ToInt();
                sum += tmp;
            }

            return ((10 - sum % 10) % 10).ToString();
        }

        public Color ColorFromHex(string hex)
        {
            FromHex(hex, out var a, out var r, out var g, out var b);
            return Color.FromArgb(a, r, g, b);
        }

        private void FromHex(string hex, out byte a, out byte r, out byte g, out byte b)
        {
            hex = ToRgbaHex(hex);
            if (hex == null || !uint.TryParse(hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture,
                    out var packedValue))
                throw new ArgumentException("Hexadecimal string is not in the correct format.", nameof(hex));

            a = (byte)(packedValue >> 0);
            r = (byte)(packedValue >> 24);
            g = (byte)(packedValue >> 16);
            b = (byte)(packedValue >> 8);
        }

        private static string ToRgbaHex(string hex)
        {
            hex = hex.StartsWith("#") ? hex.Substring(1) : hex;
            switch (hex.Length)
            {
                case 8:
                    return hex;
                case 6:
                    return hex + "FF";
            }

            if (hex.Length < 3 || hex.Length > 4) return null;

            var red = char.ToString(hex[0]);
            var green = char.ToString(hex[1]);
            var blue = char.ToString(hex[2]);
            var alpha = hex.Length == 3 ? "F" : char.ToString(hex[3]);
            return string.Concat(red, red, green, green, blue, blue, alpha, alpha);
        }


    }
}
