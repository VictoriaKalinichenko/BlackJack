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

    CreateNewGame(playerId: number, amountOfBots: number) {
        const body = { PlayerId: playerId, AmountOfBots: amountOfBots };
        return this._httpClient.post('Start/CreateNewGame', body);
    }

    ResumeGame(playerId: number) {
        const options = playerId ?
            { params: new HttpParams().set('playerId', playerId.toString()) } : {};

        return this._httpClient.get('Start/ResumeGame', options);
    }

    GetGame(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this._httpClient.get('Start/BeginRound', options);
    }

    DoRoundFirstPhase(gameId: number, humanGamePlayerId: number, bet: number) {
        const body = { GameId: gameId, Bet: bet, GamePlayerId: humanGamePlayerId };
        return this._httpClient.post('Game/DoRoundFirstPhase', body);
    }

    DoRoundSecondPhase(gameId: number, humanBlackJackContinueRound: boolean) {
        const body = { GameId: gameId, ContinueBlackJackDanger: humanBlackJackContinueRound };
        return this._httpClient.post('Game/DoRoundSecondPhase', body);
    }

    AddCard(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this._httpClient.get('Game/AddCard', options);
    }

    ResumeAfterRoundFirstPhase(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this._httpClient.get('Game/ResumeAfterRoundFirstPhase', options);
    }

    ResumeAfterRoundSecondPhase(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this._httpClient.get('Game/ResumeAfterRoundSecondPhase', options);
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
