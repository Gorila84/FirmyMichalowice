﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FirmyMichalowice.Dto_s;
using FirmyMichalowice.Helpers;
using FirmyMichalowice.Model;
using FirmyMichalowice.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;
using FirmyMichalowice.Data;

namespace FirmyMichalowice.Controllers
{

    [Route("api/[controller]")]
    [ApiController]

    public class CompanyController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IOfferRepository _offerRepository;
        private readonly ICompanyRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly CeidgService _cEIDGmanger;
        private readonly IMunicipalitieRepository _municipalitieRepository;

        public CompanyController(DataContext context, IOfferRepository offerRepository, ICompanyRepository userRepository, IMapper mapper, ILoggerManager logger, IConfiguration configuration, CeidgService cEIDGmanager, IMunicipalitieRepository municipalitieRepository)
        {
            _context = context;
            _offerRepository = offerRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
            _httpClient = new HttpClient();
            _configuration = configuration;
            _cEIDGmanger = cEIDGmanager;
            _municipalitieRepository = municipalitieRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] UserParams userParams)
        {
            try
            {

                var users = await _userRepository.GetCompanies(userParams);
                var usersToReturn = _mapper.Map<IEnumerable<CompaniesForListDTO>>(users);
                Response.AddPagination(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

                return Ok(usersToReturn);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Ręcznie wygenerowany błąd");
            }


        }
        [HttpGet("getUser/{id}/{isForEdit}")]
        public async Task<IActionResult> GetUser(int id, bool isForEdit)
        {
            var user = await _userRepository.GetCompany(id, isForEdit);

            var userToReturn = _mapper.Map<CompanyForDateilDTO>(user);
            userToReturn.ArmsUrl = _municipalitieRepository.GetMunicipalities().Result.Where(x => x.Name == user.Municipalitie).Select(x => x.Path).FirstOrDefault();

            return Ok(userToReturn);
        }

        [Authorize]
        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateCompany(int id, CompaniesForEditDTO companiesForEditDTO)
        {

            var comapnyFromRepository = await _userRepository.GetCompany(id, true);

            _mapper.Map(companiesForEditDTO, comapnyFromRepository);
            comapnyFromRepository.Modify = DateTime.Now;

            if (await _userRepository.SaveAll())
                return NoContent();

            throw new Exception($"Aktualizacja użytkownika o id: {id} nie powiodła sie przy zapisywaniu do bazy");
        }

        [HttpGet("getdatafromceidg/{nip}")]
        public async Task<JsonResult> GetDataFromCeidg(string nip)
        {
            var data = await _cEIDGmanger.GetData(nip);
            var result = new JsonResult(data);
            return result;
        }

        [HttpPost("addOffer")]
        public async Task<IActionResult> AddOffer(Offer offer)
        {

            try
            {
                offer.ModifyDate = DateTime.Now;
                await _context.Offers.AddAsync(offer);
                _context.SaveChanges();
                return Ok(offer);
            }
            catch (Exception e)
            {

                throw;
            }
          
        }

        [HttpGet("getOffers/{id}")]
        public async Task<IActionResult> GetOffers(int id)
        {
            var offers = await _offerRepository.GetOffer(id);
            //var offersForReturn = _mapper.Map<OfferDTO>(offers);
            return Ok(offers);
        }



        [HttpGet("getcompanytypes")]
        [Authorize]
        public async Task<IList<string>> GetCompanyTypes()
        {
            try
            {
                var companyTypes =  await _userRepository.GetCompanyTypes();
                return companyTypes;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return null;
            }


        }

  
    }
}