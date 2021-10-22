using FirmyMichalowice.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FirmyMichalowice.Helpers;

namespace FirmyMichalowice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArmsController : ControllerBase
    {
        private readonly IMunicipalitieRepository _municipalitieRepository;
        private readonly ILoggerManager _logger;

        public ArmsController(IMunicipalitieRepository municipalitieRepository, ILoggerManager logger)
        {
            _municipalitieRepository = municipalitieRepository;
            _logger = logger;

        }

        [HttpGet ("getarms")]
        public async Task<List<string>> GetArms()
        {
            try
            {

                var municipalitiesUrls = await _municipalitieRepository.GetMunicipalities();
                return municipalitiesUrls.Select(x => x.Path).ToList();
             
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return null;
            }


        }
    }
}
