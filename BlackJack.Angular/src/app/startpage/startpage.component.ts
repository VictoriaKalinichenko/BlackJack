import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { DataService } from '../services/data.service';
import { ErrorService } from '../services/error.service';
import { AuthPlayerViewModel } from '../viewmodels/AuthPlayerViewModel';
import { GameIdViewModel } from '../viewmodels/GameIdViewModel';

@Component({
  selector: 'app-startpage',
  templateUrl: './startpage.component.html',
  styleUrls: ['./startpage.component.css']
})
export class StartpageComponent implements OnInit {
    UserName: string;
    Player: AuthPlayerViewModel = new AuthPlayerViewModel();
    AmountOfBots: number = 0;
    GameId: number;

    constructor (
        private _dataService: DataService,
        private _errorService: ErrorService,
        private _router: Router
    ) { }

    ngOnInit() {
        this.UserName = this._dataService.GetUserName();
        this.AuthUser(this.UserName);
    }

    AuthUser(userName: string) {
        this._dataService.GetAuthorizedPlayer()
            .subscribe(
            (data: AuthPlayerViewModel) => {
                this.Player.Name = data.Name;
                this.Player.PlayerId = data.PlayerId;
                this.Player.ResumeGame = data.ResumeGame;
            },
            (error) => {
                console.log(error);
                this._errorService.SetError(error["error"]["Message"]);
                this._router.navigate(['/error']);
            }
        );
    }

    StartNewGame() {
        this._dataService.CreateNewGame(this.Player.PlayerId, this.AmountOfBots)
            .subscribe(
            (data: GameIdViewModel) => {
                this.GameId = data.GameId;
                this._router.navigate(['/user/' + this.UserName + '/game/' + this.GameId]);
            },
            (error) => {
                console.log(error);
                this._errorService.SetError(error["error"]["Message"]);
                this._router.navigate(['/error']);
            }
        );
    }

    ResumeGame() {
        this._dataService.ResumeGame(this.Player.PlayerId)
            .subscribe(
                (data: GameIdViewModel) => {
                    this.GameId = data.GameId;
                    this._router.navigate(['/user/' + this.UserName + '/game/' + this.GameId]);
                },
                (error) => {
                    console.log(error);
                    this._errorService.SetError(error["error"]["Message"]);
                    this._router.navigate(['/error']);
                }
            );
    }
}
