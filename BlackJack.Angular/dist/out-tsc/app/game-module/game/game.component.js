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
import { RoundService } from 'app/shared/services/round.service';
import { StartService } from 'app/shared/services/start.service';
import { NewGameService } from 'app/shared/services/new-game.service';
import { GameMappingModel } from 'app/shared/mapping-models/game-mapping-model';
var GameComponent = /** @class */ (function () {
    function GameComponent(route, roundService, startService, newGameService) {
        this.route = route;
        this.roundService = roundService;
        this.startService = startService;
        this.newGameService = newGameService;
        this.game = new GameMappingModel();
        this.takeCard = false;
        this.endRound = false;
    }
    GameComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.route.params.subscribe(function (params) {
            _this.gameId = params['gameId'];
            var isNewGame = _this.newGameService.getIsNewGame();
            if (isNewGame) {
                _this.onStartRound();
            }
            if (!isNewGame) {
                _this.onRestoreRound();
            }
        });
    };
    GameComponent.prototype.onStartRound = function () {
        var _this = this;
        this.roundService.startRound(this.gameId)
            .subscribe(function (data) {
            _this.game = _this.game.deserialize(data);
            _this.setGamePlay();
        });
    };
    GameComponent.prototype.onTakeCard = function () {
        var _this = this;
        this.roundService.takeCard(this.gameId)
            .subscribe(function (data) {
            _this.game = _this.game.deserialize(data);
            _this.setGamePlay();
        });
    };
    GameComponent.prototype.onContinueRound = function () {
        var _this = this;
        this.roundService.endRound(this.gameId)
            .subscribe(function (data) {
            _this.game.roundResult = data;
            _this.setGamePlay();
        });
    };
    GameComponent.prototype.onRestoreRound = function () {
        var _this = this;
        this.roundService.restoreRound(this.gameId)
            .subscribe(function (data) {
            _this.game = _this.game.deserialize(data);
            _this.setGamePlay();
        });
    };
    GameComponent.prototype.setGamePlay = function () {
        if (this.game.roundResult == "Round is in process") {
            this.endRound = false;
            this.takeCard = true;
        }
        if (this.game.roundResult != "Round is in process") {
            this.endRound = true;
            this.takeCard = false;
        }
    };
    GameComponent = __decorate([
        Component({
            selector: 'app-game',
            templateUrl: './game.component.html'
        }),
        __metadata("design:paramtypes", [ActivatedRoute,
            RoundService,
            StartService,
            NewGameService])
    ], GameComponent);
    return GameComponent;
}());
export { GameComponent };
//# sourceMappingURL=game.component.js.map