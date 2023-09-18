using System.Net;

namespace ServiceLayer.IServices
{
    public interface IVideoService
    {
        public Task ConvertImagesToVideoAsync(IList<string> imagePaths, IList<string> texts, string outputVideoPath, int frameRate);
        public void WriteToStream(string filePath, Stream outputStream, HttpContent content, TransportContext transportContext);
    }
}
