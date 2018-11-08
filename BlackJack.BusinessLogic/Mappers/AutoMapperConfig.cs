using AutoMapper;
using BlackJack.Entities.Entities;
using BlackJack.ViewModels.GameHistory;
using BlackJack.ViewModels.Round;
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
                config.CreateMap<HistoryMessage, HistoryMessageGetGameHistoryViewItem>();
                                
                config.CreateMap<GamePlayer, GamePlayerStartRoundViewItem>()
                    .ForMember("Cards", opt => opt.MapFrom(c => GetCardsStringList(c.PlayerCards)))
                    .ForMember("Name", opt => opt.MapFrom(c => c.Player.Name));

                config.CreateMap<StartRoundView, RestoreRoundView>();
                config.CreateMap<StartRoundView, TakeCardRoundView>();
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
