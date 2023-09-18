using Microsoft.AspNetCore.Http;
using ServiceLayer.IServices;
using System.Net;
using System.Xml.Linq;
using Xabe.FFmpeg;
using static System.Net.Mime.MediaTypeNames;

namespace ServiceLayer.Services
{
    public class VideoService : IVideoService
    {
        public VideoService()
        {
            var ExecutablesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ffmpeg");
            Console.WriteLine(ExecutablesPath);
            FFmpeg.SetExecutablesPath(ExecutablesPath);
        }

        public async Task ConvertImagesToVideoAsync(IList<string> imagePaths, IList<string> texts, string outputVideoPath, int frameRate)
        {
            var textFile = Path.GetTempFileName();
            try
            {
                var count = imagePaths.Count();
                if (count > 0)
                {
                    await new Conversion()
                        .BuildVideoFromImages(imagePaths)
                        .SetInputFrameRate(frameRate)
                        .SetFrameRate(count)
                        .SetPixelFormat(PixelFormat.yuv420p)
                        .SetOutput(outputVideoPath)
                        .Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                File.Delete(textFile);
            }
        }

        public async void WriteToStream(string filePath, Stream outputStream, HttpContent content, TransportContext transportContext)
        {
            int bufferSize = 65536;
            byte[] buffer = new byte[bufferSize];

            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                int totalSize = (int)fileStream.Length;

                while (totalSize > 0)
                {
                    int count = totalSize > bufferSize ? bufferSize : totalSize;
                    // Reading the buffer from orginal file  
                    int sizeOfReadedBuffer = fileStream.Read(buffer, 0, count);
                    // Writing the readed buffer to output
                    await outputStream.WriteAsync(buffer, 0, sizeOfReadedBuffer);
                    // Decrementing  
                    totalSize -= sizeOfReadedBuffer;
                }
            }
        }
    }
}
