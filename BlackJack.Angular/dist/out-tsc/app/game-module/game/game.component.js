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
import { GameModel } from 'app/shared/models/game-model';
import { RoundService } from 'app/shared/services/round.service';
import { StartService } from 'app/shared/services/start.service';
var GameComponent = /** @class */ (function () {
    function GameComponent(route, roundService, startService) {
        this.route = route;
        this.roundService = roundService;
        this.startService = startService;
        this.dealerName = "Dealer";
        this.botNames = ["Bot0", "Bot1", "Bot2", "Bot3", "Bot4"];
        this.game = new GameModel();
        this.takeCardGamePlay = false;
        this.endRoundGamePlay = false;
    }
    GameComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.route.params.subscribe(function (params) {
            _this.gameId = params['gameId'];
            _this.humanName = params['userName'];
            var isNewRound = params['isNewGame'];
            _this.startRound(isNewRound);
        });
    };
    GameComponent.prototype.startRound = function (isNewRound) {
        var _this = this;
        this.roundService.startRound(this.gameId, isNewRound)
            .subscribe(function (data) {
            _this.game = data;
            _this.setGamePlay();
        });
    };
    GameComponent.prototype.takeCard = function () {
        var _this = this;
        this.roundService.takeCard(this.gameId)
            .subscribe(function (data) {
            _this.game = data;
            _this.setGamePlay();
        });
    };
    GameComponent.prototype.endRound = function () {
        var _this = this;
        this.roundService.endRound(this.gameId)
            .subscribe(function (data) {
            _this.game.roundResult = data.roundResult;
            _this.game.dealer = data.dealer;
            _this.setGamePlay();
        });
    };
    GameComponent.prototype.setGamePlay = function () {
        if (this.game.roundResult == "Round is in process") {
            this.endRoundGamePlay = false;
            this.takeCardGamePlay = true;
        }
        if (this.game.roundResult != "Round is in process") {
            this.endRoundGamePlay = true;
            this.takeCardGamePlay = false;
        }
    };
    GameComponent = __decorate([
        Component({
            selector: 'app-game',
            templateUrl: './game.component.html'
        }),
        __metadata("design:paramtypes", [ActivatedRoute,
            RoundService,
            StartService])
    ], GameComponent);
    return GameComponent;
}());
export { GameComponent };
//# sourceMappingURL=game.component.js.map