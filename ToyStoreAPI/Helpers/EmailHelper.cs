using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using ToyStoreAPI.Models;

namespace ToyStoreAPI.Helpers
{
    public class EmailHelper
    { 
        private readonly Account _account;

        public EmailHelper(Account account)
        {
            _account = account;
        }

        public void SendEmail(string contactName,string formAdrress, string messageBody)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(contactName, formAdrress));
            message.To.Add(new MailboxAddress("Bui Duc Huy", "duchuyy16@gmail.com"));
            message.Subject = "Mail From Contact Us";
            message.Body = new TextPart("html")
            {
                Text = "<h1>Thông tin liên hệ mới</h1><br/>" +
                       "<p><strong>Tên người liên hệ:</strong> " + contactName + "</p>" +
                       "<p><strong>Email người liên hệ:</strong> " + formAdrress + "</p>" +
                       "<p><strong>Nội dung:</strong> " + messageBody + "</p>"
            };
            using var client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            client.Authenticate(_account.Email,_account.Password);
            client.Send(message);
            client.Disconnect(true);
        }
            
        public void SendEmailResetPassword(string toAdrress, string messageBody)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Bui Duc Huy", "duchuyy16@gmail.com"));
            message.To.Add(new MailboxAddress("ToyStore", toAdrress));
            message.Subject = "Reset Password";
            message.Body = new TextPart("html")
            {
                Text = messageBody
            };
            using var client = new SmtpClient();
            client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            client.Authenticate(_account.Email, _account.Password);
            client.Send(message);
            client.Disconnect(true);
        }
    }
}
