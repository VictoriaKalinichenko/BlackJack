import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { RoundService } from 'app/shared/services/round.service';
import { StartService } from 'app/shared/services/start.service';

import { GameMappingModel } from 'app/shared/mapping-models/game-mapping-model';
import { StartRoundView } from 'app/shared/models/start-round.view';
import { RestoreRoundView } from 'app/shared/models/restore-round.view';
import { TakeCardRoundView } from 'app/shared/models/take-card-round.view';
import { EndRoundView } from 'app/shared/models/end-round.view';

@Component({
    selector: 'app-game',
    templateUrl: './game.component.html'
})
export class GameComponent implements OnInit {
    gameId: number;
    game: GameMappingModel = new GameMappingModel();
    
    takeCardGamePlay: boolean = false;
    endRoundGamePlay: boolean = false;

    constructor(
        private route: ActivatedRoute,
        private roundService: RoundService,
        private startService: StartService
    ) { }

    ngOnInit() {
        this.route.params.subscribe(params => {
            this.gameId = params['gameId'];
            let isNewGame = params['isNewGame'];

            if (isNewGame == "true") {
                this.startRound();
            }

            if (isNewGame == "false") {
                this.restoreRound();
            }
        });
    }
    
    startRound() {
        this.roundService.startRound(this.gameId)
            .subscribe(
                (data: StartRoundView) => {
                    this.game = Object.assign(new GameMappingModel, data);
                    this.setGamePlay();
                }
            );
    }

    takeCard() {
        this.roundService.takeCard(this.gameId)
            .subscribe(
                (data: TakeCardRoundView) => {
                    this.game.roundResult = data.roundResult;
                    this.game.human = Object.assign(this.game.human, data.human);

                    for (let iterator = 0; iterator < data.bots.length; iterator++) {
                        this.game.bots[iterator] = Object.assign(this.game.bots[iterator], data.bots[iterator]);
                    }

                    this.setGamePlay();
                }
            );
    }

    endRound() {
        this.roundService.endRound(this.gameId)
            .subscribe(
                (data: EndRoundView) => {
                    this.game.roundResult = data.roundResult;
                    this.game.dealer = Object.assign(this.game.dealer, data.dealer);
                    this.setGamePlay();
                }
            );
    }

    restoreRound() {
        this.roundService.restoreRound(this.gameId)
            .subscribe(
                (data: RestoreRoundView) => {
                    this.game = Object.assign(new GameMappingModel, data);
                    this.setGamePlay();
                }
            );
    }

    setGamePlay() {
        if (this.game.roundResult == "Round is in process") {
            this.endRoundGamePlay = false;
            this.takeCardGamePlay = true;
        }

        if (this.game.roundResult != "Round is in process") {
            this.endRoundGamePlay = true;
            this.takeCardGamePlay = false;
        }
    }
}
