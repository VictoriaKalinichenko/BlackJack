var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { GameModule } from 'app/game-module/game.module';
var RoundService = /** @class */ (function () {
    function RoundService(httpClient) {
        this.httpClient = httpClient;
    }
    RoundService.prototype.startRound = function (gameId, humanGamePlayerId, bet) {
        var body = { GameId: gameId, Bet: bet, GamePlayerId: humanGamePlayerId };
        return this.httpClient.post('Round/Start', body);
    };
    RoundService.prototype.continueRound = function (gameId, continueRound) {
        var body = { GameId: gameId, ContinueRound: continueRound };
        return this.httpClient.post('Round/Continue', body);
    };
    RoundService.prototype.addCard = function (gameId) {
        var options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};
        return this.httpClient.get('Round/AddCard', options);
    };
    RoundService.prototype.resumeAfterStartRound = function (gameId) {
        var options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};
        return this.httpClient.get('Round/ResumeAfterStart', options);
    };
    RoundService.prototype.resumeAfterContinueRound = function (gameId) {
        var options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};
        return this.httpClient.get('Round/ResumeAfterContinue', options);
    };
    RoundService.prototype.endRound = function (gameId) {
        var options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};
        return this.httpClient.get('Round/End', options);
    };
    RoundService.prototype.endGame = function (gameId, gameResult) {
        var body = { GameId: gameId, Result: gameResult };
        return this.httpClient.post('Round/EndGame', body);
    };
    RoundService = __decorate([
        Injectable({
            providedIn: GameModule
        }),
        __metadata("design:paramtypes", [HttpClient])
    ], RoundService);
    return RoundService;
}());
export { RoundService };
//# sourceMappingURL=round.service.js.map