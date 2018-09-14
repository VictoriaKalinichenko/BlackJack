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
import { DataService } from '../services/data.service';
import { ErrorService } from '../services/error.service';
import { deserialize } from 'json-typescript-mapper';
import { GameViewModel } from '../viewmodels/GameViewModel';
import { PlayerViewModel } from '../viewmodels/PlayerViewModel';
var GameComponent = /** @class */ (function () {
    function GameComponent(_route, _router, _dataService, _errorService) {
        this._route = _route;
        this._router = _router;
        this._dataService = _dataService;
        this._errorService = _errorService;
        this.BetInput = false;
        this.TakeCard = false;
        this.BlackJackDangerChoice = false;
        this.EndRound = false;
    }
    GameComponent.prototype.ngOnInit = function () {
        var _this = this;
        this._route.params.subscribe(function (params) {
            _this.GameId = params['Id'];
            _this.GetGame();
        });
    };
    GameComponent.prototype.GamePlayInitializer = function () {
        if (this.GameViewModel.Stage == 0) {
            this.GamePlayBetInput();
        }
        if (this.GameViewModel.Stage == 1) {
            this.HumanUpdate();
            this.BotsUpdate();
            this.DealerFirstPhaseUpdate();
            this.FirstPhaseGamePlay();
        }
        if (this.GameViewModel.Stage == 2) {
            this.HumanUpdate();
            this.BotsUpdate();
            this.DealerSecondPhaseUpdate();
            this.GamePlayEndRound();
        }
    };
    GameComponent.prototype.GetGame = function () {
        var _this = this;
        this._dataService.GetGame(this.GameId)
            .subscribe(function (data) {
            _this.GameViewModel = deserialize(GameViewModel, data);
            _this.GamePlayInitializer();
        }, function (error) {
            console.log(error);
            _this._errorService.SetError(error["error"]["Message"]);
            _this._router.navigate(['/error']);
        });
    };
    GameComponent.prototype.OnBetsCreation = function () {
        this.FirstPhase();
    };
    GameComponent.prototype.OnBlackJackDangerChoice = function (takeAward) {
        var _this = this;
        if (!takeAward) {
            this._dataService.BlackJackDangerContinueRound(this.GameId)
                .subscribe(function (error) {
                console.log(error);
                _this._errorService.SetError(error["error"]["Message"]);
                _this._router.navigate(['/error']);
            });
        }
        this.SecondPhase();
    };
    GameComponent.prototype.OnTakingCard = function (takeCard) {
        var _this = this;
        if (takeCard) {
            this._dataService.AddOneMoreCardToHuman(this.GameId)
                .subscribe(function (data) {
                _this.HumanUpdate();
                _this.GamePlayInitializer();
            }, function (error) {
                console.log(error);
                _this._errorService.SetError(error["error"]["Message"]);
                _this._router.navigate(['/error']);
            });
        }
        if (!takeCard) {
            this.SecondPhase();
        }
    };
    GameComponent.prototype.FirstPhaseGamePlay = function () {
        var _this = this;
        this._dataService.FirstPhaseGamePlay(this.GameId)
            .subscribe(function (data) {
            _this.HumanUpdate();
            _this.BotsUpdate();
            _this.DealerFirstPhaseUpdate();
            if (data["HumanBjAndDealerBjDanger"]) {
                _this.GamePlayBlackJackDangerChoice();
            }
            if (data["CanHumanTakeOneMoreCard"]) {
                _this.GamePlayTakeCard();
            }
            if (!(data["HumanBjAndDealerBjDanger"]) && !(data["CanHumanTakeOneMoreCard"])) {
                _this.SecondPhase();
            }
        }, function (error) {
            console.log(error);
            _this._errorService.SetError(error["error"]["Message"]);
            _this._router.navigate(['/error']);
        });
    };
    GameComponent.prototype.FirstPhase = function () {
        var _this = this;
        this._dataService.RoundStart(this.GameId)
            .subscribe(function (data) {
            _this.HumanUpdate();
            _this.BotsUpdate();
            _this.DealerFirstPhaseUpdate();
            _this.GameViewModel.Stage = 1;
            _this.GamePlayInitializer();
        }, function (error) {
            console.log(error);
            _this._errorService.SetError(error["error"]["Message"]);
            _this._router.navigate(['/error']);
        });
    };
    GameComponent.prototype.SecondPhase = function () {
        var _this = this;
        this._dataService.SecondPhase(this.GameId)
            .subscribe(function (data) {
            _this.HumanUpdate();
            _this.BotsUpdate();
            _this.DealerSecondPhaseUpdate();
            _this.GameViewModel.Stage = 2;
            _this.GamePlayInitializer();
        }, function (error) {
            console.log(error);
            _this._errorService.SetError(error["error"]["Message"]);
            _this._router.navigate(['/error']);
        });
    };
    GameComponent.prototype.HumanUpdate = function () {
        var _this = this;
        this._dataService.GetGamePlayer(this.GameViewModel.Human.GamePlayerId)
            .subscribe(function (data) {
            var name = _this.GameViewModel.Human.Name;
            _this.GameViewModel.Human = deserialize(PlayerViewModel, data);
            _this.GameViewModel.Human.Name = name;
        }, function (error) {
            console.log(error);
            _this._errorService.SetError(error["error"]["Message"]);
            _this._router.navigate(['/error']);
        });
    };
    GameComponent.prototype.DealerFirstPhaseUpdate = function () {
        var _this = this;
        this._dataService.GetDealerFirstPhase(this.GameViewModel.Dealer.GamePlayerId)
            .subscribe(function (data) {
            var name = _this.GameViewModel.Dealer.Name;
            _this.GameViewModel.Dealer = deserialize(PlayerViewModel, data);
            _this.GameViewModel.Dealer.Name = name;
        }, function (error) {
            console.log(error);
            _this._errorService.SetError(error["error"]["Message"]);
            _this._router.navigate(['/error']);
        });
    };
    GameComponent.prototype.DealerSecondPhaseUpdate = function () {
        var _this = this;
        this._dataService.GetDealerSecondPhase(this.GameViewModel.Dealer.GamePlayerId)
            .subscribe(function (data) {
            var name = _this.GameViewModel.Dealer.Name;
            _this.GameViewModel.Dealer = deserialize(PlayerViewModel, data);
            _this.GameViewModel.Dealer.Name = name;
        }, function (error) {
            console.log(error);
            _this._errorService.SetError(error["error"]["Message"]);
            _this._router.navigate(['/error']);
        });
    };
    GameComponent.prototype.BotsUpdate = function () {
        var _this = this;
        this.GameViewModel.Bots.forEach(function (bot) {
            _this._dataService.GetGamePlayer(bot.GamePlayerId)
                .subscribe(function (data) {
                var inBot = deserialize(PlayerViewModel, data);
                bot.Bet = inBot.Bet;
                bot.Score = inBot.Score;
                bot.RoundScore = inBot.RoundScore;
                bot.Cards = inBot.Cards;
            }, function (error) {
                console.log(error);
                _this._errorService.SetError(error["error"]["Message"]);
                _this._router.navigate(['/error']);
            });
        });
    };
    GameComponent.prototype.GamePlayBetInput = function () {
        this.BetInput = true;
        this.TakeCard = false;
        this.BlackJackDangerChoice = false;
        this.EndRound = false;
    };
    GameComponent.prototype.GamePlayTakeCard = function () {
        this.BetInput = false;
        this.TakeCard = true;
        this.BlackJackDangerChoice = false;
        this.EndRound = false;
    };
    GameComponent.prototype.GamePlayBlackJackDangerChoice = function () {
        this.BetInput = false;
        this.TakeCard = false;
        this.BlackJackDangerChoice = true;
        this.EndRound = false;
    };
    GameComponent.prototype.GamePlayEndRound = function () {
        this.BetInput = false;
        this.TakeCard = false;
        this.BlackJackDangerChoice = false;
        this.EndRound = true;
    };
    GameComponent.prototype.Reload = function () {
        this.GetGame();
    };
    GameComponent = __decorate([
        Component({
            selector: 'app-game',
            templateUrl: './game.component.html',
            styleUrls: ['./game.component.css']
        }),
        __metadata("design:paramtypes", [ActivatedRoute,
            Router,
            DataService,
            ErrorService])
    ], GameComponent);
    return GameComponent;
}());
export { GameComponent };
//# sourceMappingURL=game.component.js.map