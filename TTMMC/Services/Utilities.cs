using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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


    }
}
