import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()
export class RoundService {
    constructor(private httpClient: HttpClient) { }
    
    startRound(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this.httpClient.get('Round/Start', options);
    }

    continueRound(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this.httpClient.get('Round/Continue', options);
    }

    addCard(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this.httpClient.get('Round/AddCard', options);
    }

    restoreRound(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this.httpClient.get('Round/Restore', options);
    }
}