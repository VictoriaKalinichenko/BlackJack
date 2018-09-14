import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { HttpService } from '../../../services/http.service';
import { ErrorService } from '../../../services/error.service';
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
        private _httpService: HttpService,
        private _errorService: ErrorService,
        private _router: Router
    ) { }

    ngOnInit() {
        this._httpService.HumanRoundResult(this.GameId)
            .subscribe(
            (data) => {
                this.RoundResult = data["RoundResult"];
            },
            (error) => {
                console.log(error);
                this._errorService.SetError(error["error"]["Message"]);
                this._router.navigate(['/error']);
            }
        )
    }

    EndRound() {
        this._httpService.UpdateGamePlayersForNewRound(this.GameId)
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
                this._errorService.SetError(error["error"]["Message"]);
                this._router.navigate(['/error']);
            }
            )
    }

    StartNewGame() {
        this._router.navigate(['/user/' + this.UserName]);
    }
}
