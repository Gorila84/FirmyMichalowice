using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FirmyMichalowice.Dto_s;
using FirmyMichalowice.Helpers;
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

        public AdminController(ICompanyRepository companyRepository, IMapper mapper, ILoggerManager logger)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
            _logger = logger;
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

       
        [HttpPut("admin/uzytkownicy/{id}")]

        public async Task<IActionResult> UpdateCompanyForAdmin(int id, CompaniesForAdminDTO companiesForAdminDTO)
        {

            var comapnyFromRepository = await _companyRepository.GetCompany(id, true);

            _mapper.Map(companiesForAdminDTO, comapnyFromRepository);
            comapnyFromRepository.Modify = DateTime.Now;

            if (await _companyRepository.SaveAll())
                return NoContent();

            throw new Exception($"Aktualizacja użytkownika o id: {id} nie powiodła sie przy zapisywaniu do bazy");
        }
    }

  
}