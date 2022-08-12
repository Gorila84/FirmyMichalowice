using FirmyMichalowice.Data;
using FirmyMichalowice.Helpers;
using FirmyMichalowice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;
        private readonly ILoggerManager _logger;

        public CategoryRepository(DataContext context, ILoggerManager logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IList<string>> GetCategory()
        {
            return _context.Categories.OrderBy(x => x.CategoryName).Select(x => x.CategoryName).ToList();
        }

      


    }
}
