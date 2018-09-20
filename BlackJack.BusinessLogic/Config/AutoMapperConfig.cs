using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using BlackJack.Entities.Entities;
using BlackJack.BusinessLogic.Helpers;
using BlackJack.ViewModels.ViewModels;

namespace BlackJack.BusinessLogic.Config
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(config =>
            {
                config.CreateMap<GamePlayer, GamePlayerGameLogicDoRoundFirstPhaseResponseItem>()
                    .ForMember("Cards", opt => opt.MapFrom(c => GetCardsStringList(c.PlayerCards)))
                    .ForMember("Name", opt => opt.MapFrom(c => c.Player.Name));

                config.CreateMap<GamePlayer, GamePlayerGameLogicDoRoundSecondPhaseResponseItem>()
                    .ForMember("Cards", opt => opt.MapFrom(c => GetCardsStringList(c.PlayerCards)))
                    .ForMember("Name", opt => opt.MapFrom(c => c.Player.Name));

                config.CreateMap<GamePlayer, GamePlayerGameLogicResumeGameAfterRoundFirstPhaseItem>()
                    .ForMember("Cards", opt => opt.MapFrom(c => GetCardsStringList(c.PlayerCards)))
                    .ForMember("Name", opt => opt.MapFrom(c => c.Player.Name));

                config.CreateMap<GamePlayer, GamePlayerGameLogicResumeGameAfterRoundSecondPhaseItem>()
                    .ForMember("Cards", opt => opt.MapFrom(c => GetCardsStringList(c.PlayerCards)))
                    .ForMember("Name", opt => opt.MapFrom(c => c.Player.Name));

                config.CreateMap<GamePlayer, GameLogicAddOneMoreCardToHumanView>()
                    .ForMember("Cards", opt => opt.MapFrom(c => GetCardsStringList(c.PlayerCards)));

                config.CreateMap<Game, StartGameStartRoundView>();
                config.CreateMap<Log, LogGetLogsView>();

                config.CreateMap<GamePlayer, PlayerStartGameStartRoundItem>()
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
