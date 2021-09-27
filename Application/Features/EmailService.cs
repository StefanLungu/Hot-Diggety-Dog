using Application.Interfaces;
using Application.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace Application.Features
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public void Send(string from, string to, string subject, string html)
        {
            MimeMessage email = new();
            email.From.Add(MailboxAddress.Parse(from));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            using SmtpClient smtp = new();
            smtp.Connect(_emailSettings.SMTPHost, _emailSettings.SMTPPort, SecureSocketOptions.StartTls);
            smtp.Authenticate(_emailSettings.SMTPUser, _emailSettings.SMTPPassword);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
