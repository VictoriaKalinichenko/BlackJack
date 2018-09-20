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

        return this.http.get('StartGame/AuthorizePlayer', options);
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

        return this.http.get('StartGame/StartRound', options);
    }

    DoRoundFirstPhase(gameId: number, humanGamePlayerId: number, bet: number) {
        const body = { GameId: gameId, Bet: bet, GamePlayerId: humanGamePlayerId };
        return this.http.post('GameLogic/DoRoundFirstPhase', body);
    }

    DoRoundSecondPhase(gameId: number, humanBlackJackContinueRound: boolean) {
        const body = { GameId: gameId, HumanBlackJackAndDealerBlackJackDanger: humanBlackJackContinueRound };
        return this.http.post('GameLogic/DoRoundSecondPhase', body);
    }

    AddOneMoreCardToHuman(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this.http.get('GameLogic/AddOneMoreCardToHuman', options);
    }

    ResumeGameAfterRoundFirstPhase(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this.http.get('GameLogic/ResumeGameAfterRoundFirstPhase', options);
    }

    ResumeGameAfterRoundSecondPhase(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this.http.get('GameLogic/ResumeGameAfterRoundSecondPhase', options);
    }

    EndRound(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this.http.get('GameLogic/EndRound', options);
    }

    EndGame(gameId: number, gameResult: string) {
        const body = { GameId: gameId, GameResult: gameResult };
        return this.http.post('GameLogic/EndGame', body);
    }
}
