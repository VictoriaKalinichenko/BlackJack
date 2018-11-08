import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { deserialize } from 'json-typescript-mapper';

import { RoundService } from 'app/shared/services/round.service';
import { StartService } from 'app/shared/services/start.service';

import { StartRoundView } from 'app/shared/models/start-round.view';
import { TakeCardRoundView } from 'app/shared/models/take-card-round.view';
import { GameMappingModel } from 'app/shared/mapping-models/game-mapping-model';
import { forEach } from '@angular/router/src/utils/collection';

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
                (data: StartRoundView) => {
                    this.game = deserialize(GameMappingModel, data);

                    if (this.game.roundResult == "") {
                        this.endRound = false;
                        this.takeCard = true;
                    }

                    if (this.game.roundResult != "") {
                        this.endRound = true;
                        this.takeCard = false;
                    }
                }
            );
    }

    onTakeCard() {
        this.roundService.takeCard(this.gameId)
            .subscribe(
                (data: TakeCardRoundView) => {
                    let humanName = this.game.human.name;
                    let dealerName = this.game.dealer.name;
                    let botNames: string[] = [];

                    this.game.bots.forEach(function (bot) {
                        botNames.push(bot.name);
                    });

                    this.game = deserialize(GameMappingModel, data);

                    this.game.human.name = humanName;
                    this.game.dealer.name = dealerName;

                    for (let iterator = 0; iterator < botNames.length; iterator++) {
                        this.game.bots[iterator].name = botNames[iterator];
                    }

                    if (this.game.roundResult != "") {
                        this.endRound = true;
                        this.takeCard = false;
                    }
                }
            );
    }

    onContinueRound() {
        this.roundService.endRound(this.gameId)
            .subscribe(
                (data: string) => {
                    this.game.roundResult = data;
                    this.endRound = true;
                    this.takeCard = false;
                }
            );
    }
}
