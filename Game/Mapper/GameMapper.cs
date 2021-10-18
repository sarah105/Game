using AutoMapper;
using Game_Models.Models;
using Game_Models.Models.Card;

namespace Game.Mapper
{
    public class GameMapper: Profile
    {
        public GameMapper()
        {
            CreateMap<Account, AccountDto>().ReverseMap();
            CreateMap<Card, CardDto>().ReverseMap();
        }
    }
}
