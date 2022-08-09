using AutoMapper;
using FirmyMichalowice.Data;
using FirmyMichalowice.Dto_s;
using FirmyMichalowice.Helpers;
using FirmyMichalowice.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdditionalSettingsController : ControllerBase
    {

        private readonly ISettingsTemplateRepository _settingsTemplateRepository;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public AdditionalSettingsController(ISettingsTemplateRepository settingsTemplateRepository, IMapper mapper, ILoggerManager logger)
        {
            _settingsTemplateRepository = settingsTemplateRepository;
            _mapper = mapper;
            _logger = logger;

        }

        [HttpGet("GetAllSettings")]
        public async Task<IActionResult> GetAllSettings()
        {
            try
            {
                var data = await _settingsTemplateRepository.GetSettingsTemplates();
                var result = _mapper.Map<IEnumerable<SettingsTemplateDTO>>(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return StatusCode(500, "Ręcznie wygenerowany błąd");
            }


        }
    }
}
