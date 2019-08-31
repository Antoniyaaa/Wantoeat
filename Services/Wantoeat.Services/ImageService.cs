namespace Wantoeat.Services
{
    using System;
    using System.IO;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;

    public class ImageService : IImageService
    {
        private readonly IHostingEnvironment hostingEnvironment;

        public ImageService(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        public string UploadImage(IFormFile imageFile, string entityName)
        {
            var fileInfo = new FileInfo(imageFile.FileName);

            var newFileName = entityName + "_" + string.Format("{0:d}",
                              (DateTime.Now.Ticks / 10) % 100000000) + fileInfo.Extension;

            var webPath = this.hostingEnvironment.WebRootPath;
            string imagePath = @"/images/" + newFileName;
            var fullPath = Path.Combine(webPath + imagePath);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }

            return imagePath;
        }

        public string ReplaceImage(IFormFile imageFile, string filePath, string entityName)
        {
            var webPath = this.hostingEnvironment.WebRootPath;
            var fullPath = Path.Combine(webPath + filePath);
            File.Delete(fullPath);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }

            return filePath;
        }
    }
}
