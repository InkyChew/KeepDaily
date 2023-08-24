using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using Serilog;
using ServiceLayer.IServices;

namespace Mymvc.Services
{
    public class AuthMessageSenderOptions
    {
        public string? SendGridKey { get; set; }
    }

    public class EmailService : IEmailService
    {
        private readonly string _mail;
        private readonly string _password;
        public EmailService(IOptions<AuthMessageSenderOptions> optionsAccessor,
            IConfiguration config)
        {
            Options = optionsAccessor.Value;
            _mail = config["Sender:Email"] ?? throw new NullReferenceException("Null Sender:Email");
            _password = config["Sender:Password"] ?? throw new NullReferenceException("Null Sender:Password");
        }

        public AuthMessageSenderOptions Options { get; }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            if (string.IsNullOrEmpty(Options.SendGridKey))
            {
                throw new Exception("Null SendGridKey");
            }
            await Execute(Options.SendGridKey, subject, message, toEmail);
        }

        public async Task Execute(string apiKey, string subject, string message, string toEmail)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(_mail, _password),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(toEmail));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);
            var response = await client.SendEmailAsync(msg);
            Log.Information(response.IsSuccessStatusCode
                                   ? $"Email to {toEmail} queued successfully!"
                                   : $"Failure Email to {toEmail}");
        }
    }
}
