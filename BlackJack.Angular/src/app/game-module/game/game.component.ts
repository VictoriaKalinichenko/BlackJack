import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { deserialize } from 'json-typescript-mapper';

import { HttpService } from 'app/shared/services/http.service';
import { GameMappingModel } from 'app/shared/mapping-models/game-mapping-model';
import { PlayerMappingModel } from 'app/shared/mapping-models/player-mapping-model';

@Component({
    selector: 'app-game',
    templateUrl: './game.component.html',
    styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {
    gameId: number;
    game: GameMappingModel = new GameMappingModel();

    betValidationMessage: string;
    betValidationError: boolean = false;
    bet: number = 50;
    roundResult: string;
    gameResult: string;

    betInput: boolean = false;
    takeCard: boolean = false;
    blackJackDangerChoice: boolean = false;
    endRound: boolean = false;
    endGame: boolean = false;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private httpService: HttpService
    ) { }

    ngOnInit() {
        this.route.params.subscribe(params => {
            this.gameId = params['Id'];
            this.GetGame();
        });
    }

    GamePlayInitializer() {
        if (this.game.stage == 0) {
            this.GamePlayBetInput();
        }

        if (this.game.stage == 1) {
            this.ResumeAfterStartRound();
        }

        if (this.game.stage == 2) {
            this.ResumeAfterContinueRound();
        }
    }

    GetGame() {
        this.httpService.GetGame(this.gameId)
            .subscribe(
                (data) => {
                    this.game = deserialize(GameMappingModel, data);

                    if (data["IsGameOver"] != "") {
                        this.gameResult = data["IsGameOver"];
                        this.GamePlayEndGame();
                    }

                    this.GamePlayInitializer();
                }
            );
    }

    ResumeAfterStartRound() {
        this.httpService.ResumeAfterStartRound(this.game.id)
            .subscribe(
                (data) => {
                    this.game = deserialize(GameMappingModel, data);
                    this.StartRoundGamePlay(data["BlackJackChoice"], data["CanTakeCard"]);
                }
            );
    }

    ResumeAfterContinueRound() {
        this.httpService.ResumeAfterContinueRound(this.game.id)
            .subscribe(
                (data) => {
                    this.game = deserialize(GameMappingModel, data);
                    this.ContinueRoundGamePlay(data["RoundResult"]);
                }
            );
    }

    AddCard(takeCard: boolean) {
        if (takeCard) {
            this.httpService.AddCard(this.game.id)
                .subscribe(
                (data) => {
                    if (data["CanTakeCard"]) {
                        this.game.human = deserialize(PlayerMappingModel, data);
                    }

                    if (!data["CanTakeCard"]) {
                        this.ContinueRound(false);
                    }
                }
            );
        }

        if (!takeCard) {
            this.ContinueRound(false);
        }
    }

    StartRoundGamePlay(blackJackChoice: boolean, canTakeCard: boolean) {
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
    }

    ContinueRoundGamePlay(roundResult: string) {
        this.roundResult = roundResult;
        this.GamePlayEndRound();
    }

    StartRound() {
        this.httpService.StartRound(this.game.id, this.game.human.gamePlayerId, this.bet)
            .subscribe(
            (data) => {
                this.game = deserialize(GameMappingModel, data["Data"]);

                if (data["Message"] != null) {
                    this.ShowValidationMessage(data["Message"]);
                }
                if (data["Message"] == null) {
                    this.StartRoundGamePlay(data["Data"]["BlackJackChoice"], data["Data"]["CanTakeCard"]);
                }
            }
        );
    }

    ShowValidationMessage(validationMessage: string) {
        this.betValidationError = true;
        this.betValidationMessage = validationMessage;
    }

    ContinueRound(continueRound: boolean) {
        this.httpService.ContinueRound(this.game.id, continueRound)
            .subscribe(
                (data) => {
                    this.game = deserialize(GameMappingModel, data);
                    this.ContinueRoundGamePlay(data["RoundResult"]);
                }
            );
    }

    StartNewGame() {
        this.httpService.EndGame(this.game.id, this.gameResult)
            .subscribe(
                (data) => {
                    this.router.navigate(['/user/' + this.game.human.name]);
                }
            );
    }

    StartNewRound() {
        this.httpService.EndRound(this.game.id)
            .subscribe(
                (data) => {
                    this.GetGame();
                }
            );
    }

    GamePlayBetInput() {
        this.betInput = true;
        this.takeCard = false;
        this.blackJackDangerChoice = false;
        this.endRound = false;
        this.endGame = false;
    }

    GamePlayTakeCard() {
        this.betInput = false;
        this.takeCard = true;
        this.blackJackDangerChoice = false;
        this.endRound = false;
        this.endGame = false;
    }

    GamePlayBlackJackDangerChoice() {
        this.betInput = false;
        this.takeCard = false;
        this.blackJackDangerChoice = true;
        this.endRound = false;
        this.endGame = false;
    }

    GamePlayEndRound() {
        this.game.stage = 2;
        this.betInput = false;
        this.takeCard = false;
        this.blackJackDangerChoice = false;
        this.endRound = true;
        this.endGame = false;
    }

    GamePlayEndGame() {
        this.betInput = false;
        this.takeCard = false;
        this.blackJackDangerChoice = false;
        this.endRound = false;
        this.endGame = true;
    }
}
