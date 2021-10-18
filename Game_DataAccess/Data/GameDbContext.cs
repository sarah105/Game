using Game_Models.Models;
using Game_Models.Models.Card;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_DataAccess.Data
{
    public class GameDbContext:DbContext
    {
        public GameDbContext(DbContextOptions <GameDbContext> options):base(options) { }

        public DbSet <Account> Accounts { get; set; }
        public DbSet <Card> Cards { get; set; }
    }
}
