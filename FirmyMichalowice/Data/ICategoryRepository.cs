using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Data
{
    public interface ICategoryRepository
    {
        Task<IList<string>> GetCategory();
    }
}
