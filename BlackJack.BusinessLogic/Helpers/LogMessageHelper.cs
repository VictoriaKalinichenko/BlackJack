namespace BlackJack.BusinessLogic.Helpers
{
    static class LogMessageHelper
    {
        public static string BetCreated(string playerType, int playerId, string playerName, int playerScore, int playerBet)
        {
            string message = $"{playerType}(Id={playerId}, Name={playerName}, Score={playerScore}) created the bet(={playerBet})";
            return message;
        }

        public static string GameCreated(int gameId, int gameStage)
        {
            string message = $"Game(Id={gameId}, Stage={gameStage}) is created";
            return message;
        }

        public static string PlayerAddedToGame(string playerType, int playerId, string playerName, int playerScore)
        {
            string message = $"{playerType}(Id={playerId}, Name={playerName}, Score={playerScore}) is added to game";
            return message;
        }

        public static string GameStageChanged(int gameStage)
        {
            string message = $"Stage is changed (Stage={gameStage})";
            return message;
        }

        public static string NewRoundStarted()
        {
            string message = "New round is started";
            return message;
        }

        public static string DealerBlackJackDanger(int dealerId, string dealerName, int dealerFirstCardId, int dealerFirstCardValue, string dealerFirstCardName)
        {
            string message = $"Dealer(Id={dealerId}, Name={dealerName}) has BlackJackDanger. His first card is (Id={dealerFirstCardId}, Value={dealerFirstCardValue}, Name={dealerFirstCardName})";
            return message;
        }

        public static string PlayerBlackJackResult(string playerType, int playerId, string playerName, int playerRoundScore, float betPayCoef)
        {
            string message = $"{playerType}(Id={playerId}, Name={playerName}) has Blackjack(RoundScore={playerRoundScore}). BetPayCoefficient is changed(={betPayCoef})";
            return message;
        }

        public static string PlayerWinResult(string playerType, int playerId, string playerName, int playerRoundScore, float betPayCoefficient, int dealerRoundScore)
        {
            string message = $"{playerType}(Id={playerId}, Name={playerName}) has win result(PlayerRoundScore={playerRoundScore}, DealerRoundScore={dealerRoundScore}). BetPayCoefficient is changed(={betPayCoefficient})";
            return message;
        }

        public static string PlayerEqualResult(string playerType, int playerId, string playerName, int playerRoundScore, float betPayCoefficient, int dealerRoundScore)
        {
            string message = $"{playerType}(Id={playerId}, Name={playerName}) has equal result(PlayerRoundScore={playerRoundScore}, DealerRoundScore={dealerRoundScore}). BetPayCoefficient is changed(={betPayCoefficient})";
            return message;
        }

        public static string PlayerLoseResult(string playerType, int playerId, string playerName, int playerRoundScore, float betPayCoefficient, int dealerRoundScore)
        {
            string message = $"{playerType}(Id={playerId}, Name={playerName}) has lose result(PlayerRoundScore={playerRoundScore}, DealerRoundScore={dealerRoundScore}). BetPayCoefficient is changed(={betPayCoefficient})";
            return message;
        }

        public static string PlayerBlackJackAndDealerBlackJackDanger(string playerType, int playerId, string playerName, int playerRoundScore, float betPayCoef)
        {
            string message = $"{playerType}(Id={playerId}, Name={playerName}) has Blackjack(RoundScore={playerRoundScore}) with DealerBlackJackDanger. BetPayCoefficient is changed(={betPayCoef})";
            return message;
        }

        public static string CardAdded(int cardId, int cardValue, string cardName, string playerType, int playerId, string playerName)
        {
            string message = $"Card(Id={cardId}, Value={cardValue}, Name={cardName}) is added to {playerType}(Id={playerId}, Name={playerName})";
            return message;
        }
    }
}
