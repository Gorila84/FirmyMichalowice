using FirmyMichalowice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirmyMichalowice.Repositories
{
    public interface IAuthRepository
    {
        Task<User> Login(string nuserName, string pasword);
        Task<User> Register(User user, string password);
        Task<Tuple<bool, string>> UserValidation(string userName, string nip);
    }
}
