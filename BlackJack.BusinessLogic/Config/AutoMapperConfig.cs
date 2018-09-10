using AutoMapper;
using BlackJack.Entities.Models;
using BlackJack.ViewModels.ViewModels;

namespace BlackJack.BusinessLogic.Config
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(config => config.CreateMap<GamePlayer, GamePlayerViewModel>());
        }
    }
}
