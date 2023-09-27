using System.Net;
using System.Net.Http.Headers;

namespace ServiceLayer.IServices
{
    public interface IVideoService
    {
        public Task ConvertImagesToVideoAsync(IList<string> imagePaths, IList<string> texts, string outputVideoPath, int frameRate);
        public void WriteToStream(string filePath, Stream outputStream, HttpContent content, TransportContext transportContext);
        public HttpContent WriteToStream(string filePath, RangeHeaderValue rangeHeader);
        public Task PartialWriteToStream(FileInfo file, long videoStart, long videoEnd, Stream outputStream);
    }
}
