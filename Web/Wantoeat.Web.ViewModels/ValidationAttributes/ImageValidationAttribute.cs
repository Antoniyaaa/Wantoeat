namespace Wantoeat.Web.ViewModels.ValidationAttributes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.IO;

    using Microsoft.AspNetCore.Http;

    public class ImageValidationAttribute : ValidationAttribute
    {
        private static readonly List<string> AllowedExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".bmp" };

        public override bool IsValid(object value)
        {
            if (value != null)
            {
                IFormFile file = value as IFormFile;

                var fileInfo = new FileInfo(file.FileName);

                if (!AllowedExtensions.Contains(fileInfo.Extension))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
