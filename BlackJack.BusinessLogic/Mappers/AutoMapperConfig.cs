using AutoMapper;
using BlackJack.Entities.Entities;
using BlackJack.ViewModels;
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
                config.CreateMap<Game, InitializeRoundStartView>();
                config.CreateMap<HistoryMessage, HistoryMessageGetGameHistoryViewItem>();

                config.CreateMap<GamePlayer, GamePlayerInitializeRoundStartViewItem>()
                    .ForMember("Name", opt => opt.MapFrom(c => c.Player.Name));
                
                config.CreateMap<GamePlayer, GamePlayerResponseStartRoundViewItem>()
                    .ForMember("Cards", opt => opt.MapFrom(c => GetCardsStringList(c.PlayerCards)))
                    .ForMember("Name", opt => opt.MapFrom(c => c.Player.Name));

                config.CreateMap<GamePlayer, GamePlayerResponseContinueRoundViewItem>()
                    .ForMember("Cards", opt => opt.MapFrom(c => GetCardsStringList(c.PlayerCards)))
                    .ForMember("Name", opt => opt.MapFrom(c => c.Player.Name));

                config.CreateMap<GamePlayer, AddCardRoundView>()
                    .ForMember("Cards", opt => opt.MapFrom(c => GetCardsStringList(c.PlayerCards)))
                    .ForMember("Name", opt => opt.MapFrom(c => c.Player.Name));
                
                config.CreateMap<ResponseStartRoundView, ResumeAfterStartRoundView>();
                config.CreateMap<ResponseContinueRoundView, ResumeAfterContinueRoundView>();
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
