var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
var HttpService = /** @class */ (function () {
    function HttpService(http) {
        this.http = http;
    }
    HttpService.prototype.GetAuthorizedPlayer = function (userName) {
        var options = userName ?
            { params: new HttpParams().set('userName', userName.toString()) } : {};
        return this.http.get('StartGame/AuthorizePlayer', options);
    };
    HttpService.prototype.CreateNewGame = function (playerId, amountOfBots) {
        var body = { PlayerId: playerId, AmountOfBots: amountOfBots };
        return this.http.post('StartGame/CreateNewGame', body);
    };
    HttpService.prototype.ResumeGame = function (playerId) {
        var options = playerId ?
            { params: new HttpParams().set('playerId', playerId.toString()) } : {};
        return this.http.get('StartGame/ResumeGame', options);
    };
    HttpService.prototype.GetGame = function (gameId) {
        var options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};
        return this.http.get('StartGame/StartRound', options);
    };
    HttpService.prototype.DoRoundFirstPhase = function (gameId, humanGamePlayerId, bet) {
        var body = { GameId: gameId, Bet: bet, GamePlayerId: humanGamePlayerId };
        return this.http.post('GameLogic/DoRoundFirstPhase', body);
    };
    HttpService.prototype.DoRoundSecondPhase = function (gameId, humanBlackJackContinueRound) {
        var body = { GameId: gameId, HumanBlackJackAndDealerBlackJackDanger: humanBlackJackContinueRound };
        return this.http.post('GameLogic/DoRoundSecondPhase', body);
    };
    HttpService.prototype.AddCardToHuman = function (gameId) {
        var options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};
        return this.http.get('GameLogic/AddCardToHuman', options);
    };
    HttpService.prototype.ResumeGameAfterRoundFirstPhase = function (gameId) {
        var options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};
        return this.http.get('GameLogic/ResumeGameAfterRoundFirstPhase', options);
    };
    HttpService.prototype.ResumeGameAfterRoundSecondPhase = function (gameId) {
        var options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};
        return this.http.get('GameLogic/ResumeGameAfterRoundSecondPhase', options);
    };
    HttpService.prototype.EndRound = function (gameId) {
        var options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};
        return this.http.get('GameLogic/EndRound', options);
    };
    HttpService.prototype.EndGame = function (gameId, gameResult) {
        var body = { GameId: gameId, GameResult: gameResult };
        return this.http.post('GameLogic/EndGame', body);
    };
    HttpService = __decorate([
        Injectable({
            providedIn: 'root'
        }),
        __metadata("design:paramtypes", [HttpClient])
    ], HttpService);
    return HttpService;
}());
export { HttpService };
//# sourceMappingURL=http.service.js.map