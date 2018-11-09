import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { RoundService } from 'app/shared/services/round.service';
import { StartService } from 'app/shared/services/start.service';
import { NewGameService } from 'app/shared/services/new-game.service';

import { StartRoundView } from 'app/shared/models/start-round.view';
import { TakeCardRoundView } from 'app/shared/models/take-card-round.view';
import { GameMappingModel } from 'app/shared/mapping-models/game-mapping-model';

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
        private startService: StartService,
        private newGameService: NewGameService
    ) { }

    ngOnInit() {
        this.route.params.subscribe(params => {
            this.gameId = params['gameId'];

            let isNewGame = this.newGameService.getIsNewGame();
            if (isNewGame) {
                this.onStartRound();
            }

            if (!isNewGame) {
                this.onRestoreRound();
            }
        });
    }
    
    onStartRound() {
        this.roundService.startRound(this.gameId)
            .subscribe(
                (data: StartRoundView) => {
                    this.game = this.game.deserialize(data);
                    this.setGamePlay(this.game.roundResult);
                }
            );
    }

    onTakeCard() {
        this.roundService.takeCard(this.gameId)
            .subscribe(
                (data: TakeCardRoundView) => {
                    this.game = this.game.deserialize(data);
                    this.setGamePlay(this.game.roundResult);
                }
            );
    }

    onContinueRound() {
        this.roundService.endRound(this.gameId)
            .subscribe(
                (data: string) => {
                    this.game.roundResult = data;
                    this.setGamePlay(this.game.roundResult);
                }
            );
    }

    onRestoreRound() {
        this.roundService.restoreRound(this.gameId)
            .subscribe(
                (data: StartRoundView) => {
                    this.game = this.game.deserialize(data);
                    this.setGamePlay(this.game.roundResult);
                }
            );
    }

    setGamePlay(roundResult: string) {
        if (this.game.roundResult == "") {
            this.endRound = false;
            this.takeCard = true;
        }

        if (this.game.roundResult != "") {
            this.endRound = true;
            this.takeCard = false;
        }
    }
}
