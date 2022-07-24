using FirmyMichalowice.Data;
using FirmyMichalowice.Helpers;
using FirmyMichalowice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly DataContext _context;
        private readonly ILoggerManager _logger;

        public AdminRepository(DataContext context, ILoggerManager logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> AddConfigurations(AppConfiguration appConfiguration)
        {
            try
            {
                appConfiguration.Create = DateTime.Now;
                appConfiguration.Update = DateTime.Now;
                _context.Add(appConfiguration);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                _logger.LogInformation(ex.Message);
                return false;
            }
        }
        public async Task<bool> CompanyConfigurations(CompanySetting companySetting)
        {
            try
            {
                companySetting.LinkVisibility = false;
                companySetting.PKDVisibility = false;
                companySetting.OfferVisibility = false;

                _context.Add(companySetting);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                _logger.LogInformation(ex.Message);
                return false;
            }
        }
        public async Task<AppConfiguration> GetAppConfigurationForEdit(int id)
        {
            var key = _context.AppConfigurations.Where(x => x.Id == id).FirstOrDefault();
            return key;
        }

        public async Task<List<AppConfiguration>> GetAppConfigurationKeys()
        {
            var keys = _context.AppConfigurations.ToList();
            return keys;
        }

        public async Task<bool> GetAppConfigurationValue(string keyName)
        {
            var keyValue =   _context.AppConfigurations.Where(x => x.KeyName == keyName).Select(y=>y.IsActive).FirstOrDefault();
            return keyValue;
        }

        public async Task<bool> GetLinkVisibility(int userId)
        {
            var linkVisibility = _context.CompanySettings.Where(x => x.UserId == userId).Select(y => y.LinkVisibility).FirstOrDefault();
            return linkVisibility;
        }

        public async Task<bool> GetPKDisibility(int userId)
        {
            var pkdVisibility = _context.CompanySettings.Where(x => x.UserId == userId).Select(y => y.PKDVisibility).FirstOrDefault();
            return pkdVisibility;
        }
        public async Task<bool> GetOfferisibility(int userId)
        {
            var offerVisibility = _context.CompanySettings.Where(x => x.UserId == userId).Select(y => y.OfferVisibility).FirstOrDefault();
            return offerVisibility;
        }

        public async Task<bool> AddKey(CompanySetting companySetting)
        {
            _context.Add(companySetting);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
