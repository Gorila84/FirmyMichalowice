using FirmyMichalowice.Data;
using FirmyMichalowice.Dto_s;
using FirmyMichalowice.Helpers;
using FirmyMichalowice.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly DataContext _context;
        private readonly ILoggerManager _logger;
        public PhotoRepository(DataContext context, ILoggerManager logger)
        {
            _context = context;
            _logger = logger;
        }

        public bool SaveImage(Photo logo)
        {
            try
            {
                if (_context.Photo.Any(x => x.UserId == logo.UserId))
                {
                    RemoveUserLogo(logo.UserId);

                }
                _context.Photo.Add(logo);
                _context.SaveChanges();
                return true;
            }

            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return false;
            }

        }

        private void RemoveUserLogo(int userId)
        {
            Photo photo = _context.Photo.Where(x => x.UserId == userId).FirstOrDefault();
            _context.Photo.Remove(photo);
            _context.SaveChanges();
        }
    }
}
