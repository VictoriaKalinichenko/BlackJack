import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
    constructor(private http: HttpClient) { }
    
    GetAuthorizedPlayer(userName: string) {
        const options = userName ?
            { params: new HttpParams().set('userName', userName.toString()) } : {};

        return this.http.get('StartGame/AuthorizedPlayer', options);
    }

    CreateNewGame(playerId: number, amountOfBots: number) {
        const body = { PlayerId: playerId, AmountOfBots: amountOfBots };
        return this.http.post('StartGame/CreateNewGame', body);
    }

    ResumeGame(playerId: number) {
        const options = playerId ?
            { params: new HttpParams().set('playerId', playerId.toString()) } : {};

        return this.http.get('StartGame/ResumeGame', options);
    }

    GetGame(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this.http.get('StartGame/Round', options);
    }

    GetGamePlayer(gamePlayerId: number) {
        const options = gamePlayerId ?
            { params: new HttpParams().set('gamePlayerId', gamePlayerId.toString()) } : {};

        return this.http.get('PlayerLogic/GetPlayer', options);
    }

    GetDealerFirstPhase(gamePlayerId: number) {
        const options = gamePlayerId ?
            { params: new HttpParams().set('gamePlayerId', gamePlayerId.toString()) } : {};

        return this.http.get('PlayerLogic/GetDealerInFirstPhase', options);
    }

    GetDealerSecondPhase(gamePlayerId: number) {
        const options = gamePlayerId ?
            { params: new HttpParams().set('gamePlayerId', gamePlayerId.toString()) } : {};

        return this.http.get('PlayerLogic/GetDealerInSecondPhase', options);
    }

    BetsCreation(gameId: number, humanGamePlayerId: number, bet: number) {
        const body = { InGameId: gameId, Bet: bet, HumanGamePlayerId: humanGamePlayerId };
        return this.http.post('PlayerLogic/BetsCreation', body);
    }

    RoundStart(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('inGameId', gameId.toString()) } : {};

        return this.http.get('GameLogic/RoundStart', options);
    }

    FirstPhaseGamePlay(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('inGameId', gameId.toString()) } : {};

        return this.http.get('GameLogic/FirstPhaseGamePlay', options);
    }

    SecondPhase(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('inGameId', gameId.toString()) } : {};

        return this.http.get('GameLogic/SecondPhase', options);
    }

    BlackJackDangerContinueRound(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('inGameId', gameId.toString()) } : {};

        return this.http.get('GameLogic/BlackJackDangerContinueRound', options);
    }

    AddOneMoreCardToHuman(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('inGameId', gameId.toString()) } : {};

        return this.http.get('GameLogic/AddOneMoreCardToHuman', options);
    }

    HumanRoundResult(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('inGameId', gameId.toString()) } : {};

        return this.http.get('PlayerLogic/HumanRoundResult', options);
    }

    UpdateGamePlayersForNewRound(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('inGameId', gameId.toString()) } : {};

        return this.http.get('PlayerLogic/UpdateGamePlayersForNewRound', options);
    }
}
