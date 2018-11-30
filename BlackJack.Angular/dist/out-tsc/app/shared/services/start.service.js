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
import { CreateGameStartView } from 'app/shared/models/create-game-start.view';
var StartService = /** @class */ (function () {
    function StartService(httpClient) {
        this.httpClient = httpClient;
    }
    StartService.prototype.searchGame = function (userName) {
        var options = userName ?
            { params: new HttpParams().set('userName', userName.toString()) } : {};
        return this.httpClient.get('Start/SearchGame', options);
    };
    StartService.prototype.createGame = function (userName, amountOfBots) {
        var request = new CreateGameStartView();
        request.userName = userName;
        request.amountOfBots = amountOfBots;
        return this.httpClient.post('Start/CreateGame', request);
    };
    StartService = __decorate([
        Injectable({
            providedIn: 'root'
        }),
        __metadata("design:paramtypes", [HttpClient])
    ], StartService);
    return StartService;
}());
export { StartService };
//# sourceMappingURL=start.service.js.map