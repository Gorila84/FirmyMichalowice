﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirmyMichalowice.Dto_s;
using FirmyMichalowice.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirmyMichalowice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChangePasswordController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public ChangePasswordController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }


        [HttpPost("{id}")]
        public async Task<ActionResult> ChangePassword(int id, UserForLoginDTO userForLoginDTO)
        {
            try
            {
                _authRepository.ChangePassword(id, userForLoginDTO.Password);
                return Ok(200);
                
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }
           
        }
    }
}