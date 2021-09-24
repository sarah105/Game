using Game_DataAccess.Data;
using Game_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_DataAccess.Repositories
{
    class AccountRepository : IRepository<Account>
    {
        private readonly GameDbContext db;

        public AccountRepository(GameDbContext db)
        {
            this.db = db;
        }
        public bool Add(Account account)
        {
            db.Accounts.Add(account);
            return Save();
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
