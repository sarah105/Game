using AutoMapper;
using Game_Models.Models;

namespace Game.Mapper
{
    public class GameMapper: Profile
    {
        public GameMapper()
        {
            CreateMap<Account, AccountDto>().ReverseMap();
        }
    }
}
