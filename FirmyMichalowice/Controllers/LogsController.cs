using FirmyMichalowice.Dto_s;
using FirmyMichalowice.Helpers;
using log4net;
using log4net.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace FirmyMichalowice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly ILoggerManager _logger;

        public LogsController(ILoggerManager logger)
        {
            _logger = logger;
        }

        [HttpPost("post")]
        public IActionResult Post([FromBody] LogDto dto)
        {
            MessageFromLog message = JsonConvert.DeserializeObject<MessageFromLog>(dto.Message);
            _logger.LogInformation(string.Format("ComponentName: {0}, UserId: {1}, ErrorText:  {2}", message.ComponentName, message.UserId, message.Message));
            return Ok();
        }
      

    }

}
