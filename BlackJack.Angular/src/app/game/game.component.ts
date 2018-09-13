import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { deserialize } from 'json-typescript-mapper';

import { GameViewModel } from '../viewmodels/GameViewModel';
import { DataService } from '../services/data.service';
import { forEach } from '@angular/router/src/utils/collection';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent implements OnInit {
    GameId: number;
    GameViewModel: GameViewModel;

    BetInput: boolean = false;
    TakeCard: boolean = false;
    BjDangerChoice: boolean = false;
    EndRound: boolean = false;
    NewGame: boolean = false;

    constructor(
        private route: ActivatedRoute,
        private dataService: DataService
    ) { }

    ngOnInit() {
        this.route.params.subscribe(params => {
            this.GameId = params['Id'];
            this.GetGame();
        });
    }

    GamePlayInitializer() {
        if (this.GameViewModel.Stage == 0) {
            this.GamePlayBetInput();
        }

        if (this.GameViewModel.Stage == 1) {
            this.GamePlayTakeCard();
        }
    }

    GetGame() {
        this.dataService.GetGame(this.GameId)
            .subscribe(
                (data) => {
                    this.GameViewModel = deserialize(GameViewModel, data);
                    this.GamePlayInitializer();
                },
                (error) => {
                    console.log(error);
                }
            );
    }

    OnBetsCreation() {
        this.dataService.RoundStart(this.GameId)
            .subscribe(
                (data) => {
                    this.GetGame();
                },
                (error) => {
                    console.log(error);
                }
            )
    }

    GamePlayBetInput() {
        this.BetInput = true;
        this.TakeCard = false;
        this.BjDangerChoice = false;
        this.EndRound = false;
        this.NewGame = false;
    }

    GamePlayTakeCard() {
        this.BetInput = false;
        this.TakeCard = true;
        this.BjDangerChoice = false;
        this.EndRound = false;
        this.NewGame = false;
    }

    GamePlayBjDangerChoice() {
        this.BetInput = false;
        this.TakeCard = false;
        this.BjDangerChoice = true;
        this.EndRound = false;
        this.NewGame = false;
    }

    GamePlayEndRound() {
        this.BetInput = false;
        this.TakeCard = false;
        this.BjDangerChoice = false;
        this.EndRound = true;
        this.NewGame = false;
    }

    GamePlayNewGame() {
        this.BetInput = false;
        this.TakeCard = false;
        this.BjDangerChoice = false;
        this.EndRound = false;
        this.NewGame = true;
    }
}
