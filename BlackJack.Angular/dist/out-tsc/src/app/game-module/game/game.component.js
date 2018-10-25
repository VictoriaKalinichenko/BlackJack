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
        this.betValidationError = false;
        this.bet = 50;
        this.betInput = false;
        this.takeCard = false;
        this.blackJackDangerChoice = false;
        this.endRound = false;
        this.endGame = false;
    }
    GameComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.route.params.subscribe(function (params) {
            _this.gameId = params['Id'];
            _this.getGame();
        });
    };
    GameComponent.prototype.gamePlayInitializer = function () {
        if (this.game.stage == 0) {
            this.gamePlayBetInput();
        }
        if (this.game.stage == 1) {
            this.resumeAfterStartRound();
        }
        if (this.game.stage == 2) {
            this.resumeAfterContinueRound();
        }
    };
    GameComponent.prototype.getGame = function () {
        var _this = this;
        this.startService.getGame(this.gameId)
            .subscribe(function (data) {
            _this.game = deserialize(GameMappingModel, data);
            if (data["IsGameOver"] != "") {
                _this.gameResult = data["IsGameOver"];
                _this.gamePlayEndGame();
            }
            _this.gamePlayInitializer();
        });
    };
    GameComponent.prototype.resumeAfterStartRound = function () {
        var _this = this;
        this.roundService.resumeAfterStartRound(this.game.id)
            .subscribe(function (data) {
            _this.game = deserialize(GameMappingModel, data);
            _this.startRoundGamePlay(data["BlackJackChoice"], data["CanTakeCard"]);
        });
    };
    GameComponent.prototype.resumeAfterContinueRound = function () {
        var _this = this;
        this.roundService.resumeAfterContinueRound(this.game.id)
            .subscribe(function (data) {
            _this.game = deserialize(GameMappingModel, data);
            _this.continueRoundGamePlay(data["RoundResult"]);
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
                    _this.continueRound(false);
                }
            });
        }
        if (!takeCard) {
            this.continueRound(false);
        }
    };
    GameComponent.prototype.startRoundGamePlay = function (blackJackChoice, canTakeCard) {
        this.game.stage = 1;
        this.betValidationError = false;
        if (blackJackChoice) {
            this.gamePlayBlackJackDangerChoice();
        }
        if (canTakeCard) {
            this.gamePlayTakeCard();
        }
        if (!blackJackChoice && !canTakeCard) {
            this.continueRound(false);
        }
    };
    GameComponent.prototype.continueRoundGamePlay = function (roundResult) {
        this.roundResult = roundResult;
        this.gamePlayEndRound();
    };
    GameComponent.prototype.startRound = function () {
        var _this = this;
        this.roundService.startRound(this.game.id, this.game.human.gamePlayerId, this.bet)
            .subscribe(function (data) {
            _this.game = deserialize(GameMappingModel, data["Data"]);
            if (data["Message"] != null) {
                _this.showValidationMessage(data["Message"]);
            }
            if (data["Message"] == null) {
                _this.startRoundGamePlay(data["Data"]["BlackJackChoice"], data["Data"]["CanTakeCard"]);
            }
        });
    };
    GameComponent.prototype.showValidationMessage = function (validationMessage) {
        this.betValidationError = true;
        this.betValidationMessage = validationMessage;
    };
    GameComponent.prototype.continueRound = function (continueRound) {
        var _this = this;
        this.roundService.continueRound(this.game.id, continueRound)
            .subscribe(function (data) {
            _this.game = deserialize(GameMappingModel, data);
            _this.continueRoundGamePlay(data["RoundResult"]);
        });
    };
    GameComponent.prototype.startNewGame = function () {
        var _this = this;
        this.roundService.endGame(this.game.id, this.gameResult)
            .subscribe(function (data) {
            _this.router.navigate(['/user/' + _this.game.human.name]);
        });
    };
    GameComponent.prototype.startNewRound = function () {
        var _this = this;
        this.roundService.endRound(this.game.id)
            .subscribe(function (data) {
            _this.getGame();
        });
    };
    GameComponent.prototype.gamePlayBetInput = function () {
        this.betInput = true;
        this.takeCard = false;
        this.blackJackDangerChoice = false;
        this.endRound = false;
        this.endGame = false;
    };
    GameComponent.prototype.gamePlayTakeCard = function () {
        this.betInput = false;
        this.takeCard = true;
        this.blackJackDangerChoice = false;
        this.endRound = false;
        this.endGame = false;
    };
    GameComponent.prototype.gamePlayBlackJackDangerChoice = function () {
        this.betInput = false;
        this.takeCard = false;
        this.blackJackDangerChoice = true;
        this.endRound = false;
        this.endGame = false;
    };
    GameComponent.prototype.gamePlayEndRound = function () {
        this.game.stage = 2;
        this.betInput = false;
        this.takeCard = false;
        this.blackJackDangerChoice = false;
        this.endRound = true;
        this.endGame = false;
    };
    GameComponent.prototype.gamePlayEndGame = function () {
        this.betInput = false;
        this.takeCard = false;
        this.blackJackDangerChoice = false;
        this.endRound = false;
        this.endGame = true;
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