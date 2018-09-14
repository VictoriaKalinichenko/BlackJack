import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
    constructor(private http: HttpClient) { }
    
    GetAuthorizedPlayer(userName: string) {
        const body = { UserName: userName };
        return this.http.post('http://localhost:55953/StartGame/GetAuthorizedPlayer', body);
    }

    CreateNewGame(playerId: number, amountOfBots: number) {
        const body = { PlayerId: playerId, AmountOfBots: amountOfBots };
        return this.http.post('http://localhost:55953/StartGame/CreateNewGame', body);
    }

    ResumeGame(playerId: number) {
        return this.http.get('http://localhost:55953/StartGame/ResumeGame?playerId=' + playerId);
    }

    GetGame(gameId: number) {
        return this.http.get('http://localhost:55953/StartGame/GetGame?gameId=' + gameId);
    }

    GetGamePlayer(gamePlayerId: number) {
        return this.http.get('http://localhost:55953/PlayerLogic/GetPlayer?gamePlayerId=' + gamePlayerId);
    }

    GetDealerFirstPhase(gamePlayerId: number) {
        return this.http.get('http://localhost:55953/PlayerLogic/GetDealerInFirstPhase?gamePlayerId=' + gamePlayerId);
    }

    GetDealerSecondPhase(gamePlayerId: number) {
        return this.http.get('http://localhost:55953/PlayerLogic/GetDealerInSecondPhase?gamePlayerId=' + gamePlayerId);
    }

    BetsCreation(gameId: number, humanGamePlayerId: number, bet: number) {
        const body = { InGameId: gameId, Bet: bet, HumanGamePlayerId: humanGamePlayerId };
        return this.http.post('http://localhost:55953/PlayerLogic/BetsCreation', body);
    }

    RoundStart(gameId: number) {
        return this.http.get('http://localhost:55953/GameLogic/RoundStart?inGameId=' + gameId);
    }

    FirstPhaseGamePlay(gameId: number) {
        return this.http.get('http://localhost:55953/GameLogic/FirstPhaseGamePlay?inGameId=' + gameId);
    }

    SecondPhase(gameId: number) {
        return this.http.get('http://localhost:55953/GameLogic/SecondPhase?inGameId=' + gameId);
    }

    BlackJackDangerContinueRound(gameId: number) {
        return this.http.get('http://localhost:55953/GameLogic/BlackJackDangerContinueRound?inGameId=' + gameId);
    }

    AddOneMoreCardToHuman(gameId: number) {
        return this.http.get('http://localhost:55953/GameLogic/AddOneMoreCardToHuman?inGameId=' + gameId);
    }

    HumanRoundResult(gameId: number) {
        return this.http.get('http://localhost:55953/PlayerLogic/HumanRoundResult?inGameId=' + gameId);
    }

    UpdateGamePlayersForNewRound(gameId: number) {
        return this.http.get('http://localhost:55953/PlayerLogic/UpdateGamePlayersForNewRound?inGameId=' + gameId);
    }
}
