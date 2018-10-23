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
import { HttpService } from 'app/shared/services/http.service';
import { GameMappingModel } from 'app/shared/mapping-models/game-mapping-model';
import { PlayerMappingModel } from 'app/shared/mapping-models/player-mapping-model';
var GameComponent = /** @class */ (function () {
    function GameComponent(route, router, httpService) {
        this.route = route;
        this.router = router;
        this.httpService = httpService;
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
            _this.GetGame();
        });
    };
    GameComponent.prototype.GamePlayInitializer = function () {
        if (this.game.stage == 0) {
            this.GamePlayBetInput();
        }
        if (this.game.stage == 1) {
            this.ResumeAfterStartRound();
        }
        if (this.game.stage == 2) {
            this.ResumeAfterContinueRound();
        }
    };
    GameComponent.prototype.GetGame = function () {
        var _this = this;
        this.httpService.GetGame(this.gameId)
            .subscribe(function (data) {
            _this.game = deserialize(GameMappingModel, data);
            if (data["IsGameOver"] != "") {
                _this.gameResult = data["IsGameOver"];
                _this.GamePlayEndGame();
            }
            _this.GamePlayInitializer();
        });
    };
    GameComponent.prototype.ResumeAfterStartRound = function () {
        var _this = this;
        this.httpService.ResumeAfterStartRound(this.game.id)
            .subscribe(function (data) {
            _this.game = deserialize(GameMappingModel, data);
            _this.StartRoundGamePlay(data["BlackJackChoice"], data["CanTakeCard"]);
        });
    };
    GameComponent.prototype.ResumeAfterContinueRound = function () {
        var _this = this;
        this.httpService.ResumeAfterContinueRound(this.game.id)
            .subscribe(function (data) {
            _this.game = deserialize(GameMappingModel, data);
            _this.ContinueRoundGamePlay(data["RoundResult"]);
        });
    };
    GameComponent.prototype.AddCard = function (takeCard) {
        var _this = this;
        if (takeCard) {
            this.httpService.AddCard(this.game.id)
                .subscribe(function (data) {
                if (data["CanTakeCard"]) {
                    _this.game.human = deserialize(PlayerMappingModel, data);
                }
                if (!data["CanTakeCard"]) {
                    _this.ContinueRound(false);
                }
            });
        }
        if (!takeCard) {
            this.ContinueRound(false);
        }
    };
    GameComponent.prototype.StartRoundGamePlay = function (blackJackChoice, canTakeCard) {
        this.game.stage = 1;
        this.betValidationError = false;
        if (blackJackChoice) {
            this.GamePlayBlackJackDangerChoice();
        }
        if (canTakeCard) {
            this.GamePlayTakeCard();
        }
        if (!blackJackChoice && !canTakeCard) {
            this.ContinueRound(false);
        }
    };
    GameComponent.prototype.ContinueRoundGamePlay = function (roundResult) {
        this.roundResult = roundResult;
        this.GamePlayEndRound();
    };
    GameComponent.prototype.StartRound = function () {
        var _this = this;
        this.httpService.StartRound(this.game.id, this.game.human.gamePlayerId, this.bet)
            .subscribe(function (data) {
            _this.game = deserialize(GameMappingModel, data["Data"]);
            if (data["Message"] != null) {
                _this.ShowValidationMessage(data["Message"]);
            }
            if (data["Message"] == null) {
                _this.StartRoundGamePlay(data["Data"]["BlackJackChoice"], data["Data"]["CanTakeCard"]);
            }
        });
    };
    GameComponent.prototype.ShowValidationMessage = function (validationMessage) {
        this.betValidationError = true;
        this.betValidationMessage = validationMessage;
    };
    GameComponent.prototype.ContinueRound = function (continueRound) {
        var _this = this;
        this.httpService.ContinueRound(this.game.id, continueRound)
            .subscribe(function (data) {
            _this.game = deserialize(GameMappingModel, data);
            _this.ContinueRoundGamePlay(data["RoundResult"]);
        });
    };
    GameComponent.prototype.StartNewGame = function () {
        var _this = this;
        this.httpService.EndGame(this.game.id, this.gameResult)
            .subscribe(function (data) {
            _this.router.navigate(['/user/' + _this.game.human.name]);
        });
    };
    GameComponent.prototype.StartNewRound = function () {
        var _this = this;
        this.httpService.EndRound(this.game.id)
            .subscribe(function (data) {
            _this.GetGame();
        });
    };
    GameComponent.prototype.GamePlayBetInput = function () {
        this.betInput = true;
        this.takeCard = false;
        this.blackJackDangerChoice = false;
        this.endRound = false;
        this.endGame = false;
    };
    GameComponent.prototype.GamePlayTakeCard = function () {
        this.betInput = false;
        this.takeCard = true;
        this.blackJackDangerChoice = false;
        this.endRound = false;
        this.endGame = false;
    };
    GameComponent.prototype.GamePlayBlackJackDangerChoice = function () {
        this.betInput = false;
        this.takeCard = false;
        this.blackJackDangerChoice = true;
        this.endRound = false;
        this.endGame = false;
    };
    GameComponent.prototype.GamePlayEndRound = function () {
        this.game.stage = 2;
        this.betInput = false;
        this.takeCard = false;
        this.blackJackDangerChoice = false;
        this.endRound = true;
        this.endGame = false;
    };
    GameComponent.prototype.GamePlayEndGame = function () {
        this.betInput = false;
        this.takeCard = false;
        this.blackJackDangerChoice = false;
        this.endRound = false;
        this.endGame = true;
    };
    GameComponent = __decorate([
        Component({
            selector: 'app-game',
            templateUrl: './game.component.html',
            styleUrls: ['./game.component.css']
        }),
        __metadata("design:paramtypes", [ActivatedRoute,
            Router,
            HttpService])
    ], GameComponent);
    return GameComponent;
}());
export { GameComponent };
//# sourceMappingURL=game.component.js.map