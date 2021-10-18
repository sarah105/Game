using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Game_Models.Models.Card.Card;

namespace Game_Models.Models.Card
{
    public class CardDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ManaCost { get; set; }
        //public enum CardType { Creature, Magic }
        public CardType Type { get; set; }
        public int Attack { get; set; }
        public int Health { get; set; }
    }
}
