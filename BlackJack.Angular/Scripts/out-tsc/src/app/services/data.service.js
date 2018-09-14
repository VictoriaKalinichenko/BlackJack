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
var DataService = /** @class */ (function () {
    function DataService(http) {
        this.http = http;
    }
    DataService.prototype.SetUserName = function (userName) {
        this.UserName = userName;
    };
    DataService.prototype.GetUserName = function () {
        return this.UserName;
    };
    DataService.prototype.GetAuthorizedPlayer = function () {
        var body = { UserName: this.UserName };
        return this.http.post('http://localhost:55953/StartGame/GetAuthorizedPlayer', body);
    };
    DataService.prototype.CreateNewGame = function (playerId, amountOfBots) {
        var body = { PlayerId: playerId, AmountOfBots: amountOfBots };
        return this.http.post('http://localhost:55953/StartGame/CreateNewGame', body);
    };
    DataService.prototype.ResumeGame = function (playerId) {
        return this.http.get('http://localhost:55953/StartGame/ResumeGame?playerId=' + playerId);
    };
    DataService.prototype.GetGame = function (gameId) {
        return this.http.get('http://localhost:55953/StartGame/GetGame?gameId=' + gameId);
    };
    DataService.prototype.GetGamePlayer = function (gamePlayerId) {
        return this.http.get('http://localhost:55953/PlayerLogic/GetPlayer?gamePlayerId=' + gamePlayerId);
    };
    DataService.prototype.GetDealerFirstPhase = function (gamePlayerId) {
        return this.http.get('http://localhost:55953/PlayerLogic/GetDealerInFirstPhase?gamePlayerId=' + gamePlayerId);
    };
    DataService.prototype.GetDealerSecondPhase = function (gamePlayerId) {
        return this.http.get('http://localhost:55953/PlayerLogic/GetDealerInSecondPhase?gamePlayerId=' + gamePlayerId);
    };
    DataService.prototype.BetsCreation = function (gameId, humanGamePlayerId, bet) {
        var body = { InGameId: gameId, Bet: bet, HumanGamePlayerId: humanGamePlayerId };
        return this.http.post('http://localhost:55953/PlayerLogic/BetsCreation', body);
    };
    DataService.prototype.RoundStart = function (gameId) {
        return this.http.get('http://localhost:55953/GameLogic/RoundStart?inGameId=' + gameId);
    };
    DataService.prototype.FirstPhaseGamePlay = function (gameId) {
        return this.http.get('http://localhost:55953/GameLogic/FirstPhaseGamePlay?inGameId=' + gameId);
    };
    DataService.prototype.SecondPhase = function (gameId) {
        return this.http.get('http://localhost:55953/GameLogic/SecondPhase?inGameId=' + gameId);
    };
    DataService.prototype.BlackJackDangerContinueRound = function (gameId) {
        return this.http.get('http://localhost:55953/GameLogic/HumanBjAndDealerBjDangerContinueRound?inGameId=' + gameId);
    };
    DataService.prototype.AddOneMoreCardToHuman = function (gameId) {
        return this.http.get('http://localhost:55953/GameLogic/AddOneMoreCardToHuman?inGameId=' + gameId);
    };
    DataService.prototype.HumanRoundResult = function (gameId) {
        return this.http.get('http://localhost:55953/PlayerLogic/HumanRoundResult?inGameId=' + gameId);
    };
    DataService.prototype.UpdateGamePlayersForNewRound = function (gameId) {
        return this.http.get('http://localhost:55953/PlayerLogic/UpdateGamePlayersForNewRound?inGameId=' + gameId);
    };
    DataService = __decorate([
        Injectable({
            providedIn: 'root'
        }),
        __metadata("design:paramtypes", [HttpClient])
    ], DataService);
    return DataService;
}());
export { DataService };
//# sourceMappingURL=data.service.js.map