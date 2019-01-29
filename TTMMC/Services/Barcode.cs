using Microsoft.AspNetCore.Hosting;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using TTMMC.Utils;
using TyKonKet.BarcodeGenerator;

namespace TTMMC.Services
{
    public class Barcode
    {
        private readonly TyKonKet.BarcodeGenerator.Barcode _ean8;
        private readonly TyKonKet.BarcodeGenerator.Barcode _ean13;
        private readonly IHostingEnvironment _hostingEnvironment;

        public Barcode(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _ean8 = new TyKonKet.BarcodeGenerator.Barcode(o =>
            {
                o.Encode = Encodes.Ean8;
                o.Height = 30;
                o.Scale = 5;
                o.BackgroundColor = Rgba32.Transparent;
                o.Font = "Arial";
            });
            _ean13 = new TyKonKet.BarcodeGenerator.Barcode(o =>
            {
                o.Encode = Encodes.Ean13;
                o.Height = 50;
                o.Scale = 5;
                o.BackgroundColor = Rgba32.Transparent;
                o.Font = "Arial";
            });
        }

        public string GenerateEan8(string barcode)
        {
            return _ean8.Encode(barcode, $"{_hostingEnvironment.WebRootPath}/barcodes/{barcode}.png");
        }
        public string GenerateEan13(string barcode)
        {
            return _ean13.Encode(barcode, $"{_hostingEnvironment.WebRootPath}/barcodes/{barcode}.png");
        }

        public string CreateNewEan8(List<string> exclude = null)
        {
            exclude = exclude ?? new List<string>();
            var rnd = new Random();
            string barcode;
            while (true)
            {
                barcode = "";
                for (var i = 0; i < 7; i++) barcode += rnd.Next(10).ToString();
                barcode += EanValidate(barcode);
                if (!exclude.Contains(barcode)) break;
            }

            return barcode;
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
