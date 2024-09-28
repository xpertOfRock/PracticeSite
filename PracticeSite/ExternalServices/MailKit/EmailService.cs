using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace PracticeSite.ExternalServices.MailKit
{
    public class EmailService : IEmailService
    {
        private readonly MailKitSettings _options;

        public EmailService(IOptions<MailKitSettings> options)
        {
            _options = options.Value;
        }

        public void SendEmail(string to, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_options.EmailUsername));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Plain) { Text = body };

            using var smtp = new SmtpClient();
            smtp.Connect(_options.EmailHost, _options.EmailPort, SecureSocketOptions.StartTls);
            smtp.Authenticate(_options.EmailUsername, _options.EmailPassword);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
