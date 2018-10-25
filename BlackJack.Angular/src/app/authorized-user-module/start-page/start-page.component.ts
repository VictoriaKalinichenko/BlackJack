import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { UserNameService } from 'app/shared/services/user-name.service';
import { StartService } from 'app/shared/services/start.service';
import { AuthorizePlayerStartView } from 'app/shared/view-models/authorize-player-start-view';

@Component({
  selector: 'app-start-page',
  templateUrl: './start-page.component.html'
})
export class StartPageComponent implements OnInit {
    userName: string;
    player: AuthorizePlayerStartView = new AuthorizePlayerStartView();
    amountOfBots: number = 0;
    gameId: number;

    constructor (
        private userNameService: UserNameService,
        private startService: StartService,
        private router: Router
    ) { }

    ngOnInit() {
        this.userName = this.userNameService.getUserName();
        this.authUser(this.userName);
    }

    authUser(userName: string) {
        this.startService.getAuthorizedPlayer(this.userName)
            .subscribe(
            (data) => {
                this.player.name = data["Name"];
                this.player.playerId = data["PlayerId"];
                this.player.resumeGame = data["ResumeGame"];
            }
        );
    }

    startNewGame() {
        this.startService.createGame(this.player.playerId, this.amountOfBots)
            .subscribe(
            (data) => {
                this.gameId = data["GameId"];
                this.router.navigate(['/user/' + this.userName + '/game/' + this.gameId]);
            }
        );
    }

    resumeGame() {
        this.startService.resumeGame(this.player.playerId)
            .subscribe(
                (data) => {
                    this.gameId = data["GameId"];
                    this.router.navigate(['/user/' + this.userName + '/game/' + this.gameId]);
                }
            );
    }
}
