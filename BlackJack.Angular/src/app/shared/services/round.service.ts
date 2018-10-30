import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()
export class RoundService {
    constructor(private httpClient: HttpClient) { }
    
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