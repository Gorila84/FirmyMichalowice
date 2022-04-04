﻿using FirmyMichalowice.Data;
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

        public async Task<bool> AddConfiguration(AppConfiguration appConfiguration)
        {
            try
            {
                appConfiguration.Create = DateTime.Now;
                appConfiguration.Update = DateTime.Now;
                _context.AppConfigurations.Add(appConfiguration);
                _context.SaveChangesAsync();
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
    }
}