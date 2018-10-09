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
    function HttpService(_httpClient) {
        this._httpClient = _httpClient;
    }
    HttpService.prototype.GetAuthorizedPlayer = function (userName) {
        var options = userName ?
            { params: new HttpParams().set('userName', userName.toString()) } : {};
        return this._httpClient.get('Start/AuthorizePlayer', options);
    };
    HttpService.prototype.CreateGame = function (playerId, amountOfBots) {
        var body = { PlayerId: playerId, AmountOfBots: amountOfBots };
        return this._httpClient.post('Start/CreateGame', body);
    };
    HttpService.prototype.ResumeGame = function (playerId) {
        var options = playerId ?
            { params: new HttpParams().set('playerId', playerId.toString()) } : {};
        return this._httpClient.get('Start/ResumeGame', options);
    };
    HttpService.prototype.GetGame = function (gameId) {
        var options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};
        return this._httpClient.get('Start/InitRound', options);
    };
    HttpService.prototype.StartRound = function (gameId, humanGamePlayerId, bet) {
        var body = { GameId: gameId, Bet: bet, GamePlayerId: humanGamePlayerId };
        return this._httpClient.post('Game/StartRound', body);
    };
    HttpService.prototype.ContinueRound = function (gameId, continueRound) {
        var body = { GameId: gameId, ContinueRound: continueRound };
        return this._httpClient.post('Game/ContinueRound', body);
    };
    HttpService.prototype.AddCard = function (gameId) {
        var options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};
        return this._httpClient.get('Game/AddCard', options);
    };
    HttpService.prototype.ResumeAfterStartRound = function (gameId) {
        var options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};
        return this._httpClient.get('Game/ResumeAfterStartRound', options);
    };
    HttpService.prototype.ResumeAfterContinueRound = function (gameId) {
        var options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};
        return this._httpClient.get('Game/ResumeAfterContinueRound', options);
    };
    HttpService.prototype.EndRound = function (gameId) {
        var options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};
        return this._httpClient.get('Game/EndRound', options);
    };
    HttpService.prototype.EndGame = function (gameId, gameResult) {
        var body = { GameId: gameId, Result: gameResult };
        return this._httpClient.post('Game/EndGame', body);
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