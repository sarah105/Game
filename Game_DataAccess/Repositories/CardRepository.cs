using Game_DataAccess.Data;
using Game_DataAccess.Repositories.IRepositories;
using Game_Models.Models.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_DataAccess.Repositories
{
    class CardRepository : ICURT<Card>
    {
        private readonly GameDbContext db;

        public CardRepository(GameDbContext db)
        {
            this.db = db;
        }

        public Card Add(Card entity)
        {
            throw new NotImplementedException();
        }

        public Card Find(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Card> List()
        {
            throw new NotImplementedException();
        }

        public bool Remove(Card entity)
        {
            throw new NotImplementedException();
        }

        public Card Update(Card entity)
        {
            throw new NotImplementedException();
        }

        private bool Save()
        {
            return db.SaveChanges() >= 0;
        }
    }
}
