using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FirmyMichalowice.Dto_s;
using FirmyMichalowice.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirmyMichalowice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _userRepository;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {

                var users = await _userRepository.GetCompanies();

                var usersToReturn = _mapper.Map<IEnumerable<CompaniesForListDTO>>(users);

                return Ok(usersToReturn);
            }
            catch (Exception)
            {

                return StatusCode(500, "Ręcznie wygenerowany błąd");
            }


        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userRepository.GetCompany(id);

            var userToReturn = _mapper.Map<CompanyForDateilDTO>(user);

            return Ok(userToReturn);
        }
    }
}