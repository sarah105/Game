﻿using Game_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_DataAccess.Repositories
{
    public interface IAccountRepository
    {
        IList<Account> List();
        Account Add(Account account);
        bool Delete(Account account);
        Account Find(int id);
        Account Find(string email);
        bool Update(Account account);
    }
}
