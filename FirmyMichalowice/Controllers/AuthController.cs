using FirmyMichalowice.Data;
using FirmyMichalowice.Dto_s;
using FirmyMichalowice.Model;
using FirmyMichalowice.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FirmyMichalowice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repository;
        private readonly IConfiguration _config;
        private readonly CeidgService _ceidgService;
        private readonly IRegonService _regonService;

        public AuthController(IAuthRepository repository, IConfiguration config, CeidgService ceidgService, IRegonService regonService)
        {
            _repository = repository;
            _config = config;
            _ceidgService = ceidgService;
            _regonService = regonService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDTO userRegisterDto)
        {
            if (!IsValidEmail(userRegisterDto.UserName))
            {
                return BadRequest("Logowanie możliwe tylko za pomocą adresu email");
            }
            //var firma = await _ceidgService.GetData(userRegisterDto.NIP);
            var firmaRs = await _regonService.GetData(userRegisterDto.NIP);

            userRegisterDto.UserName = userRegisterDto.UserName.ToLower();
            

            var validationResult = await _repository.UserValidation(userRegisterDto.UserName, userRegisterDto.NIP, firmaRs.Gmina);
            if (validationResult.Item1)
                return BadRequest(validationResult.Item2);

            string companyAddress;

                if (!string.IsNullOrEmpty(firmaRs.NrLokalu))
                {
                    companyAddress = string.Format("{0} {1}/{2}", firmaRs.Ulica, firmaRs.NrNieruchomosci, firmaRs.NrLokalu);
                }
                else
                {
                    companyAddress = string.Format("{0} {1}", firmaRs.Ulica, firmaRs.NrNieruchomosci);
                }
            

            var userToCreate = new User
            {
                Username = userRegisterDto.UserName,
                NIP = userRegisterDto.NIP,
                Created = DateTime.Now,
                Municipalitie = validationResult.Item3,
                CompanyName =   firmaRs.Nazwa,
                City =  firmaRs.Miejscowosc,
                Street = companyAddress,
                PostalCode =  firmaRs.KodPocztowy,
                OfficeMunicipalitie =  firmaRs.Gmina

            };

             await _repository.Register(userToCreate, userRegisterDto.Password);



            return StatusCode(201);

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDTO userForLoginDto)
        {

            if (!IsValidEmail(userForLoginDto.UserName))
            {
                return BadRequest("Logowanie możliwe tylko za pomocą adresu email");
            }
            var userFromRepository = await _repository.Login(userForLoginDto.UserName.ToLower(),
                userForLoginDto.Password
                );
            if (userFromRepository == null)
                return Unauthorized();
            // tworzymy token
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepository.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepository.Username)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creeds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(12),
                SigningCredentials = creeds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
         
            return Ok(new { token = tokenHandler.WriteToken(token) });

        }

        
        [HttpPost("resetPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO userForLoginDto)
        {
            try
            {
                var result = _repository.ResetPassword(userForLoginDto.UserName);

                Message message = new Message() { Content = string.Format("<b><u> Wiadomość od: FPK - Firmy Północy Krakowa </u ></b> <br/> <br/> Twoje nowe hasło to : {0} <br/> Po zalogowaniu do aplikacji użyj opcji zmień hasło aby ustawić nowe hasło.<br/> <br/>" + "<p style='font-size:8px'>Wiadomość od FPK - Firmy Północy Krakowa, prosimy nie odpowiadać na ten email.</p>", result), Subject = "Twoje nowe hasło do aplikacji Firmy Północy Krakowa.", Username = userForLoginDto.UserName };
                if (message.IsEmailValid())
                {
                    string json = JsonConvert.SerializeObject(message);

                    return RedirectToAction("AddMessage", "Message", new { json });
                 
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {
                return null;
            }
         
        }

        [HttpPost("changePassword/{id}")]
        public async Task<ActionResult> ChangePassword(int id, UserForLoginDTO userForLoginDTO)
        {
            try
            {
                _repository.ChangePassword(id, userForLoginDTO.Password);
                return Ok(200);

            }
            catch (Exception e)
            {

                return BadRequest(e);
            }

        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

       
    }
}