namespace ServiceLayer.IServices
{
    public interface IEmailService
    {
        public Task SendEmailAsync(string toEmail, string subject, string message);
    }
}
