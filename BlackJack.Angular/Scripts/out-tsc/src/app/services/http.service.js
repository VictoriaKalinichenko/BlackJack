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
        var body = { UserName: userName };
        return this.http.post('StartGame/AuthorizedPlayer', body);
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
        return this.http.get('StartGame/GetGame', options);
    };
    HttpService.prototype.GetGamePlayer = function (gamePlayerId) {
        var options = gamePlayerId ?
            { params: new HttpParams().set('gamePlayerId', gamePlayerId.toString()) } : {};
        return this.http.get('PlayerLogic/GetPlayer', options);
    };
    HttpService.prototype.GetDealerFirstPhase = function (gamePlayerId) {
        var options = gamePlayerId ?
            { params: new HttpParams().set('gamePlayerId', gamePlayerId.toString()) } : {};
        return this.http.get('PlayerLogic/GetDealerInFirstPhase', options);
    };
    HttpService.prototype.GetDealerSecondPhase = function (gamePlayerId) {
        var options = gamePlayerId ?
            { params: new HttpParams().set('gamePlayerId', gamePlayerId.toString()) } : {};
        return this.http.get('PlayerLogic/GetDealerInSecondPhase', options);
    };
    HttpService.prototype.BetsCreation = function (gameId, humanGamePlayerId, bet) {
        var body = { InGameId: gameId, Bet: bet, HumanGamePlayerId: humanGamePlayerId };
        return this.http.post('PlayerLogic/BetsCreation', body);
    };
    HttpService.prototype.RoundStart = function (gameId) {
        var options = gameId ?
            { params: new HttpParams().set('inGameId', gameId.toString()) } : {};
        return this.http.get('GameLogic/RoundStart', options);
    };
    HttpService.prototype.FirstPhaseGamePlay = function (gameId) {
        var options = gameId ?
            { params: new HttpParams().set('inGameId', gameId.toString()) } : {};
        return this.http.get('GameLogic/FirstPhaseGamePlay', options);
    };
    HttpService.prototype.SecondPhase = function (gameId) {
        var options = gameId ?
            { params: new HttpParams().set('inGameId', gameId.toString()) } : {};
        return this.http.get('GameLogic/SecondPhase', options);
    };
    HttpService.prototype.BlackJackDangerContinueRound = function (gameId) {
        var options = gameId ?
            { params: new HttpParams().set('inGameId', gameId.toString()) } : {};
        return this.http.get('GameLogic/BlackJackDangerContinueRound', options);
    };
    HttpService.prototype.AddOneMoreCardToHuman = function (gameId) {
        var options = gameId ?
            { params: new HttpParams().set('inGameId', gameId.toString()) } : {};
        return this.http.get('GameLogic/AddOneMoreCardToHuman', options);
    };
    HttpService.prototype.HumanRoundResult = function (gameId) {
        var options = gameId ?
            { params: new HttpParams().set('inGameId', gameId.toString()) } : {};
        return this.http.get('PlayerLogic/HumanRoundResult', options);
    };
    HttpService.prototype.UpdateGamePlayersForNewRound = function (gameId) {
        var options = gameId ?
            { params: new HttpParams().set('inGameId', gameId.toString()) } : {};
        return this.http.get('PlayerLogic/UpdateGamePlayersForNewRound', options);
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