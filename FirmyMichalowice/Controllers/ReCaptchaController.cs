using FirmyMichalowice.Model;
using FirmyMichalowice.Serv;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecaptchaController : ControllerBase
    {
        private RecaptchaService _recaptchaService;
        private readonly ILogger<RecaptchaController> _logger;
        public RecaptchaController(RecaptchaService recaptchaService, ILogger<RecaptchaController> logger)
        {
            _recaptchaService = recaptchaService;
            _logger = logger;
        }

        [HttpPost("check")]
        public async Task<RecaptchaServiceResponse> Check([FromBody] RecaptchaResponse response)
        {
            try
            {
                RecaptchaServiceResponse serviceResponse = await _recaptchaService.GetData(response);
                return serviceResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ' ' + ex.InnerException.Message);
            }

            return null;
        }

    }
}
