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
import { HttpClient } from '@angular/common/http';
var HttpService = /** @class */ (function () {
    function HttpService(http) {
        this.http = http;
    }
    HttpService.prototype.GetAuthorizedPlayer = function (userName) {
        var body = { UserName: userName };
        return this.http.post('http://localhost:55953/StartGame/GetAuthorizedPlayer', body);
    };
    HttpService.prototype.CreateNewGame = function (playerId, amountOfBots) {
        var body = { PlayerId: playerId, AmountOfBots: amountOfBots };
        return this.http.post('http://localhost:55953/StartGame/CreateNewGame', body);
    };
    HttpService.prototype.ResumeGame = function (playerId) {
        return this.http.get('http://localhost:55953/StartGame/ResumeGame?playerId=' + playerId);
    };
    HttpService.prototype.GetGame = function (gameId) {
        return this.http.get('http://localhost:55953/StartGame/GetGame?gameId=' + gameId);
    };
    HttpService.prototype.GetGamePlayer = function (gamePlayerId) {
        return this.http.get('http://localhost:55953/PlayerLogic/GetPlayer?gamePlayerId=' + gamePlayerId);
    };
    HttpService.prototype.GetDealerFirstPhase = function (gamePlayerId) {
        return this.http.get('http://localhost:55953/PlayerLogic/GetDealerInFirstPhase?gamePlayerId=' + gamePlayerId);
    };
    HttpService.prototype.GetDealerSecondPhase = function (gamePlayerId) {
        return this.http.get('http://localhost:55953/PlayerLogic/GetDealerInSecondPhase?gamePlayerId=' + gamePlayerId);
    };
    HttpService.prototype.BetsCreation = function (gameId, humanGamePlayerId, bet) {
        var body = { InGameId: gameId, Bet: bet, HumanGamePlayerId: humanGamePlayerId };
        return this.http.post('http://localhost:55953/PlayerLogic/BetsCreation', body);
    };
    HttpService.prototype.RoundStart = function (gameId) {
        return this.http.get('http://localhost:55953/GameLogic/RoundStart?inGameId=' + gameId);
    };
    HttpService.prototype.FirstPhaseGamePlay = function (gameId) {
        return this.http.get('http://localhost:55953/GameLogic/FirstPhaseGamePlay?inGameId=' + gameId);
    };
    HttpService.prototype.SecondPhase = function (gameId) {
        return this.http.get('http://localhost:55953/GameLogic/SecondPhase?inGameId=' + gameId);
    };
    HttpService.prototype.BlackJackDangerContinueRound = function (gameId) {
        return this.http.get('http://localhost:55953/GameLogic/BlackJackDangerContinueRound?inGameId=' + gameId);
    };
    HttpService.prototype.AddOneMoreCardToHuman = function (gameId) {
        return this.http.get('http://localhost:55953/GameLogic/AddOneMoreCardToHuman?inGameId=' + gameId);
    };
    HttpService.prototype.HumanRoundResult = function (gameId) {
        return this.http.get('http://localhost:55953/PlayerLogic/HumanRoundResult?inGameId=' + gameId);
    };
    HttpService.prototype.UpdateGamePlayersForNewRound = function (gameId) {
        return this.http.get('http://localhost:55953/PlayerLogic/UpdateGamePlayersForNewRound?inGameId=' + gameId);
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