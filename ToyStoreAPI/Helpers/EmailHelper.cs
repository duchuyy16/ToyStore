using MailKit.Security;
using MimeKit;
using MailKit.Net.Smtp;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace ToyStoreAPI.Helpers
{
    public static class EmailHelper
    {
        //Trình xác thực này luôn trả về giá trị true, cho phép SmtpClient kết nối với máy chủ email mà không cần xác thực chứng chỉ SSL.
        public class CustomSslCertificateValidator
        {
            public static bool ValidateCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
            {
                return true;
            }
        }

        public static void SendEmail(string contactName,string formAdrress, string messageBody)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("", formAdrress));
            message.To.Add(new MailboxAddress("", "duchuyy16@gmail.com"));
            //message.To.Add(new MailboxAddress("", toAddress));
            message.Subject = "Mail From Contact Us";
            message.Body = new TextPart("html")
            {
                Text = "<h1>Thông tin liên hệ mới</h1><br/>" +
                       "<p><strong>Tên người liên hệ:</strong> " + contactName + "</p>" +
                       "<p><strong>Email người liên hệ:</strong> " + formAdrress + "</p>" +
                       "<p><strong>Nội dung:</strong> " + messageBody + "</p>"
            };
            using var client = new SmtpClient();
            client.ServerCertificateValidationCallback = CustomSslCertificateValidator.ValidateCertificate!;
            client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            client.Authenticate("duchuyy16@gmail.com", "opeifkfxcqidghwg");
            client.Send(message);
            client.Disconnect(true);
        }
    }
}
