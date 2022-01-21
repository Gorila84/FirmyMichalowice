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
        public void AddConsent([FromBody] CookieConsentDTO consent)
        {
            try
            {
                consent.Date = DateTime.Now;
                var _consent = _mapper.Map<CookieConsent>(consent);
                _consentRepository.AddConsent(_consent);
                //return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogInformation(ex.Message);
                //return BadRequest();
            }
        }
        
    }
}
