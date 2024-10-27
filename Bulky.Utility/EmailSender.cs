using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace BookBazaar.Utility
{
    public class EmailSender : IEmailSender
    {
        public string SendGridSecret { get; set; }
        public EmailSender(IConfiguration configuration)
        {
            SendGridSecret = configuration["SendGrid:SecretKey"];
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // logic to send email
            var client = new SendGridClient(SendGridSecret);

            var from = new EmailAddress("hello@aliarmaganuygun.com", "Book Bazaar");
            var to = new EmailAddress(email);
            var message = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);

            return client.SendEmailAsync(message);
        }
    }
}
