import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthorizedUserModule } from 'app/authorized-user-module/authorized-user.module';

@Injectable({
    providedIn: AuthorizedUserModule
})
export class StartService {
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

        return this.httpClient.get('Start/Initialize', options);
    }
}