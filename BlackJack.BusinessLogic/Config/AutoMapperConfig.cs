using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using BlackJack.Entities.Entities;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.ViewModels.ViewModels.Game;
using BlackJack.ViewModels.ViewModels.Log;
using BlackJack.ViewModels.ViewModels.Start;

namespace BlackJack.BusinessLogic.Config
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<GamePlayer, GamePlayerViewItem>()
                    .ForMember("Cards", opt => opt.MapFrom(c => GetCardsStringList(c.PlayerCards)))
                    .ForMember("Name", opt => opt.MapFrom(c => c.Player.Name));

                config.CreateMap<GamePlayer, AddCardViewModel>()
                    .ForMember("Cards", opt => opt.MapFrom(c => GetCardsStringList(c.PlayerCards)))
                    .ForMember("Name", opt => opt.MapFrom(c => c.Player.Name));

                config.CreateMap<Game, BeginRoundViewModel>();
                config.CreateMap<Log, GetLogsViewModel>();

                config.CreateMap<GamePlayer, PlayerBeginRoundViewItem>()
                    .ForMember("Name", opt => opt.MapFrom(c => c.Player.Name));
            });
        }

        private static List<string> GetCardsStringList(IEnumerable<PlayerCard> playerCards)
        {
            var cardsStringList = playerCards.ToList().ConvertAll(delegate (PlayerCard playerCard)
            {
                string cardString = CardToStringHelper.Convert(playerCard.Card);
                return cardString;
            });
            return cardsStringList;
        }
    }
}
