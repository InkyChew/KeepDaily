using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using Microsoft.AspNetCore.Http;

namespace ServiceLayer.Services
{
    public class GoogleDriveService
    {
        private readonly DriveService _service;
        private readonly GoogleCredential _credential;
        private readonly string folderId = "1uZemvBye_CIw-5yEUEqvg9nkqW398KsN";

        public GoogleDriveService()
        {
            var keyPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "credential.json");
            if (keyPath == null) throw new ArgumentNullException("Google drive api key not exist.");
            using (FileStream fs = new(keyPath, FileMode.Open, FileAccess.Read))
            {
                _credential = GoogleCredential.FromStream(fs).CreateScoped(new[] { DriveService.Scope.Drive });
            }
            _service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = _credential,
                ApplicationName = "KeepDaily"
            });
        }

        public void GetFile(string fileName)
        {
            // Define the query parameters to filter the results.
            string query = $"'{folderId}' in parents and name = '{fileName}' and trashed = false";

            // Request the list of files matching the query.
            var request = _service.Files.List();
            request.Q = query;
            var files = request.Execute().Files;

            // Check if the file was found.
            if (files != null && files.Count > 0)
            {
                // Access the first file found.
                var file = files[0];
                Console.WriteLine($"File found: {file.Name}, ID: {file.Id}");
                Console.WriteLine(file.WebViewLink);
            }
            else
            {
                Console.WriteLine("File not found.");
            }
        }

        public async Task<Google.Apis.Drive.v3.Data.File> UploadFileAsync(IFormFile file, string fileName)
        {
            using var uploadStream = file.OpenReadStream();

            // Create the File resource to upload.
            Google.Apis.Drive.v3.Data.File driveFile = new()
            {
                Name = fileName,
                Parents = new[] { folderId }
            };
            // Get the media upload request object.
            var insertRequest = _service.Files.Create(driveFile, uploadStream, file.ContentType);

            var tcs = new TaskCompletionSource<Google.Apis.Drive.v3.Data.File>();
            insertRequest.ProgressChanged += (IUploadProgress progress) =>
            {
                if (progress.Status == UploadStatus.Failed)
                    throw new Exception("File fail to upload.");
                //Console.WriteLine(progress.Status + " " + progress.BytesSent);
            };


            insertRequest.ResponseReceived += (Google.Apis.Drive.v3.Data.File file) =>
            {
                //Console.WriteLine(file.Name + " was uploaded successfully");
                tcs.SetResult(file);
            };

            await insertRequest.UploadAsync();

            return await tcs.Task;
        }

        public async Task UpdateFileAsync(IFormFile file, string fileName, string fileId)
        {
            using var uploadStream = file.OpenReadStream();

            Google.Apis.Drive.v3.Data.File driveFile = new()
            {
                Name = fileName
            };

            var updateRequest = _service.Files.Update(driveFile, fileId, uploadStream, file.ContentType);

            updateRequest.ProgressChanged += Upload_ProgressChanged;
            updateRequest.ResponseReceived += Upload_ResponseReceived;

            await updateRequest.UploadAsync();

            static void Upload_ProgressChanged(IUploadProgress progress) =>
                Console.WriteLine(progress.Status + " " + progress.BytesSent);

            static void Upload_ResponseReceived(Google.Apis.Drive.v3.Data.File file) =>
                Console.WriteLine(file.Name + " was uploaded successfully");
        }

        public void DeleteFile(string fileId)
        {
            var x = _service.Files.Delete(fileId).Execute();
            Console.WriteLine(x);
        }
    }
}
