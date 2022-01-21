using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirmyMichalowice.Data;
using FirmyMichalowice.Dto_s;
using FirmyMichalowice.Helpers;
using FirmyMichalowice.Repositories;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace FirmyMichalowice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResetPasswordController : ControllerBase
    {
        private readonly IAuthRepository _repository;
        private readonly SmtpManager _smtpManager;
        private readonly ILoggerManager _logger;

        public ResetPasswordController(IAuthRepository repository, SmtpManager smtpManager, ILoggerManager logger)
        {
            _repository = repository;
            _smtpManager = smtpManager;
            _logger = logger;
        }

        [HttpPost()]
        public async Task<IActionResult> ResetPassword(UserForLoginDTO userForLoginDto)
        {

            try
            {
                MimeMessage message2 = new MimeMessage();

                MailboxAddress from = new MailboxAddress("Admin", "admin@firmymichalowiceapi.berg-dev.eu");
                message2.From.Add(from);

                MailboxAddress to = new MailboxAddress("Admin", userForLoginDto.UserName);
                message2.To.Add(to);

                message2.Subject = "Twoje hasło do serwisu Firmy Północy Krakowa zostało zresetowane";
                BodyBuilder bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = "<b><u> Twoje hasło zostało zresetowane. </u></b> <br> <br> <p> Twoje nowe hasło:</p>" + _repository.ResetPassword(userForLoginDto.UserName) + "<br> <br>" + "<p style='font-size:8px'>Wiadomość od FirmyMichalowice</p>";
                message2.Body = bodyBuilder.ToMessageBody();

                bool result = SendEmail(message2);

                return Ok(200);
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
               
              
                   
        }

        private bool SendEmail(MimeMessage message2)
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








    }
}