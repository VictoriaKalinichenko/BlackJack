import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
    constructor(private http: HttpClient) { }
    
    GetAuthorizedPlayer(userName: string) {
        const body = { UserName: userName };
        return this.http.post('StartGame/GetAuthorizedPlayer', body);
    }

    CreateNewGame(playerId: number, amountOfBots: number) {
        const body = { PlayerId: playerId, AmountOfBots: amountOfBots };
        return this.http.post('StartGame/CreateNewGame', body);
    }

    ResumeGame(playerId: number) {
        return this.http.get('StartGame/ResumeGame?playerId=' + playerId);
    }

    GetGame(gameId: number) {
        return this.http.get('StartGame/GetGame?gameId=' + gameId);
    }

    GetGamePlayer(gamePlayerId: number) {
        return this.http.get('PlayerLogic/GetPlayer?gamePlayerId=' + gamePlayerId);
    }

    GetDealerFirstPhase(gamePlayerId: number) {
        return this.http.get('PlayerLogic/GetDealerInFirstPhase?gamePlayerId=' + gamePlayerId);
    }

    GetDealerSecondPhase(gamePlayerId: number) {
        return this.http.get('PlayerLogic/GetDealerInSecondPhase?gamePlayerId=' + gamePlayerId);
    }

    BetsCreation(gameId: number, humanGamePlayerId: number, bet: number) {
        const body = { InGameId: gameId, Bet: bet, HumanGamePlayerId: humanGamePlayerId };
        return this.http.post('PlayerLogic/BetsCreation', body);
    }

    RoundStart(gameId: number) {
        return this.http.get('GameLogic/RoundStart?inGameId=' + gameId);
    }

    FirstPhaseGamePlay(gameId: number) {
        return this.http.get('GameLogic/FirstPhaseGamePlay?inGameId=' + gameId);
    }

    SecondPhase(gameId: number) {
        return this.http.get('GameLogic/SecondPhase?inGameId=' + gameId);
    }

    BlackJackDangerContinueRound(gameId: number) {
        return this.http.get('GameLogic/BlackJackDangerContinueRound?inGameId=' + gameId);
    }

    AddOneMoreCardToHuman(gameId: number) {
        return this.http.get('GameLogic/AddOneMoreCardToHuman?inGameId=' + gameId);
    }

    HumanRoundResult(gameId: number) {
        return this.http.get('PlayerLogic/HumanRoundResult?inGameId=' + gameId);
    }

    UpdateGamePlayersForNewRound(gameId: number) {
        return this.http.get('PlayerLogic/UpdateGamePlayersForNewRound?inGameId=' + gameId);
    }
}
