import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { deserialize } from 'json-typescript-mapper';

import { RoundService } from 'app/shared/services/round.service';
import { StartService } from 'app/shared/services/start.service';

import { GameMappingModel } from 'app/shared/mapping-models/game-mapping-model';
import { PlayerMappingModel } from 'app/shared/mapping-models/player-mapping-model';

@Component({
    selector: 'app-game',
    templateUrl: './game.component.html'
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
        private roundService: RoundService,
        private startService: StartService
    ) { }

    ngOnInit() {
        this.route.params.subscribe(params => {
            this.gameId = params['Id'];
            this.getGame();
        });
    }

    gamePlayInitializer() {
        if (this.game.stage == 0) {
            this.gamePlayBetInput();
        }

        if (this.game.stage == 1) {
            this.resumeAfterStartRound();
        }

        if (this.game.stage == 2) {
            this.resumeAfterContinueRound();
        }
    }

    getGame() {
        this.startService.getGame(this.gameId)
            .subscribe(
                (data) => {
                    this.game = deserialize(GameMappingModel, data);

                    if (data["IsGameOver"] != "") {
                        this.gameResult = data["IsGameOver"];
                        this.gamePlayEndGame();
                    }

                    this.gamePlayInitializer();
                }
            );
    }

    resumeAfterStartRound() {
        this.roundService.resumeAfterStartRound(this.game.id)
            .subscribe(
                (data) => {
                    this.game = deserialize(GameMappingModel, data);
                    this.startRoundGamePlay(data["BlackJackChoice"], data["CanTakeCard"]);
                }
            );
    }

    resumeAfterContinueRound() {
        this.roundService.resumeAfterContinueRound(this.game.id)
            .subscribe(
                (data) => {
                    this.game = deserialize(GameMappingModel, data);
                    this.continueRoundGamePlay(data["RoundResult"]);
                }
            );
    }

    addCard(takeCard: boolean) {
        if (takeCard) {
            this.roundService.addCard(this.game.id)
                .subscribe(
                (data) => {
                    if (data["CanTakeCard"]) {
                        this.game.human = deserialize(PlayerMappingModel, data);
                    }

                    if (!data["CanTakeCard"]) {
                        this.continueRound(false);
                    }
                }
            );
        }

        if (!takeCard) {
            this.continueRound(false);
        }
    }

    startRoundGamePlay(blackJackChoice: boolean, canTakeCard: boolean) {
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
    }

    continueRoundGamePlay(roundResult: string) {
        this.roundResult = roundResult;
        this.gamePlayEndRound();
    }

    startRound() {
        this.roundService.startRound(this.game.id, this.game.human.gamePlayerId, this.bet)
            .subscribe(
            (data) => {
                this.game = deserialize(GameMappingModel, data["Data"]);

                if (data["Message"] != null) {
                    this.showValidationMessage(data["Message"]);
                }
                if (data["Message"] == null) {
                    this.startRoundGamePlay(data["Data"]["BlackJackChoice"], data["Data"]["CanTakeCard"]);
                }
            }
        );
    }

    showValidationMessage(validationMessage: string) {
        this.betValidationError = true;
        this.betValidationMessage = validationMessage;
    }

    continueRound(continueRound: boolean) {
        this.roundService.continueRound(this.game.id, continueRound)
            .subscribe(
                (data) => {
                    this.game = deserialize(GameMappingModel, data);
                    this.continueRoundGamePlay(data["RoundResult"]);
                }
            );
    }

    startNewGame() {
        this.roundService.endGame(this.game.id, this.gameResult)
            .subscribe(
                (data) => {
                    this.router.navigate(['/user/' + this.game.human.name]);
                }
            );
    }

    startNewRound() {
        this.roundService.endRound(this.game.id)
            .subscribe(
                (data) => {
                    this.getGame();
                }
            );
    }

    gamePlayBetInput() {
        this.betInput = true;
        this.takeCard = false;
        this.blackJackDangerChoice = false;
        this.endRound = false;
        this.endGame = false;
    }

    gamePlayTakeCard() {
        this.betInput = false;
        this.takeCard = true;
        this.blackJackDangerChoice = false;
        this.endRound = false;
        this.endGame = false;
    }

    gamePlayBlackJackDangerChoice() {
        this.betInput = false;
        this.takeCard = false;
        this.blackJackDangerChoice = true;
        this.endRound = false;
        this.endGame = false;
    }

    gamePlayEndRound() {
        this.game.stage = 2;
        this.betInput = false;
        this.takeCard = false;
        this.blackJackDangerChoice = false;
        this.endRound = true;
        this.endGame = false;
    }

    gamePlayEndGame() {
        this.betInput = false;
        this.takeCard = false;
        this.blackJackDangerChoice = false;
        this.endRound = false;
        this.endGame = true;
    }
}
