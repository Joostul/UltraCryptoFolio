using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace UltraCryptoFolio.Services
{
    public class EmailSender : IEmailSender
    {
        private ISendGridClient _sendGridClient;
        public EmailSender(ISendGridClient sendGridClient)
        {
            _sendGridClient = sendGridClient;
        }
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("registration@ultracryptofolio.com"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            await _sendGridClient.SendEmailAsync(msg);
        }
    }
}
