namespace ServiceLayer.IServices
{
    public interface IVideoService
    {
        public Task ConvertImagesToVideoAsync(IEnumerable<string> imagePaths, string outputVideoPath, int frameRate);
    }
}
