var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { UserNameService } from 'app/shared/services/user-name.service';
import { HttpService } from 'app/shared/services/http.service';
import { AuthorizePlayerStartView } from 'app/shared/view-models/authorize-player-start-view';
var StartPageComponent = /** @class */ (function () {
    function StartPageComponent(userNameService, httpService, router) {
        this.userNameService = userNameService;
        this.httpService = httpService;
        this.router = router;
        this.player = new AuthorizePlayerStartView();
        this.amountOfBots = 0;
    }
    StartPageComponent.prototype.ngOnInit = function () {
        this.userName = this.userNameService.getUserName();
        this.authUser(this.userName);
    };
    StartPageComponent.prototype.authUser = function (userName) {
        var _this = this;
        this.httpService.getAuthorizedPlayer(this.userName)
            .subscribe(function (data) {
            _this.player.name = data["Name"];
            _this.player.playerId = data["PlayerId"];
            _this.player.resumeGame = data["ResumeGame"];
        });
    };
    StartPageComponent.prototype.startNewGame = function () {
        var _this = this;
        this.httpService.createGame(this.player.playerId, this.amountOfBots)
            .subscribe(function (data) {
            _this.gameId = data["GameId"];
            _this.router.navigate(['/user/' + _this.userName + '/game/' + _this.gameId]);
        });
    };
    StartPageComponent.prototype.resumeGame = function () {
        var _this = this;
        this.httpService.resumeGame(this.player.playerId)
            .subscribe(function (data) {
            _this.gameId = data["GameId"];
            _this.router.navigate(['/user/' + _this.userName + '/game/' + _this.gameId]);
        });
    };
    StartPageComponent = __decorate([
        Component({
            selector: 'app-start-page',
            templateUrl: './start-page.component.html',
            styleUrls: ['./start-page.component.css']
        }),
        __metadata("design:paramtypes", [UserNameService,
            HttpService,
            Router])
    ], StartPageComponent);
    return StartPageComponent;
}());
export { StartPageComponent };
//# sourceMappingURL=start-page.component.js.map