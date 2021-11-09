
using FirmyMichalowice.Helpers;
using FirmyMichalowice.Model;
using FirmyMichalowice.Repositories;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FirmyMichalowice.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IPhotoRepository _photoRepository;
        private readonly ILoggerManager _logger;

        public PhotosController(IPhotoRepository photoRepository,
                                 ILoggerManager logger)
        {
            _photoRepository = photoRepository;
            _logger = logger;
        }

        [Authorize]
        [HttpPost("upload/{userId}")]
  
        public async Task<IActionResult> Upload([FromRoute] int userId)
        {
            try
            {
                List<string> allowedExtensions = new List<string>() { "image/png", "image/jpeg", "image/jpg" };
                var image = Request.Form.Files[0];
                if (image.Length > 2500000 || !allowedExtensions.Contains(image.ContentType))
                {
                    return BadRequest();
                }
                var id = userId;
                Photo logo = new Photo();
                using (var ms = new MemoryStream())
                {
                    image.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    string s = Convert.ToBase64String(fileBytes);
                    logo = new Photo { DateAdded = DateTime.Now, UserId = userId, IsMain = true, Description = "This is main Logo", Url = image.FileName, FileData = fileBytes };
                }

                bool result = await _photoRepository.SaveImage(logo);

                if (result)
                    return Ok();
                else
                    return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return BadRequest();
            }
        }
     
    }
}
