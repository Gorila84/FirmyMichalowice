using FirmyMichalowice.Data;
using FirmyMichalowice.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Repositories
{
    public class MunicipalitieRepository : IMunicipalitieRepository
    {
        private readonly DataContext _context;
        public MunicipalitieRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Municipalitie>> GetMunicipalities()
        {
            return await _context.Municipalities.Where(x=>x.Name != null && x.IsConsentToUse).ToListAsync();
        }
    }
}
