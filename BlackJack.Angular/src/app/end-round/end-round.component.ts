import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { DataService } from '../services/data.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-end-round',
  templateUrl: './end-round.component.html',
  styleUrls: ['./end-round.component.css']
})
export class EndRoundComponent implements OnInit {
    @Input() UserName: string;
    @Input() GameId: number;
    @Output() Reload = new EventEmitter();
    RoundResult: string;
    GameOver: string;
    IsEndRound: boolean = true;
    IsGameOver: boolean = false;

    constructor(
        private dataService: DataService,
        private router: Router
    ) { }

    ngOnInit() {
        this.dataService.HumanRoundResult(this.GameId)
            .subscribe(
            (data) => {
                this.RoundResult = data["RoundResult"];
            },
            (error) => {
                console.log(error);
            }
        )
    }

    EndRound() {
        this.dataService.UpdateGamePlayersForNewRound(this.GameId)
            .subscribe(
                (data) => {
                    if (data["IsGameOver"] != "") {
                        this.IsGameOver = true;
                        this.IsEndRound = false;

                        this.GameOver = data["IsGameOver"];
                    }

                    if (data["IsGameOver"] == "") {
                        this.Reload.emit();
                    }
                },
                (error) => {
                    console.log(error);
                }
            )
    }

    StartNewGame() {
        this.router.navigate(['/user/' + this.UserName]);
    }
}
