var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component, Input, Output, EventEmitter } from '@angular/core';
import { DataService } from '../services/data.service';
import { Router } from '@angular/router';
var EndRoundComponent = /** @class */ (function () {
    function EndRoundComponent(dataService, router) {
        this.dataService = dataService;
        this.router = router;
        this.Reload = new EventEmitter();
        this.IsEndRound = true;
        this.IsGameOver = false;
    }
    EndRoundComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.dataService.HumanRoundResult(this.GameId)
            .subscribe(function (data) {
            _this.RoundResult = data["RoundResult"];
        }, function (error) {
            console.log(error);
        });
    };
    EndRoundComponent.prototype.EndRound = function () {
        var _this = this;
        this.dataService.UpdateGamePlayersForNewRound(this.GameId)
            .subscribe(function (data) {
            if (data["IsGameOver"] != "") {
                _this.IsGameOver = true;
                _this.IsEndRound = false;
                _this.GameOver = data["IsGameOver"];
            }
            if (data["IsGameOver"] == "") {
                _this.Reload.emit();
            }
        }, function (error) {
            console.log(error);
        });
    };
    EndRoundComponent.prototype.StartNewGame = function () {
        this.router.navigate(['/user/' + this.UserName]);
    };
    __decorate([
        Input(),
        __metadata("design:type", String)
    ], EndRoundComponent.prototype, "UserName", void 0);
    __decorate([
        Input(),
        __metadata("design:type", Number)
    ], EndRoundComponent.prototype, "GameId", void 0);
    __decorate([
        Output(),
        __metadata("design:type", Object)
    ], EndRoundComponent.prototype, "Reload", void 0);
    EndRoundComponent = __decorate([
        Component({
            selector: 'app-end-round',
            templateUrl: './end-round.component.html',
            styleUrls: ['./end-round.component.css']
        }),
        __metadata("design:paramtypes", [DataService,
            Router])
    ], EndRoundComponent);
    return EndRoundComponent;
}());
export { EndRoundComponent };
//# sourceMappingURL=end-round.component.js.map