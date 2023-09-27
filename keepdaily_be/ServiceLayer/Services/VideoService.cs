using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ServiceLayer.IServices;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Xabe.FFmpeg;
using System;
using System.Net.Mail;

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
                var count = imagePaths.Count;
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

        public async Task PartialWriteToStream(FileInfo file, long videoStart, long videoEnd, Stream outputStream)
        {
            int bufferSize = 1024 * 1024;
            var buffer = new byte[bufferSize];

            using (var video = file.OpenRead())
            {
                var position = videoStart;
                var bytesLeft = videoEnd - videoStart + 1;
                // set position
                video.Position = videoStart;
                var bytesRead = 1;
                while (position <= videoEnd)
                {
                    bytesRead = video.Read(buffer, 0, (int)Math.Min(bytesLeft, buffer.Length));
                    await outputStream.WriteAsync(buffer, 0, bytesRead);
                    position += bytesRead;
                    bytesLeft = videoEnd - position + 1;
                }
            }
        }

        public HttpContent WriteToStream(string filePath, RangeHeaderValue rangeHeader)
        {
            long totalSize = 0;
            int bufferSize = 1024 * 1024;
            var buffer = new byte[bufferSize];
            var bytesRead = 1;
            var range = new List<RangeItemHeaderValue>(rangeHeader.Ranges)[0];
            long videoStart = range.From ?? 0;
            long videoEnd = 0;
            var content = new PushStreamContent(async (outputStream, content, transportContext) =>
            {

                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    totalSize = fileStream.Length;
                    videoEnd = range.To ?? totalSize;
                    var position = videoStart;
                    var bytesLeft = videoEnd - videoStart + 1;
                    // set position
                    fileStream.Position = videoStart;
                    while (position <= videoEnd)
                    {
                        //Throttle((int)Math.Min(bytesLeft, buffer.Length));
                        bytesRead = fileStream.Read(buffer, 0, (int)Math.Min(bytesLeft, buffer.Length));
                        await outputStream.WriteAsync(buffer.AsMemory(0, bytesRead));
                        position += bytesRead;
                        bytesLeft = videoEnd - position + 1;
                    }
                }
            }, "video/mp4");
            content.Headers.ContentRange = new ContentRangeHeaderValue(bytesRead, videoEnd, totalSize);
            return content;
        }

        public async void WriteToStream(string filePath, Stream outputStream, HttpContent content, TransportContext transportContext)
        {
            int bufferSize = 1024 * 1024;
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
                        await outputStream.WriteAsync(buffer.AsMemory(0, sizeOfReadedBuffer));
                        // Decrementing  
                        totalSize -= sizeOfReadedBuffer;
                    }
                }
        }
    }
}
