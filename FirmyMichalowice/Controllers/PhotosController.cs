

using FirmyMichalowice.Helpers;
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
        private readonly ICompanyRepository _companyRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;
        private readonly ILoggerManager _logger;

        public PhotosController(IPhotoRepository photoRepository, 
                                ICompanyRepository companyRepository,
                                IHostingEnvironment environment,
                                 IConfiguration config,
                                 ILoggerManager logger)
        {
            _photoRepository = photoRepository;
            _companyRepository = companyRepository;
            _hostingEnvironment = environment;
            _configuration = config;
            _logger = logger;
        }

        [Authorize]
        [HttpPost("upload/{userId}")]
        //[Route("upload")]
        public async Task<IActionResult> Upload([FromRoute] int userId)
        {
            try
            {
                var image = Request.Form.Files[0];
                var id = userId;
#if (DEBUG)
                string PureFileName = new FileInfo(image.FileName).Name;
                string ftpUri = _configuration.GetSection("FtpAccount:FtpUri").Value;
                string userFolderId = string.Format("{0}/{1}", ftpUri, "Images/" + userId);
                bool isDirectory = DirectoryExists(userFolderId);
                if (!isDirectory)
                {
                    CreateDirectory(userFolderId);
                }
                else
                {
                    try
                    {
                        var result = GetFilesArray(userFolderId);
                        foreach(var file in result)
                        {
                            FtpWebRequest request3 = (FtpWebRequest)WebRequest.Create(string.Format("{0}/{1}", userFolderId, file));
                            AuthorizeRequest(ref request3);
                            request3.Method = WebRequestMethods.Ftp.DeleteFile;

                            using (FtpWebResponse response3 = (FtpWebResponse)request3.GetResponse())
                            {
                                var res = response3.StatusDescription;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                string uploadUrl = string.Format("{0}/{1}", userFolderId, PureFileName);
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uploadUrl);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                AuthorizeRequest(ref request);
                request.Proxy = null;
                request.KeepAlive = true;
                request.UseBinary = true;
                request.Method = WebRequestMethods.Ftp.UploadFile;

                var filePath = Path.Combine(uploadUrl, image.FileName);
                byte[] fileContents = await GetByteArrayFromImageAsync(Request.Form.Files[0]);


                request.ContentLength = fileContents.Length;
                Stream requestStream = request.GetRequestStream();
                requestStream.Write(fileContents, 0, fileContents.Length);
                requestStream.Close();
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Console.WriteLine("Upload File Complete, status {0}", response.StatusDescription);
            }
            catch (WebException e)
            {
                String status = ((FtpWebResponse)e.Response).StatusDescription;
            }
#else
            var folderName = Path.Combine("Resources", "Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), Path.Combine(folderName, id.ToString()));
            if (!Directory.Exists(pathToSave))
            {
                Directory.CreateDirectory(pathToSave);
            }
            if (image.Length > 0)
            {
                System.IO.DirectoryInfo di = new DirectoryInfo(pathToSave);

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }
                var filePath = Path.Combine(pathToSave, image.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
            }
#endif

            return Ok();
        }

        private void CreateDirectory(string directory)
        {

            WebRequest request = WebRequest.Create(directory);
            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            using (var resp = (FtpWebResponse)request.GetResponse())
            {
                Console.WriteLine(resp.StatusCode);
            }
        }
 
        [HttpGet("download/{userId}")]
        public async Task<IActionResult> Download([FromRoute] int userId)
        {

#if (DEBUG)
            try
            {
                string ftpUri = _configuration.GetSection("FtpAccount:FtpUri").Value;
                string userFolderId = string.Format("{0}/{1}", ftpUri, "Images/" + userId);
                var imagesNames = GetFilesArray(userFolderId);

                if (imagesNames == null)
                {
                    var folderName = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Images");
                    var filePath = Path.Combine(folderName, "Default/LogoTemp.png");
                    if (!System.IO.File.Exists(filePath))
                        return NotFound();

                    var memory = new MemoryStream();
                    using (var stream = new FileStream(filePath, FileMode.Open))
                    {
                        await stream.CopyToAsync(memory);
                    }
                    memory.Position = 0;

                    return File(memory, GetContentType(filePath));
                }
                string ftpUserName = _configuration.GetSection("FtpAccount:UserAccount").Value;
                string ftpUserPass = _configuration.GetSection("FtpAccount:UserPass").Value;

                using (WebClient request2 = new WebClient())
                {
                    request2.Credentials = new NetworkCredential(ftpUserName, ftpUserPass);
                    byte[] fileData = request2.DownloadData(userFolderId + '/' + imagesNames[0]);
                    //string temp_inBase64 = Convert.ToBase64String(fileData);

                    return File(fileData, "application/octet-stream");

                }

                return null;
            }
            catch (Exception e)
            {
               
                return null;
            }
#else
            var folderName = Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Images");
            var filePath = Path.Combine(folderName, userId.ToString());
            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, GetContentType(filePath));
#endif
        }

        
        [Authorize]
        [HttpGet]
        [Route("files")]
        public IActionResult Files()
        {
            var result = new List<string>();

            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            if (Directory.Exists(uploads))
            {
                var provider = _hostingEnvironment.ContentRootFileProvider;
                foreach (string fileName in Directory.GetFiles(uploads))
                {
                    var fileInfo = provider.GetFileInfo(fileName);
                    result.Add(fileInfo.Name);
                }
            }
            return Ok(result);
        }


        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;
            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }

        private async Task<byte[]> GetByteArrayFromImageAsync(IFormFile file)
        {
            using (var target = new MemoryStream())
            {
                await file.CopyToAsync(target);
                return target.ToArray();
            }
        }


        public bool DirectoryExists(string directory)
        {
            try
            {
                FtpWebRequest request = GetRequest(directory);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                return request.GetResponse() != null;
            }
            catch (WebException e)
            {
                String status = ((FtpWebResponse)e.Response).StatusDescription;
                return false;
            }
        }

        private FtpWebRequest GetRequest(string directory)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(directory);
            AuthorizeRequest(ref request);
            return request;
        }

        private void AuthorizeRequest(ref FtpWebRequest request)
        {
            string ftpUserName = _configuration.GetSection("FtpAccount:UserAccount").Value;
            string ftpUserPass = _configuration.GetSection("FtpAccount:UserPass").Value;
            request.Credentials = new NetworkCredential(ftpUserName, ftpUserPass);
        }

        private string[] GetFilesArray(string userFolderId)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(userFolderId);
                request.Method = WebRequestMethods.Ftp.ListDirectory;

                AuthorizeRequest(ref request);
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                string names = reader.ReadToEnd();

                reader.Close();
                response.Close();

                var result = names.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                return result;
            }
            catch(WebException ex)
            {
                String status = ((FtpWebResponse)ex.Response).StatusDescription;
                _logger.LogInformation(status);
                return null;
            }
        }
    }
}
