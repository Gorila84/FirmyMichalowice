using AutoMapper;
using FirmyMichalowice.Data;
using FirmyMichalowice.Dto_s;
using FirmyMichalowice.Helpers;
using FirmyMichalowice.Model;
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
    [AllowAnonymous]
    public class CookieConsentController : ControllerBase
    {
        private ICookieConsentRepository _consentRepository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;


        public CookieConsentController(ICookieConsentRepository consentRepository,ILoggerManager logger, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _consentRepository = consentRepository;
        }
        

      
        [HttpPost("addconsent")]
        public async Task<bool> AddConsent([FromBody] CookieConsentDTO consent)
        {
            try
            {
                consent.Date = DateTime.Now;
                var _consent = _mapper.Map<CookieConsent>(consent);
                var result = await _consentRepository.AddConsent(_consent);
                return result;
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message);
                //return BadRequest();
                return false;
            }
        }
        
    }
}
