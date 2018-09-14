import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { DataService } from '../../services/data.service';
import { HttpService } from '../../services/http.service';
import { ErrorService } from '../../services/error.service';
import { AuthPlayerViewModel } from '../../viewmodels/AuthPlayerViewModel';

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
        private _httpService: HttpService,
        private _errorService: ErrorService,
        private _router: Router
    ) { }

    ngOnInit() {
        this.UserName = this._dataService.GetUserName();
        this.AuthUser(this.UserName);
    }

    AuthUser(userName: string) {
        this._httpService.GetAuthorizedPlayer(this.UserName)
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
        this._httpService.CreateNewGame(this.Player.PlayerId, this.AmountOfBots)
            .subscribe(
            (data) => {
                this.GameId = data["GameId"];
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
        this._httpService.ResumeGame(this.Player.PlayerId)
            .subscribe(
                (data) => {
                    this.GameId = data["GameId"];
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
