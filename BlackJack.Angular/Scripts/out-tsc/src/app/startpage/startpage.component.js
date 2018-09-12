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
import { DataService } from '../services/data.service';
import { AuthPlayerViewModel } from '../viewmodels/AuthPlayerViewModel';
var StartpageComponent = /** @class */ (function () {
    function StartpageComponent(dataService, router) {
        this.dataService = dataService;
        this.router = router;
        this.Player = new AuthPlayerViewModel();
        this.AmountOfBots = 0;
    }
    StartpageComponent.prototype.ngOnInit = function () {
        this.UserName = this.dataService.GetUserName();
        this.AuthUser(this.UserName);
    };
    StartpageComponent.prototype.AuthUser = function (userName) {
        var _this = this;
        this.dataService.GetAuthorizedPlayer()
            .subscribe(function (data) {
            _this.Player.Name = data.Name;
            _this.Player.PlayerId = data.PlayerId;
            _this.Player.ResumeGame = data.ResumeGame;
        }, function (error) {
            console.log(error);
        });
    };
    StartpageComponent.prototype.StartNewGame = function () {
        var _this = this;
        this.dataService.CreateNewGame(this.Player.PlayerId, this.AmountOfBots)
            .subscribe(function (data) {
            _this.GameId = data.GameId;
            _this.router.navigate(['/user/' + _this.UserName + '/game/' + _this.GameId]);
        }, function (error) {
            console.log(error);
        });
    };
    StartpageComponent.prototype.ResumeGame = function () {
        var _this = this;
        this.dataService.ResumeGame(this.Player.PlayerId)
            .subscribe(function (data) {
            _this.GameId = data.GameId;
            _this.router.navigate(['/user/' + _this.UserName + '/game/' + _this.GameId]);
        }, function (error) {
            console.log(error);
        });
    };
    StartpageComponent = __decorate([
        Component({
            selector: 'app-startpage',
            templateUrl: './startpage.component.html',
            styleUrls: ['./startpage.component.css']
        }),
        __metadata("design:paramtypes", [DataService,
            Router])
    ], StartpageComponent);
    return StartpageComponent;
}());
export { StartpageComponent };
//# sourceMappingURL=startpage.component.js.map