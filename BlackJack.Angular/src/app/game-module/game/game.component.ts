import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
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
        private roundService: RoundService,
        private startService: StartService
    ) { }

    ngOnInit() {
        this.route.params.subscribe(params => {
            this.gameId = params['gameId'];
            this.onStartRound();
        });
    }
    
    onStartRound() {
        this.roundService.startRound(this.gameId)
            .subscribe(
                (data) => {
                    this.game = deserialize(GameMappingModel, data);

                    if (data["CanTakeCard"] != null) {
                        this.endRound = false;
                        this.takeCard = true;
                    }

                    if (data["CanTakeCard"] == null) {
                        this.endRound = true;
                        this.takeCard = false;
                    }
                }
            );
    }

    onTakeCard(takeCard: boolean) {
        if (takeCard) {
            this.roundService.takeCard(this.gameId)
                .subscribe(
                    (data) => {
                        if (data["CanTakeCard"] != null) {
                            this.game.human = deserialize(PlayerMappingModel, data);
                        }

                        if (data["CanTakeCard"] == null) {
                            this.game = deserialize(GameMappingModel, data);
                            this.endRound = true;
                            this.takeCard = false;
                        }
                    }
                );
        }

        if (!takeCard) {
            this.onContinueRound();
        }
    }

    onContinueRound() {
        this.roundService.endRound(this.gameId)
            .subscribe(
                (data) => {
                    this.game = deserialize(GameMappingModel, data);
                    this.endRound = true;
                    this.takeCard = false;
                }
            );
    }
}
