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
import { RequestStartRoundView } from '../models/request-start-round.view';
var RoundService = /** @class */ (function () {
    function RoundService(httpClient) {
        this.httpClient = httpClient;
    }
    RoundService.prototype.startRound = function (gameId, isNewRound) {
        var request = new RequestStartRoundView();
        request.gameId = gameId;
        request.isNewRound = isNewRound;
        return this.httpClient.post('Round/Start', request);
    };
    RoundService.prototype.takeCard = function (gameId) {
        var options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};
        return this.httpClient.get('Round/TakeCard', options);
    };
    RoundService.prototype.endRound = function (gameId) {
        var options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};
        return this.httpClient.get('Round/End', options);
    };
    RoundService = __decorate([
        Injectable(),
        __metadata("design:paramtypes", [HttpClient])
    ], RoundService);
    return RoundService;
}());
export { RoundService };
//# sourceMappingURL=round.service.js.map