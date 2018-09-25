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
import { UserNameService } from '../../shared/services/user-name.service';
import { HttpService } from '../../shared/services/http.service';
import { ErrorService } from '../../shared/services/error.service';
import { AuthorizedUserView } from '../../shared/models/authorized-user-view';
var StartpageComponent = /** @class */ (function () {
    function StartpageComponent(_userNameService, _httpService, _errorService, _router) {
        this._userNameService = _userNameService;
        this._httpService = _httpService;
        this._errorService = _errorService;
        this._router = _router;
        this.Player = new AuthorizedUserView();
        this.AmountOfBots = 0;
    }
    StartpageComponent.prototype.ngOnInit = function () {
        this.UserName = this._userNameService.GetUserName();
        this.AuthUser(this.UserName);
    };
    StartpageComponent.prototype.AuthUser = function (userName) {
        var _this = this;
        this._httpService.GetAuthorizedPlayer(this.UserName)
            .subscribe(function (data) {
            _this.Player.Name = data.Name;
            _this.Player.PlayerId = data.PlayerId;
            _this.Player.ResumeGame = data.ResumeGame;
        }, function (error) {
            console.log(error);
            _this._errorService.SetError(error["error"]["Message"]);
            _this._router.navigate(['/error']);
        });
    };
    StartpageComponent.prototype.StartNewGame = function () {
        var _this = this;
        this._httpService.CreateNewGame(this.Player.PlayerId, this.AmountOfBots)
            .subscribe(function (data) {
            _this.GameId = data["GameId"];
            _this._router.navigate(['/user/' + _this.UserName + '/game/' + _this.GameId]);
        }, function (error) {
            console.log(error);
            _this._errorService.SetError(error["error"]["Message"]);
            _this._router.navigate(['/error']);
        });
    };
    StartpageComponent.prototype.ResumeGame = function () {
        var _this = this;
        this._httpService.ResumeGame(this.Player.PlayerId)
            .subscribe(function (data) {
            _this.GameId = data["GameId"];
            _this._router.navigate(['/user/' + _this.UserName + '/game/' + _this.GameId]);
        }, function (error) {
            console.log(error);
            _this._errorService.SetError(error["error"]["Message"]);
            _this._router.navigate(['/error']);
        });
    };
    StartpageComponent = __decorate([
        Component({
            selector: 'app-startpage',
            templateUrl: './startpage.component.html',
            styleUrls: ['./startpage.component.css']
        }),
        __metadata("design:paramtypes", [UserNameService,
            HttpService,
            ErrorService,
            Router])
    ], StartpageComponent);
    return StartpageComponent;
}());
export { StartpageComponent };
//# sourceMappingURL=startpage.component.js.map