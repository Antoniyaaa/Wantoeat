namespace Wantoeat.Services
{
    using Microsoft.AspNetCore.Http;

    public interface IImageService
    {
        string UploadImage(IFormFile imageFile, string entityName);

        string ReplaceImage(IFormFile imageFile, string filePath, string entityName);
    }
}
