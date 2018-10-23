import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { UserNameService } from 'app/shared/services/user-name.service';
import { HttpService } from 'app/shared/services/http.service';
import { AuthorizePlayerStartView } from 'app/shared/view-models/authorize-player-start-view';

@Component({
  selector: 'app-start-page',
  templateUrl: './start-page.component.html',
  styleUrls: ['./start-page.component.css']
})
export class StartPageComponent implements OnInit {
    userName: string;
    player: AuthorizePlayerStartView = new AuthorizePlayerStartView();
    amountOfBots: number = 0;
    gameId: number;

    constructor (
        private userNameService: UserNameService,
        private httpService: HttpService,
        private router: Router
    ) { }

    ngOnInit() {
        this.userName = this.userNameService.GetUserName();
        this.AuthUser(this.userName);
    }

    AuthUser(userName: string) {
        this.httpService.GetAuthorizedPlayer(this.userName)
            .subscribe(
            (data) => {
                this.player.name = data["Name"];
                this.player.playerId = data["PlayerId"];
                this.player.resumeGame = data["ResumeGame"];
            }
        );
    }

    StartNewGame() {
        this.httpService.CreateGame(this.player.playerId, this.amountOfBots)
            .subscribe(
            (data) => {
                this.gameId = data["GameId"];
                this.router.navigate(['/user/' + this.userName + '/game/' + this.gameId]);
            }
        );
    }

    ResumeGame() {
        this.httpService.ResumeGame(this.player.playerId)
            .subscribe(
                (data) => {
                    this.gameId = data["GameId"];
                    this.router.navigate(['/user/' + this.userName + '/game/' + this.gameId]);
                }
            );
    }
}
