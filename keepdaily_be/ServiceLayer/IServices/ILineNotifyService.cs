namespace ServiceLayer.IServices
{
    public interface ILineNotifyService
    {
        public string GetAuthorizationUrl(string email);
        public Task<string> PostTokenAsync(string code);
    }
}
