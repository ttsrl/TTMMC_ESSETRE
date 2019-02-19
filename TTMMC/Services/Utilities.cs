﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using TTMMC_ESSETRE.Utils;

namespace TTMMC_ESSETRE.Services
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

        public string ValueToString(Type type, string val, int numberDecimal = 2)
        {
            if (type != typeof(string))
            {
                if ((val.Contains(",")))
                {
                    return val.Substring(0, val.IndexOf(",") + numberDecimal + 1);
                }
            }
            return val;
        }
    }
}
