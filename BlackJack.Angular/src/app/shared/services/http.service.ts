import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
    constructor(private httpClient: HttpClient) { }
    
    GetAuthorizedPlayer(userName: string) {
        const options = userName ?
            { params: new HttpParams().set('userName', userName.toString()) } : {};

        return this.httpClient.get('Start/AuthorizePlayer', options);
    }

    CreateGame(playerId: number, amountOfBots: number) {
        const body = { PlayerId: playerId, AmountOfBots: amountOfBots };
        return this.httpClient.post('Start/CreateGame', body);
    }

    ResumeGame(playerId: number) {
        const options = playerId ?
            { params: new HttpParams().set('playerId', playerId.toString()) } : {};

        return this.httpClient.get('Start/ResumeGame', options);
    }

    GetGame(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this.httpClient.get('Start/InitializeRound', options);
    }

    StartRound(gameId: number, humanGamePlayerId: number, bet: number) {
        const body = { GameId: gameId, Bet: bet, GamePlayerId: humanGamePlayerId };
        return this.httpClient.post('Round/Start', body);
    }

    ContinueRound(gameId: number, continueRound: boolean) {
        const body = { GameId: gameId, ContinueRound: continueRound };
        return this.httpClient.post('Round/Continue', body);
    }

    AddCard(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this.httpClient.get('Round/AddCard', options);
    }

    ResumeAfterStartRound(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this.httpClient.get('Round/ResumeAfterStart', options);
    }

    ResumeAfterContinueRound(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this.httpClient.get('Round/ResumeAfterContinue', options);
    }

    EndRound(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this.httpClient.get('Round/End', options);
    }

    EndGame(gameId: number, gameResult: string) {
        const body = { GameId: gameId, Result: gameResult };
        return this.httpClient.post('Round/EndGame', body);
    }
}
