using Abp.Extensions;
using FirmyMichalowice.Model;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace FirmyMichalowice.Repositories
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private MimeMessage CreateEmailMessage(EmailMessage message)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(message.Sender);
            mimeMessage.To.Add(message.Reciever);
            mimeMessage.Subject = message.Subject;
            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            { Text = message.Content };
            return mimeMessage;
        }

        public void SendEmail()
        {
            var emailFrom = _configuration.GetSection("EmailConfiguration:From").Value.ToString();
            var fromEmailDisplayName = _configuration.GetSection("EmailConfiguration:From").Value.ToString();
            var fromEmailPassword = _configuration.GetSection("EmailConfiguration:Password").Value.ToString();
            var smtpHost = _configuration.GetSection("EmailConfiguration:SmtpServer").Value.ToString();
            var smtpPort = int.Parse(_configuration.GetSection("EmailConfiguration:Port").Value);





            EmailMessage message = new EmailMessage();
            message.Sender = new MailboxAddress("Self", emailFrom);
            message.Reciever = new MailboxAddress("Self", "maciek.sikora84@gmail.com");
            message.Subject = "Welcome";
            message.Content = "Hello World!";
            var mimeMessage = CreateEmailMessage(message);

           
            using (SmtpClient smtpClient = new SmtpClient())
            {
                smtpClient.Connect(smtpHost,
                smtpPort, true);
                smtpClient.Authenticate(emailFrom,
                fromEmailPassword);
                smtpClient.Send(mimeMessage);
                smtpClient.Disconnect(true);
            }
            

            //using (SmtpClient smtpClient = new SmtpClient())
            //{


            //     smtpClient.Connect(smtpPort,
            //    smtpHost, true);
            //     smtpClient.Authenticate(emailFrom,
            //    fromEmailPassword);
            //     smtpClient.Send(mimeMessage);
            //     smtpClient.Disconnect(true);
            //}
        }
    }
}
