import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class StartService {
    constructor(private httpClient: HttpClient) { }

    searchGameForPlayer(userName: string) {
        const options = userName ?
            { params: new HttpParams().set('userName', userName.toString()) } : {};

        return this.httpClient.get('Start/Index', options);
    }

    createGame(userName: string, amountOfBots: number) {
        const body = { UserName: userName, AmountOfBots: amountOfBots };
        return this.httpClient.post('Start/CreateGame', body);
    }

    initializeRound(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this.httpClient.get('Start/Initialize', options);
    }
}