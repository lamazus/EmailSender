using EmailSender.Infrastructure.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace EmailSender.Infrastructure.Services
{
    public class SmtpService
    {
        private readonly SmtpSettings smtpSettings;

        public SmtpService(IOptions<SmtpSettings> smtpSettings)
        {
            this.smtpSettings = smtpSettings.Value;
        }

        public async void SendMessage(string subject, string body, List<string> recipients, CancellationToken cancellationToken)
        {
            var addresses = new InternetAddressList();
            foreach(var address in recipients)
            {
                var username = address.Substring(0, address.IndexOf("@"));
                addresses.Add(new MailboxAddress(username, address));
            }

            var msg = new MimeMessage();
            msg.From.Add(new MailboxAddress (smtpSettings.Username, smtpSettings.Email)); 
            msg.To.AddRange(addresses);
            msg.Subject = subject;
            msg.Body = new TextPart("plain")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                client.Connect(smtpSettings.Host, smtpSettings.Port, smtpSettings.UseSsl);
                client.Authenticate(smtpSettings.Email, smtpSettings.Password);
                await client.SendAsync(msg, cancellationToken);
                client.Disconnect(true);
            }
        }
    }
}