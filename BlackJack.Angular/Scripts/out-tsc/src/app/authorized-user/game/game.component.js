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
import { HttpService } from '../../shared/services/http.service';
import { ErrorService } from '../../shared/services/error.service';
import { GameView } from '../../shared/models/game-view';
import { PlayerView } from '../../shared/models/player-view';
var GameComponent = /** @class */ (function () {
    function GameComponent(_route, _router, _httpService, _errorService) {
        this._route = _route;
        this._router = _router;
        this._httpService = _httpService;
        this._errorService = _errorService;
        this.Game = new GameView();
        this.BetValidationError = false;
        this.Bet = 50;
        this.BetInput = false;
        this.TakeCard = false;
        this.BlackJackDangerChoice = false;
        this.EndRound = false;
        this.EndGame = false;
    }
    GameComponent.prototype.ngOnInit = function () {
        var _this = this;
        this._route.params.subscribe(function (params) {
            _this.GameId = params['Id'];
            _this.GetGame();
        });
    };
    GameComponent.prototype.GamePlayInitializer = function () {
        if (this.Game.Stage == 0) {
            this.GamePlayBetInput();
        }
        if (this.Game.Stage == 1) {
            this.ResumeAfterStartRound();
        }
        if (this.Game.Stage == 2) {
            this.ResumeAfterContinueRound();
        }
    };
    GameComponent.prototype.GetGame = function () {
        var _this = this;
        this._httpService.GetGame(this.GameId)
            .subscribe(function (data) {
            _this.Game = deserialize(GameView, data);
            if (data["IsGameOver"] != "") {
                _this.GameResult = data["IsGameOver"];
                _this.GamePlayEndGame();
            }
            _this.GamePlayInitializer();
        }, function (error) {
            console.log(error);
            _this._errorService.SetError(error["error"]["Message"]);
            _this._router.navigate(['/error']);
        });
    };
    GameComponent.prototype.ResumeAfterStartRound = function () {
        var _this = this;
        this._httpService.ResumeAfterStartRound(this.Game.Id)
            .subscribe(function (data) {
            _this.Game = deserialize(GameView, data);
            _this.StartRoundGamePlay(data["BlackJackChoice"], data["CanTakeCard"]);
        }, function (error) {
            console.log(error);
            _this._errorService.SetError(error["error"]["Message"]);
            _this._router.navigate(['/error']);
        });
    };
    GameComponent.prototype.ResumeAfterContinueRound = function () {
        var _this = this;
        this._httpService.ResumeAfterContinueRound(this.Game.Id)
            .subscribe(function (data) {
            _this.Game = deserialize(GameView, data);
            _this.ContinueRoundGamePlay(data["RoundResult"]);
        }, function (error) {
            console.log(error);
            _this._errorService.SetError(error["error"]["Message"]);
            _this._router.navigate(['/error']);
        });
    };
    GameComponent.prototype.AddCard = function (takeCard) {
        var _this = this;
        if (takeCard) {
            this._httpService.AddCard(this.Game.Id)
                .subscribe(function (data) {
                if (data["CanTakeCard"]) {
                    _this.Game.Human = deserialize(PlayerView, data);
                }
                if (!data["CanTakeCard"]) {
                    _this.ContinueRound(false);
                }
            }, function (error) {
                console.log(error);
                _this._errorService.SetError(error["error"]["Message"]);
                _this._router.navigate(['/error']);
            });
        }
        if (!takeCard) {
            this.ContinueRound(false);
        }
    };
    GameComponent.prototype.StartRoundGamePlay = function (blackJackChoice, canTakeCard) {
        this.Game.Stage = 1;
        this.BetValidationError = false;
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
        this.RoundResult = roundResult;
        this.GamePlayEndRound();
    };
    GameComponent.prototype.StartRound = function () {
        var _this = this;
        this._httpService.StartRound(this.Game.Id, this.Game.Human.GamePlayerId, this.Bet)
            .subscribe(function (data) {
            _this.Game = deserialize(GameView, data["Data"]);
            if (data["Message"] != "") {
                _this.ShowValidationMessage(data["Message"]);
            }
            if (data["Message"] == "") {
                _this.StartRoundGamePlay(data["Data"]["BlackJackChoice"], data["Data"]["CanTakeCard"]);
            }
        }, function (error) {
            console.log(error);
            _this._errorService.SetError(error["error"]["Message"]);
            _this._router.navigate(['/error']);
        });
    };
    GameComponent.prototype.ShowValidationMessage = function (validationMessage) {
        this.BetValidationError = true;
        this.BetValidationMessage = validationMessage;
    };
    GameComponent.prototype.ContinueRound = function (humanBlackJackContinueRound) {
        var _this = this;
        this._httpService.ContinueRound(this.Game.Id, humanBlackJackContinueRound)
            .subscribe(function (data) {
            _this.Game = deserialize(GameView, data);
            _this.ContinueRoundGamePlay(data["RoundResult"]);
        }, function (error) {
            console.log(error);
            _this._errorService.SetError(error["error"]["Message"]);
            _this._router.navigate(['/error']);
        });
    };
    GameComponent.prototype.StartNewGame = function () {
        var _this = this;
        this._httpService.EndGame(this.Game.Id, this.GameResult)
            .subscribe(function (data) {
            _this._router.navigate(['/user/' + _this.Game.Human.Name]);
        }, function (error) {
            console.log(error);
            _this._errorService.SetError(error["error"]["Message"]);
            _this._router.navigate(['/error']);
        });
    };
    GameComponent.prototype.StartNewRound = function () {
        var _this = this;
        this._httpService.EndRound(this.Game.Id)
            .subscribe(function (data) {
            _this.GetGame();
        }, function (error) {
            console.log(error);
            _this._errorService.SetError(error["error"]["Message"]);
            _this._router.navigate(['/error']);
        });
    };
    GameComponent.prototype.GamePlayBetInput = function () {
        this.BetInput = true;
        this.TakeCard = false;
        this.BlackJackDangerChoice = false;
        this.EndRound = false;
        this.EndGame = false;
    };
    GameComponent.prototype.GamePlayTakeCard = function () {
        this.BetInput = false;
        this.TakeCard = true;
        this.BlackJackDangerChoice = false;
        this.EndRound = false;
        this.EndGame = false;
    };
    GameComponent.prototype.GamePlayBlackJackDangerChoice = function () {
        this.BetInput = false;
        this.TakeCard = false;
        this.BlackJackDangerChoice = true;
        this.EndRound = false;
        this.EndGame = false;
    };
    GameComponent.prototype.GamePlayEndRound = function () {
        this.Game.Stage = 2;
        this.BetInput = false;
        this.TakeCard = false;
        this.BlackJackDangerChoice = false;
        this.EndRound = true;
        this.EndGame = false;
    };
    GameComponent.prototype.GamePlayEndGame = function () {
        this.BetInput = false;
        this.TakeCard = false;
        this.BlackJackDangerChoice = false;
        this.EndRound = false;
        this.EndGame = true;
    };
    GameComponent = __decorate([
        Component({
            selector: 'app-game',
            templateUrl: './game.component.html',
            styleUrls: ['./game.component.css']
        }),
        __metadata("design:paramtypes", [ActivatedRoute,
            Router,
            HttpService,
            ErrorService])
    ], GameComponent);
    return GameComponent;
}());
export { GameComponent };
//# sourceMappingURL=game.component.js.map