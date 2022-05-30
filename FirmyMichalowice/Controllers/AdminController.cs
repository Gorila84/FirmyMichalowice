using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FirmyMichalowice.Data;
using FirmyMichalowice.Dto_s;
using FirmyMichalowice.Helpers;
using FirmyMichalowice.Model;
using FirmyMichalowice.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirmyMichalowice.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly IAdminRepository _adminRepository;
        private readonly IGenericRepository _genericRepository;

        public AdminController(ICompanyRepository companyRepository, 
                               IMapper mapper, 
                               ILoggerManager logger,
                               IAdminRepository adminRepository,
                               IGenericRepository genericRepository)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _logger = logger;
            _adminRepository = adminRepository;
            _genericRepository = genericRepository;
        }

        public IMapper Mapper { get; }

        [HttpGet("getUsersForAdmin")]
        public async Task<IActionResult> GetCompaniesForAdmin()
        {
            try
            {
                

                var users = await _companyRepository.GetCompaniesForAdmin();
                
                var usersToReturn = _mapper.Map<IEnumerable<CompaniesForAdminDTO>>(users);

               

                return Ok(usersToReturn);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return StatusCode(500);
            }

        }

        [HttpGet("admin/getUser/{id}/{isForEdit}")]
        public async Task<IActionResult> GetUserForAdmin(int id, bool isForEdit)
        {
            var user = await _companyRepository.GetCompany(id, isForEdit);

            var userToReturn = _mapper.Map<CompaniesForAdminDTO>(user);
            
            return Ok(userToReturn);
        }

       
        [HttpPut("uzytkownicy")]

        public async Task<IActionResult> UpdateCompanyForAdmin(CompaniesForEditAdminDTO companiesForEditAdminDTO)
        {

            var comapnyFromRepository = await _companyRepository.GetCompany(companiesForEditAdminDTO.Id, true);

            _mapper.Map(companiesForEditAdminDTO, comapnyFromRepository);
            comapnyFromRepository.Modify = DateTime.Now;

            if (await _companyRepository.SaveAll())
                return NoContent();

            throw new Exception($"Aktualizacja użytkownika o id: {companiesForEditAdminDTO.Id} nie powiodła sie przy zapisywaniu do bazy");
        }


        [HttpGet("topfive")]
        public async Task<IActionResult> GetTopFiveCompanies()
        {
            var firstFiveUsers = await  _companyRepository.GetTopFiveUsers();
           // var usersToReturn = _mapper.Map<IEnumerable<CompaniesForAdminDTO>>(firstFiveUsers);
            return Ok(firstFiveUsers);
        }

        [HttpGet("lastfive")]
        public async Task<IActionResult> GetLAstFiveCompanies()
        {
            var lastFirstFiveUsers = await _companyRepository.GetLastFiveUsers();
            // var usersToReturn = _mapper.Map<IEnumerable<CompaniesForAdminDTO>>(firstFiveUsers);
            return Ok(lastFirstFiveUsers);
        }

        
        [HttpPost("addKey")]
        public async Task<IActionResult> AddConfigurationKey(AddConfigurationForDTO addConfigurationForDTO)
        {
            try
            {
                AppConfiguration appConfiguration = _mapper.Map<AppConfiguration>(addConfigurationForDTO);
                var key = await _adminRepository.AddConfigurations(appConfiguration);
                return Ok(addConfigurationForDTO);
            }
            catch (Exception e)
            {

                _logger.LogInformation(e.Message);
                return StatusCode(400, e.Message);
            }
            
        }

        [HttpPut("updateKey")]
        public async Task<IActionResult> UpdateConfigurationKey(int id, AddConfigurationForDTO addConfigurationForDTO)
        {


            var keyForUpdate = await _adminRepository.GetAppConfigurationForEdit(id);
                _mapper.Map(addConfigurationForDTO, keyForUpdate);
                keyForUpdate.Update = DateTime.Now;
                if (await _genericRepository.SaveAll())
                    return NoContent();

                throw new Exception($"Aktualizacja oferty o id: {id} nie powiodła sie przy zapisywaniu do bazy");

        }

        [HttpGet("configurationKey")]
        public async Task<IActionResult> GetConfigurationKeys()
        {
            var keys = await _adminRepository.GetAppConfigurationKeys();
            return  Ok(keys);
        }

        [HttpGet("keyValue")]
        public async Task<IActionResult> GetKeyValue(AddConfigurationForDTO addConfigurationForDTO)
        {
            var keyValue = await _adminRepository.GetAppConfigurationValue(addConfigurationForDTO.KeyName);
            return Ok(keyValue);
        }



    }

  
}