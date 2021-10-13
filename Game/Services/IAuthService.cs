using Game_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Game.Services
{
    public interface IAuthService
    {
        Auth Register(Account account);
        Auth Login(LoginModelDto account);
    }
}
