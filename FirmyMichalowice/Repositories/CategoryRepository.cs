using FirmyMichalowice.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;

        public CategoryRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IList<string>> GetCategory()
        {
            return _context.Categories.OrderBy(x => x.CategoryName).Select(x => x.CategoryName).ToList();
        }


    }
}
