using FirmyMichalowice.Dto_s;
using FirmyMichalowice.Model;
using FirmyMichalowice.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
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
       

        public AuthController(IAuthRepository repository, IConfiguration config)
        {
            _repository = repository;
            _config = config;
           
           
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDTO userRegisterDto)
        {
            if (!IsValidEmail(userRegisterDto.UserName))
            {
                return BadRequest("Logowanie możliwe tylko za pomocą adresu email");
            }

            userRegisterDto.UserName = userRegisterDto.UserName.ToLower();
            var validationResult = await _repository.UserValidation(userRegisterDto.UserName, userRegisterDto.NIP);
            if (validationResult.Item1)
                return BadRequest(validationResult.Item2);

            var userToCreate = new User
            {
                Username = userRegisterDto.UserName,
                NIP = userRegisterDto.NIP,
                Created = DateTime.Now,
                Municipalitie = validationResult.Item3
            };

            var createdUser = await _repository.Register(userToCreate, userRegisterDto.Password);

           

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
        public async Task<IActionResult> ResetPassword(UserForLoginDTO userForLoginDto)
        {
            var result = _repository.ResetPassword(userForLoginDto.UserName);
           
                Message message = new Message() { Content = result, Subject = "Twoje nowe hasło do aplikacji Firmy Północy Krakowa.", Username = userForLoginDto.UserName };
                RedirectToAction("addmessage", "message", new { message = message });
           
            return Ok();


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