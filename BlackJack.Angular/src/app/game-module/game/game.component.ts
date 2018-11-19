import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GameModel, PlayerModel } from 'app/shared/models/game-model';
import { RoundService } from 'app/shared/services/round.service';
import { StartService } from 'app/shared/services/start.service';

@Component({
    selector: 'app-game',
    templateUrl: './game.component.html'
})
export class GameComponent implements OnInit {
    gameId: number;
    humanName: string;
    dealerName: string = "Dealer";
    botNames: string[] = ["Bot0", "Bot1", "Bot2", "Bot3", "Bot4"];
    game: GameModel = new GameModel();
    
    takeCardGamePlay: boolean = false;
    endRoundGamePlay: boolean = false;

    constructor(
        private route: ActivatedRoute,
        private roundService: RoundService,
        private startService: StartService
    ) { }

    ngOnInit() : void {
        this.route.params.subscribe(params => {
            this.gameId = params['gameId'];
            this.humanName = params['userName'];
            let isNewRound: boolean = params['isNewGame'];
            this.startRound(isNewRound);
        });
    }

    startRound(isNewRound: boolean): void {
        this.roundService.startRound(this.gameId, isNewRound)
            .subscribe(
                (data) => {
                    this.game = data as GameModel;
                    this.setGamePlay();
                }
            );
    }

    takeCard(): void {
        this.roundService.takeCard(this.gameId)
            .subscribe(
                (data) => {
                    this.game = data as GameModel;
                    this.setGamePlay();
                }
            );
    }

    endRound(): void {
        this.roundService.endRound(this.gameId)
            .subscribe(
                (data) => {
                    this.game.roundResult = data.roundResult;
                    this.game.dealer = data.dealer as PlayerModel;
                    this.setGamePlay();
                }
            );
    }

    setGamePlay(): void {
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
