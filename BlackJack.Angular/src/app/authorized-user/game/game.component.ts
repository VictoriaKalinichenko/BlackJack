import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { deserialize } from 'json-typescript-mapper';

import { HttpService } from '../../shared/services/http.service';
import { ErrorService } from '../../shared/services/error.service';
import { GameView } from '../../shared/models/game-view';
import { PlayerView } from '../../shared/models/player-view';

@Component({
    selector: 'app-game',
    templateUrl: './game.component.html',
    styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {
    GameId: number;
    Game: GameView = new GameView();

    BetValidationMessage: string;
    BetValidationError: boolean = false;
    Bet: number = 50;
    RoundResult: string;
    GameResult: string;

    BetInput: boolean = false;
    TakeCard: boolean = false;
    BlackJackDangerChoice: boolean = false;
    EndRound: boolean = false;
    EndGame: boolean = false;

    constructor(
        private _route: ActivatedRoute,
        private _router: Router,
        private _httpService: HttpService,
        private _errorService: ErrorService
    ) { }

    ngOnInit() {
        this._route.params.subscribe(params => {
            this.GameId = params['Id'];
            this.GetGame();
        });
    }

    GamePlayInitializer() {
        if (this.Game.Stage == 0) {
            this.GamePlayBetInput();
        }

        if (this.Game.Stage == 1) {
            this.ResumeGameAfterRoundFirstPhase();
        }

        if (this.Game.Stage == 2) {
            this.ResumeGameAfterRoundSecondPhase();
        }
    }

    GetGame() {
        this._httpService.GetGame(this.GameId)
            .subscribe(
                (data) => {
                    this.Game = deserialize(GameView, data);

                    if (data["IsGameOver"] != "") {
                        this.GameResult = data["IsGameOver"];
                        this.GamePlayEndGame();
                    }

                    this.GamePlayInitializer();
                },
                (error) => {
                    console.log(error);
                    this._errorService.SetError(error["error"]["Message"]);
                    this._router.navigate(['/error']);
                }
            );
    }

    ResumeGameAfterRoundFirstPhase() {
        this._httpService.ResumeGameAfterRoundFirstPhase(this.Game.Id)
            .subscribe(
                (data) => {
                    this.Game = deserialize(GameView, data);
                    this.FirstPhaseGamePlay(data["HumanBlackJackAndDealerBlackJackDanger"], data["CanHumanTakeOneMoreCard"]);
                },
                (error) => {
                    console.log(error);
                    this._errorService.SetError(error["error"]["Message"]);
                    this._router.navigate(['/error']);
                }
            );
    }

    ResumeGameAfterRoundSecondPhase() {
        this._httpService.ResumeGameAfterRoundSecondPhase(this.Game.Id)
            .subscribe(
                (data) => {
                    this.Game = deserialize(GameView, data);
                    this.RoundResult = data["RoundResult"];
                    this.GamePlayEndRound();
                },
                (error) => {
                    console.log(error);
                    this._errorService.SetError(error["error"]["Message"]);
                    this._router.navigate(['/error']);
                }
            );
    }

    AddCardToHuman(takeCard: boolean) {
        if (takeCard) {
            this._httpService.AddCardToHuman(this.Game.Id)
                .subscribe(
                (data) => {
                    if (data["CanHumanTakeOneMoreCard"]) {
                        this.Game.Human = deserialize(PlayerView, data);
                    }

                    if (!data["CanHumanTakeOneMoreCard"]) {
                        this.DoRoundSecondPhase(false);
                    }
                },
                (error) => {
                    console.log(error);
                    this._errorService.SetError(error["error"]["Message"]);
                    this._router.navigate(['/error']);
                }
                );
        }

        if (!takeCard) {
            this.DoRoundSecondPhase(false);
        }
    }

    FirstPhaseGamePlay(humanBlackJackAndDealerBlackJackDanger: boolean, canHumanTakeOneMoreCard: boolean) {
        this.Game.Stage = 1;
        this.BetValidationError = false;
        if (humanBlackJackAndDealerBlackJackDanger) {
            this.GamePlayBlackJackDangerChoice();
        }
        if (canHumanTakeOneMoreCard) {
            this.GamePlayTakeCard();
        }
        if (!humanBlackJackAndDealerBlackJackDanger && !canHumanTakeOneMoreCard) {
            this.DoRoundSecondPhase(false);
        }
    }

    DoRoundFirstPhase() {
        this._httpService.DoRoundFirstPhase(this.Game.Id, this.Game.Human.GamePlayerId, this.Bet)
            .subscribe(
            (data) => {
                this.Game = deserialize(GameView, data["Data"]);

                if (data["Message"] != "") {
                    this.ShowValidationMessage(data["Message"]);
                }
                if (data["Message"] == "") {
                    this.FirstPhaseGamePlay(data["Data"]["HumanBlackJackAndDealerBlackJackDanger"], data["Data"]["CanHumanTakeOneMoreCard"]);
                }
            },
            (error) => {
                console.log(error);
                this._errorService.SetError(error["error"]["Message"]);
                this._router.navigate(['/error']);
            }
            );
    }

    ShowValidationMessage(validationMessage: string) {
        this.BetValidationError = true;
        this.BetValidationMessage = validationMessage;
    }

    DoRoundSecondPhase(humanBlackJackContinueRound: boolean) {
        this._httpService.DoRoundSecondPhase(this.Game.Id, humanBlackJackContinueRound)
            .subscribe(
                (data) => {
                    this.Game = deserialize(GameView, data);
                    this.RoundResult = data["RoundResult"];
                    this.GamePlayEndRound();
                },
                (error) => {
                    console.log(error);
                    this._errorService.SetError(error["error"]["Message"]);
                    this._router.navigate(['/error']);
                }
            );
    }

    StartNewGame() {
        this._httpService.EndGame(this.Game.Id, this.GameResult)
            .subscribe(
                (data) => {
                    this._router.navigate(['/user/' + this.Game.Human.Name]);
                },
                (error) => {
                    console.log(error);
                    this._errorService.SetError(error["error"]["Message"]);
                    this._router.navigate(['/error']);
                }
            );
    }

    StartNewRound() {
        this._httpService.EndRound(this.Game.Id)
            .subscribe(
                (data) => {
                    this.GetGame();
                },
                (error) => {
                    console.log(error);
                    this._errorService.SetError(error["error"]["Message"]);
                    this._router.navigate(['/error']);
                }
            );
    }

    GamePlayBetInput() {
        this.BetInput = true;
        this.TakeCard = false;
        this.BlackJackDangerChoice = false;
        this.EndRound = false;
        this.EndGame = false;
    }

    GamePlayTakeCard() {
        this.BetInput = false;
        this.TakeCard = true;
        this.BlackJackDangerChoice = false;
        this.EndRound = false;
        this.EndGame = false;
    }

    GamePlayBlackJackDangerChoice() {
        this.BetInput = false;
        this.TakeCard = false;
        this.BlackJackDangerChoice = true;
        this.EndRound = false;
        this.EndGame = false;
    }

    GamePlayEndRound() {
        this.Game.Stage = 2;
        this.BetInput = false;
        this.TakeCard = false;
        this.BlackJackDangerChoice = false;
        this.EndRound = true;
        this.EndGame = false;
    }

    GamePlayEndGame() {
        this.BetInput = false;
        this.TakeCard = false;
        this.BlackJackDangerChoice = false;
        this.EndRound = false;
        this.EndGame = true;
    }
}
