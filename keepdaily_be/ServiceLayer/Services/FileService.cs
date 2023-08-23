using Microsoft.AspNetCore.Http;

namespace ServiceLayer.Services
{
    public class FileService
    {
        private static string dir = null!;

        public FileService(string folder)
        {
            dir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, folder);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
        }

        public async Task Create(IFormFile file, string fileName)
        {
            var filePath = Path.Combine(dir, fileName);            
            using (var stream = File.Create(filePath))
            {
                await file.CopyToAsync(stream);
            }
        }

        public string GetFilePath(string fileName)
        {
            var filepath = Path.Combine(dir, fileName);
            if (!File.Exists(filepath)) throw new FileNotFoundException();
            return filepath;
        }

        public void Delete(string fileName)
        {
            var filePath = Path.Combine(dir, fileName);
            File.Delete(filePath);
        }
    }
}
