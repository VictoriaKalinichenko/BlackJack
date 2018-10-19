using AutoMapper;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.Entities.Entities;
using BlackJack.ViewModels.ViewModels.Game;
using BlackJack.ViewModels.ViewModels.Log;
using BlackJack.ViewModels.ViewModels.Start;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack.BusinessLogic.Mappers
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<GamePlayer, GamePlayerItem>()
                    .ForMember("Cards", opt => opt.MapFrom(c => GetCardsStringList(c.PlayerCards)))
                    .ForMember("Name", opt => opt.MapFrom(c => c.Player.Name));

                config.CreateMap<GamePlayer, AddCardGameView>()
                    .ForMember("Cards", opt => opt.MapFrom(c => GetCardsStringList(c.PlayerCards)))
                    .ForMember("Name", opt => opt.MapFrom(c => c.Player.Name));

                config.CreateMap<Game, StartInitRoundViewModel>();
                config.CreateMap<HistoryMessage, GetAllLogView>();

                config.CreateMap<GamePlayer, PlayerItem>()
                    .ForMember("Name", opt => opt.MapFrom(c => c.Player.Name));
            });
        }

        private static List<string> GetCardsStringList(IEnumerable<PlayerCard> playerCards)
        {
            var cardsStringList = playerCards.ToList().ConvertAll(delegate (PlayerCard playerCard)
            {
                return playerCard.Card.ToString();
            });
            return cardsStringList;
        }
    }
}
