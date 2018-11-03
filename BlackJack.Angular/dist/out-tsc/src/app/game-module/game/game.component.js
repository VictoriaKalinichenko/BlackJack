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
import { ActivatedRoute, Router } from '@angular/router';
import { deserialize } from 'json-typescript-mapper';
import { RoundService } from 'app/shared/services/round.service';
import { StartService } from 'app/shared/services/start.service';
import { GameMappingModel } from 'app/shared/mapping-models/game-mapping-model';
import { PlayerMappingModel } from 'app/shared/mapping-models/player-mapping-model';
var GameComponent = /** @class */ (function () {
    function GameComponent(route, router, roundService, startService) {
        this.route = route;
        this.router = router;
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
            _this.initializeRound();
        });
    };
    GameComponent.prototype.initializeRound = function () {
        var _this = this;
        this.startService.initializeRound(this.gameId)
            .subscribe(function (data) {
            _this.game = deserialize(GameMappingModel, data);
            _this.restoreRound();
        });
    };
    GameComponent.prototype.startRound = function () {
        var _this = this;
        this.roundService.startRound(this.game.id)
            .subscribe(function (data) {
            _this.game = deserialize(GameMappingModel, data);
            if (data["CanTakeCard"]) {
                _this.endRound = false;
                _this.takeCard = true;
            }
            if (!data["CanTakeCard"]) {
                _this.continueRound();
            }
        });
    };
    GameComponent.prototype.restoreRound = function () {
        var _this = this;
        this.roundService.restoreRound(this.game.id)
            .subscribe(function (data) {
            var roundResult = _this.game.roundResult;
            _this.game = deserialize(GameMappingModel, data);
            _this.game.roundResult = roundResult;
            if (data["Human"]["Cards"][0] == null) {
                _this.startRound();
            }
            if (!data["CanTakeCard"] && roundResult == "") {
                _this.continueRound();
            }
            if (roundResult != "") {
                _this.endRound = true;
                _this.takeCard = false;
            }
            if (data["CanTakeCard"]) {
                _this.endRound = false;
                _this.takeCard = true;
            }
        });
    };
    GameComponent.prototype.addCard = function (takeCard) {
        var _this = this;
        if (takeCard) {
            this.roundService.addCard(this.game.id)
                .subscribe(function (data) {
                if (data["CanTakeCard"]) {
                    _this.game.human = deserialize(PlayerMappingModel, data);
                }
                if (!data["CanTakeCard"]) {
                    _this.continueRound();
                }
            });
        }
        if (!takeCard) {
            this.continueRound();
        }
    };
    GameComponent.prototype.continueRound = function () {
        var _this = this;
        this.roundService.continueRound(this.game.id)
            .subscribe(function (data) {
            _this.game = deserialize(GameMappingModel, data);
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
            Router,
            RoundService,
            StartService])
    ], GameComponent);
    return GameComponent;
}());
export { GameComponent };
//# sourceMappingURL=game.component.js.map