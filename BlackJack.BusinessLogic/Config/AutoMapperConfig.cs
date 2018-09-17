using AutoMapper;
using BlackJack.Entities.Entities;
using BlackJack.ViewModels.ViewModels;

namespace BlackJack.BusinessLogic.Config
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<GamePlayer, GamePlayerViewModel>();
                config.CreateMap<Game, GameViewModel>();
                config.CreateMap<Log, GetLogsViewModel>();

                config.CreateMap<GamePlayer, PlayerViewModel>()
                    .ForMember("Name", opt => opt.MapFrom(c => c.Player.Name));
            });
        }
    }
}
