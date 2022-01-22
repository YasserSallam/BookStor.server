using BookStore.Infrastructure.Services.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BookStore.Infrastructure.Services.Handlers
{
    public class FileService : IFileService
    {
        public string Save(IFormFile file)
        {
            var folderName = Path.Combine("wwwroot","Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            var fileName =  string.Concat( Guid.NewGuid(),"_", file.FileName);
            var fullPath = Path.Combine(pathToSave, fileName);
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return fileName;
        }
    }
}
