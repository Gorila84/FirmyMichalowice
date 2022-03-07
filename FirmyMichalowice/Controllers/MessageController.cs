using FirmyMichalowice.Data;
using FirmyMichalowice.Helpers;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace FirmyMichalowice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private SmtpManager _smtpManager;

        public MessageController(SmtpManager smtpManager, ILoggerManager logger)
        {
            _smtpManager = smtpManager;
            _logger = logger;
        }

        [HttpPost("AddMessage")]
        [AllowAnonymous]
        public async Task<bool> AddMessage(Message message, bool isResetPassRequest = false)
        {
            if (ModelState.IsValid && message.IsEmailValid()) {
                MimeMessage message2 = new MimeMessage();

                MailboxAddress from = new MailboxAddress("Admin FPK", _smtpManager.User);
                message2.From.Add(from);

                MailboxAddress to = new MailboxAddress("User", message.Username);
                message2.To.Add(to);

                message2.Subject = message.Subject;
                BodyBuilder bodyBuilder = new BodyBuilder();
                if (isResetPassRequest) bodyBuilder.HtmlBody = message.Content;
                else bodyBuilder.HtmlBody = "<b><u> Wiadomość od: </u></b>" + message.Username + "<br> <br>" + message.Content + "<br> <br>" + "<p style='font-size:8px'>Wiadomość od FirmyMichalowice</p>";
                message2.Body = bodyBuilder.ToMessageBody();

                bool result = await SendEmail(message2);
                return result;
            }
            return false;
        }

        private async Task<bool> SendEmail(MimeMessage message2)
        {
            try
            {
                SmtpClient client = new SmtpClient();
                client.Connect(_smtpManager.Server, _smtpManager.Port, true);
                client.Authenticate(_smtpManager.User, _smtpManager.Password);
                client.Send(message2);
                client.Disconnect(true);
                client.Dispose();

                // insert to db ? 
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return false;
            }
        }
        [HttpGet("AddMessage")]
        [AllowAnonymous]
        public void AddMessage(string json)
        {
            Message message = Newtonsoft.Json.JsonConvert.DeserializeObject<Message>(json);
            _ = AddMessage(message, true);
        }


    }
}
