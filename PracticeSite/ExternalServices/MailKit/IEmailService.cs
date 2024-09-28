namespace PracticeSite.ExternalServices.MailKit
{
    public interface IEmailService
    {
        void SendEmail(string to, string subject, string body);
    }
}
