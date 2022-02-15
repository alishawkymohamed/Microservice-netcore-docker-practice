using Catalog.API.Enums;
using Catalog.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Catalog.API.Services.Implementation
{
    public class ImageService : IImageService
    {
        public ImageFormatEnum GetImageFormat(byte[] bytes)
        {
            var bmp = Encoding.ASCII.GetBytes("BM");                // BMP
            var gif = Encoding.ASCII.GetBytes("GIF");               // GIF
            var png = new byte[] { 137, 80, 78, 71 };               // PNG
            var tiff = new byte[] { 73, 73, 42 };                   // TIFF
            var tiff2 = new byte[] { 77, 77, 42 };                  // TIFF
            var jpeg = new byte[] { 255, 216, 255, 224 };           // jpeg
            var jpeg2 = new byte[] { 255, 216, 255, 225 };          // jpeg canon

            if (bmp.SequenceEqual(bytes.Take(bmp.Length)))
                return ImageFormatEnum.bmp;

            if (gif.SequenceEqual(bytes.Take(gif.Length)))
                return ImageFormatEnum.gif;

            if (png.SequenceEqual(bytes.Take(png.Length)))
                return ImageFormatEnum.png;

            if (tiff.SequenceEqual(bytes.Take(tiff.Length)))
                return ImageFormatEnum.tiff;

            if (tiff2.SequenceEqual(bytes.Take(tiff2.Length)))
                return ImageFormatEnum.tiff;

            if (jpeg.SequenceEqual(bytes.Take(jpeg.Length)))
                return ImageFormatEnum.jpeg;

            if (jpeg2.SequenceEqual(bytes.Take(jpeg2.Length)))
                return ImageFormatEnum.jpeg;

            return ImageFormatEnum.unknown;
        }

        public bool CheckIfImageFile(IFormFile file)
        {
            byte[] fileBytes;
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }

            return this.GetImageFormat(fileBytes) != ImageFormatEnum.unknown;
        }
    }
}