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

    takeCard: boolean = false;
    endRound: boolean = false;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private roundService: RoundService,
        private startService: StartService
    ) { }

    ngOnInit() {
        this.route.params.subscribe(params => {
            this.gameId = params['Id'];
            this.initializeRound();
        });
    }

    initializeRound() {
        this.startService.initializeRound(this.gameId)
            .subscribe(
                (data) => {
                    this.game = deserialize(GameMappingModel, data);
                    this.restoreRound();
                }
            );
    }

    startRound() {
        this.roundService.startRound(this.game.id)
            .subscribe(
                (data) => {
                    this.game = deserialize(GameMappingModel, data);

                    if (data["CanTakeCard"]) {
                        this.endRound = false;
                        this.takeCard = true;
                    }

                    if (!data["CanTakeCard"]) {
                        this.continueRound();
                    }
                }
            );
    }

    restoreRound() {
        this.roundService.restoreRound(this.game.id)
            .subscribe(
                (data) => {
                    let roundResult = this.game.roundResult;
                    this.game = deserialize(GameMappingModel, data);
                    this.game.roundResult = roundResult;

                    if (data["Human"]["Cards"][0] == null) {
                        this.startRound();
                    }

                    if (!data["CanTakeCard"] && roundResult == "") {
                        this.continueRound();
                    }

                    if (roundResult != "") {
                        this.endRound = true;
                        this.takeCard = false;
                    }

                    if (data["CanTakeCard"]) {
                        this.endRound = false;
                        this.takeCard = true;
                    }
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
                            this.continueRound();
                        }
                    }
                );
        }

        if (!takeCard) {
            this.continueRound();
        }
    }

    continueRound() {
        this.roundService.continueRound(this.game.id)
            .subscribe(
                (data) => {
                    this.game = deserialize(GameMappingModel, data);
                    this.endRound = true;
                    this.takeCard = false;
                }
            );
    }
}
