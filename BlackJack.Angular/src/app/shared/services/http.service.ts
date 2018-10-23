import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
    constructor(private httpClient: HttpClient) { }
    
    getAuthorizedPlayer(userName: string) {
        const options = userName ?
            { params: new HttpParams().set('userName', userName.toString()) } : {};

        return this.httpClient.get('Start/AuthorizePlayer', options);
    }

    createGame(playerId: number, amountOfBots: number) {
        const body = { PlayerId: playerId, AmountOfBots: amountOfBots };
        return this.httpClient.post('Start/CreateGame', body);
    }

    resumeGame(playerId: number) {
        const options = playerId ?
            { params: new HttpParams().set('playerId', playerId.toString()) } : {};

        return this.httpClient.get('Start/ResumeGame', options);
    }

    getGame(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this.httpClient.get('Start/InitializeRound', options);
    }

    startRound(gameId: number, humanGamePlayerId: number, bet: number) {
        const body = { GameId: gameId, Bet: bet, GamePlayerId: humanGamePlayerId };
        return this.httpClient.post('Round/Start', body);
    }

    continueRound(gameId: number, continueRound: boolean) {
        const body = { GameId: gameId, ContinueRound: continueRound };
        return this.httpClient.post('Round/Continue', body);
    }

    addCard(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this.httpClient.get('Round/AddCard', options);
    }

    resumeAfterStartRound(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this.httpClient.get('Round/ResumeAfterStart', options);
    }

    resumeAfterContinueRound(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this.httpClient.get('Round/ResumeAfterContinue', options);
    }

    endRound(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this.httpClient.get('Round/End', options);
    }

    endGame(gameId: number, gameResult: string) {
        const body = { GameId: gameId, Result: gameResult };
        return this.httpClient.post('Round/EndGame', body);
    }
}
