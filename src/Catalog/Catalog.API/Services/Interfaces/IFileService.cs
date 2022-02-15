using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Catalog.API.Services.Interfaces
{
    public interface IFileService
    {
        Task<string> WriteFile(IFormFile file);
    }
}
