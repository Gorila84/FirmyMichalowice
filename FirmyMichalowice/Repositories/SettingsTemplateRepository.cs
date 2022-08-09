using FirmyMichalowice.Data;
using FirmyMichalowice.Helpers;
using FirmyMichalowice.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Repositories
{
    public class SettingsTemplateRepository: ISettingsTemplateRepository
    {
        private readonly DataContext _context;
        private readonly ILoggerManager _logger;

        public SettingsTemplateRepository(DataContext context, ILoggerManager logger)
        {
            _context = context;
            _logger = logger;

        }

        public async Task<IList<SettingsTemplate>> GetSettingsTemplates()
        {
            try
            {
                return await _context.SettingsTemplates.ToListAsync();
            }
           catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                return null;
            }
        }
    }
}
