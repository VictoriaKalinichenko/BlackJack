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
import { RoundService } from 'app/shared/services/round.service';
import { StartService } from 'app/shared/services/start.service';
import { GameMappingModel } from 'app/shared/mapping-models/game-mapping-model';
var GameComponent = /** @class */ (function () {
    function GameComponent(route, roundService, startService) {
        this.route = route;
        this.roundService = roundService;
        this.startService = startService;
        this.game = new GameMappingModel();
        this.takeCard = false;
        this.endRound = false;
    }
    GameComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.route.params.subscribe(function (params) {
            _this.gameId = params['gameId'];
            _this.onStartRound();
        });
    };
    GameComponent.prototype.onStartRound = function () {
        var _this = this;
        this.roundService.startRound(this.gameId)
            .subscribe(function (data) {
            _this.game = deserialize(GameMappingModel, data);
            if (_this.game.roundResult == "") {
                _this.endRound = false;
                _this.takeCard = true;
            }
            if (_this.game.roundResult != "") {
                _this.endRound = true;
                _this.takeCard = false;
            }
        });
    };
    GameComponent.prototype.onTakeCard = function () {
        var _this = this;
        this.roundService.takeCard(this.gameId)
            .subscribe(function (data) {
            var humanName = _this.game.human.name;
            var dealerName = _this.game.dealer.name;
            var botNames = [];
            _this.game.bots.forEach(function (bot) {
                botNames.push(bot.name);
            });
            _this.game = deserialize(GameMappingModel, data);
            _this.game.human.name = humanName;
            _this.game.dealer.name = dealerName;
            for (var iterator = 0; iterator < botNames.length; iterator++) {
                _this.game.bots[iterator].name = botNames[iterator];
            }
            if (_this.game.roundResult != "") {
                _this.endRound = true;
                _this.takeCard = false;
            }
        });
    };
    GameComponent.prototype.onContinueRound = function () {
        var _this = this;
        this.roundService.endRound(this.gameId)
            .subscribe(function (data) {
            _this.game.roundResult = data;
            _this.endRound = true;
            _this.takeCard = false;
        });
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