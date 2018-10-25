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
import { AuthorizedUserModule } from 'app/authorized-user-module/authorized-user.module';
var StartService = /** @class */ (function () {
    function StartService(httpClient) {
        this.httpClient = httpClient;
    }
    StartService.prototype.getAuthorizedPlayer = function (userName) {
        var options = userName ?
            { params: new HttpParams().set('userName', userName.toString()) } : {};
        return this.httpClient.get('Start/AuthorizePlayer', options);
    };
    StartService.prototype.createGame = function (playerId, amountOfBots) {
        var body = { PlayerId: playerId, AmountOfBots: amountOfBots };
        return this.httpClient.post('Start/CreateGame', body);
    };
    StartService.prototype.resumeGame = function (playerId) {
        var options = playerId ?
            { params: new HttpParams().set('playerId', playerId.toString()) } : {};
        return this.httpClient.get('Start/ResumeGame', options);
    };
    StartService.prototype.getGame = function (gameId) {
        var options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};
        return this.httpClient.get('Start/InitializeRound', options);
    };
    StartService = __decorate([
        Injectable({
            providedIn: AuthorizedUserModule
        }),
        __metadata("design:paramtypes", [HttpClient])
    ], StartService);
    return StartService;
}());
export { StartService };
//# sourceMappingURL=start.service.js.map