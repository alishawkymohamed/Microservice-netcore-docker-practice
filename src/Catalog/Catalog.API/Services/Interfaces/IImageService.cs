using Catalog.API.Enums;
using Microsoft.AspNetCore.Http;

namespace Catalog.API.Services.Interfaces
{
    public interface IImageService
    {
        ImageFormatEnum GetImageFormat(byte[] bytes);
        bool CheckIfImageFile(IFormFile file);
    }
}
