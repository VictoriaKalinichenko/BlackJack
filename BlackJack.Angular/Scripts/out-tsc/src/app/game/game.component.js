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
import { ActivatedRoute } from '@angular/router';
import { deserialize } from 'json-typescript-mapper';
import { GameViewModel } from '../viewmodels/GameViewModel';
import { DataService } from '../services/data.service';
var GameComponent = /** @class */ (function () {
    function GameComponent(route, dataService) {
        this.route = route;
        this.dataService = dataService;
        this.BetInput = false;
        this.TakeCard = false;
        this.BjDangerChoice = false;
        this.EndRound = false;
        this.NewGame = false;
    }
    GameComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.route.params.subscribe(function (params) {
            _this.GameId = params['Id'];
            _this.GetGame();
        });
    };
    GameComponent.prototype.GamePlayInitializer = function () {
        if (this.GameViewModel.Stage == 0) {
            this.GamePlayBetInput();
        }
        if (this.GameViewModel.Stage == 1) {
            this.GamePlayTakeCard();
        }
    };
    GameComponent.prototype.GetGame = function () {
        var _this = this;
        this.dataService.GetGame(this.GameId)
            .subscribe(function (data) {
            _this.GameViewModel = deserialize(GameViewModel, data);
            _this.GamePlayInitializer();
        }, function (error) {
            console.log(error);
        });
    };
    GameComponent.prototype.OnBetsCreation = function () {
        var _this = this;
        this.dataService.RoundStart(this.GameId)
            .subscribe(function (data) {
            _this.GetGame();
        }, function (error) {
            console.log(error);
        });
    };
    GameComponent.prototype.GamePlayBetInput = function () {
        this.BetInput = true;
        this.TakeCard = false;
        this.BjDangerChoice = false;
        this.EndRound = false;
        this.NewGame = false;
    };
    GameComponent.prototype.GamePlayTakeCard = function () {
        this.BetInput = false;
        this.TakeCard = true;
        this.BjDangerChoice = false;
        this.EndRound = false;
        this.NewGame = false;
    };
    GameComponent.prototype.GamePlayBjDangerChoice = function () {
        this.BetInput = false;
        this.TakeCard = false;
        this.BjDangerChoice = true;
        this.EndRound = false;
        this.NewGame = false;
    };
    GameComponent.prototype.GamePlayEndRound = function () {
        this.BetInput = false;
        this.TakeCard = false;
        this.BjDangerChoice = false;
        this.EndRound = true;
        this.NewGame = false;
    };
    GameComponent.prototype.GamePlayNewGame = function () {
        this.BetInput = false;
        this.TakeCard = false;
        this.BjDangerChoice = false;
        this.EndRound = false;
        this.NewGame = true;
    };
    GameComponent = __decorate([
        Component({
            selector: 'app-game',
            templateUrl: './game.component.html',
            styleUrls: ['./game.component.css']
        }),
        __metadata("design:paramtypes", [ActivatedRoute,
            DataService])
    ], GameComponent);
    return GameComponent;
}());
export { GameComponent };
//# sourceMappingURL=game.component.js.map