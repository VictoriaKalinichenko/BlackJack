import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
    constructor(private _httpClient: HttpClient) { }
    
    GetAuthorizedPlayer(userName: string) {
        const options = userName ?
            { params: new HttpParams().set('userName', userName.toString()) } : {};

        return this._httpClient.get('Start/AuthorizePlayer', options);
    }

    CreateGame(playerId: number, amountOfBots: number) {
        const body = { PlayerId: playerId, AmountOfBots: amountOfBots };
        return this._httpClient.post('Start/CreateGame', body);
    }

    ResumeGame(playerId: number) {
        const options = playerId ?
            { params: new HttpParams().set('playerId', playerId.toString()) } : {};

        return this._httpClient.get('Start/ResumeGame', options);
    }

    GetGame(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this._httpClient.get('Start/InitRound', options);
    }

    StartRound(gameId: number, humanGamePlayerId: number, bet: number) {
        const body = { GameId: gameId, Bet: bet, GamePlayerId: humanGamePlayerId };
        return this._httpClient.post('Game/StartRound', body);
    }

    ContinueRound(gameId: number, continueRound: boolean) {
        const body = { GameId: gameId, ContinueRound: continueRound };
        return this._httpClient.post('Game/ContinueRound', body);
    }

    AddCard(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this._httpClient.get('Game/AddCard', options);
    }

    ResumeAfterStartRound(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this._httpClient.get('Game/ResumeAfterStartRound', options);
    }

    ResumeAfterContinueRound(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this._httpClient.get('Game/ResumeAfterContinueRound', options);
    }

    EndRound(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this._httpClient.get('Game/EndRound', options);
    }

    EndGame(gameId: number, gameResult: string) {
        const body = { GameId: gameId, Result: gameResult };
        return this._httpClient.post('Game/EndGame', body);
    }
}
