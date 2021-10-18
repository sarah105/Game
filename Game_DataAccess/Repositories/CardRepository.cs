using Game_DataAccess.Data;
using Game_DataAccess.Repositories.IRepositories;
using Game_Models.Models.Card;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_DataAccess.Repositories
{
    public class CardRepository : ICURT<Card>
    {
        private readonly GameDbContext db;

        public CardRepository(GameDbContext db)
        {
            this.db = db;
        }

        public Card Add(Card card)
        {
            db.Cards.Add(card);
            if (Save()) return card;
            return null;
        }

        public Card Find(int id)
        {
            Card card = db.Cards.AsNoTracking().FirstOrDefault(ele => ele.Id == id);// get more info about .AsNoTracking()
            return card;
        }

        public IList<Card> List()
        {
            return db.Cards.ToList();
        }

        public bool Delete(Card card)
        {
            db.Cards.Remove(card);
            return Save();

        }

        public bool Update(Card card)
        {
            db.Cards.Update(card);
            //if (Save()) return card;
            return Save();
        }

        private bool Save()
        {
            return db.SaveChanges() >= 0;
        }
    }
}
