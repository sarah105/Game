using Game_DataAccess.Data;
using Game_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_DataAccess.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly GameDbContext db;

        public AccountRepository(GameDbContext db)
        {
            this.db = db;
        }
        public Account Add(Account account)
        {
            db.Accounts.Add(account);
            if (Save()) return account;
            return null;
        }

        public bool Delete(Account account)
        {
            db.Accounts.Remove(account);
            return Save();
        }

        public Account Find(int id)
        {
            var account = db.Accounts.SingleOrDefault(item => item.Id == id);
            return account;
        }

        public Account Find(string email)
        {
            var account = db.Accounts.SingleOrDefault(item => item.Email == email);
            return account;
        }

        public IList<Account> List()
        {
            return db.Accounts.ToList();
        }

        public bool Update(Account entity)
        {
            db.Accounts.Update(entity);
            return Save();
        }
        private bool Save()
        {
            return db.SaveChanges() >= 0;
        }
    }
}
