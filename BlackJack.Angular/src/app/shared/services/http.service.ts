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

        return this._httpClient.get('StartGame/AuthorizePlayer', options);
    }

    CreateNewGame(playerId: number, amountOfBots: number) {
        const body = { PlayerId: playerId, AmountOfBots: amountOfBots };
        return this._httpClient.post('StartGame/CreateNewGame', body);
    }

    ResumeGame(playerId: number) {
        const options = playerId ?
            { params: new HttpParams().set('playerId', playerId.toString()) } : {};

        return this._httpClient.get('StartGame/ResumeGame', options);
    }

    GetGame(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this._httpClient.get('StartGame/StartRound', options);
    }

    DoRoundFirstPhase(gameId: number, humanGamePlayerId: number, bet: number) {
        const body = { GameId: gameId, Bet: bet, GamePlayerId: humanGamePlayerId };
        return this._httpClient.post('GameLogic/DoRoundFirstPhase', body);
    }

    DoRoundSecondPhase(gameId: number, humanBlackJackContinueRound: boolean) {
        const body = { GameId: gameId, HumanBlackJackAndDealerBlackJackDanger: humanBlackJackContinueRound };
        return this._httpClient.post('GameLogic/DoRoundSecondPhase', body);
    }

    AddCardToHuman(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this._httpClient.get('GameLogic/AddCardToHuman', options);
    }

    ResumeGameAfterRoundFirstPhase(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this._httpClient.get('GameLogic/ResumeGameAfterRoundFirstPhase', options);
    }

    ResumeGameAfterRoundSecondPhase(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this._httpClient.get('GameLogic/ResumeGameAfterRoundSecondPhase', options);
    }

    EndRound(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this._httpClient.get('GameLogic/EndRound', options);
    }

    EndGame(gameId: number, gameResult: string) {
        const body = { GameId: gameId, GameResult: gameResult };
        return this._httpClient.post('GameLogic/EndGame', body);
    }
}
