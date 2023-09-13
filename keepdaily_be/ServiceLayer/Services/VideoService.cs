using ServiceLayer.IServices;
using Xabe.FFmpeg;

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

        public async Task ConvertImagesToVideoAsync(IEnumerable<string> imagePaths, string outputVideoPath, int frameRate)
        {
            try
            {
                //Directory.EnumerateFiles();
                if (imagePaths.Any())
                {
                    await new Conversion()
                        .SetInputFrameRate(frameRate)
                        .BuildVideoFromImages(imagePaths)
                        .SetFrameRate(frameRate)
                        .SetPixelFormat(PixelFormat.yuv420p)
                        .SetOutput(outputVideoPath)
                        .Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
