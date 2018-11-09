import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { StartService } from 'app/shared/services/start.service';
import { NewGameService } from 'app/shared/services/new-game.service';

@Component({
    selector: 'app-create-game',
    templateUrl: './create-game.component.html'
})
export class CreateGameComponent implements OnInit {
    userName: string;
    amountOfBots: number = 0;

    constructor (
        private startService: StartService,
        private newGameService: NewGameService,
        private route: ActivatedRoute,
        private router: Router
    ) { }

    ngOnInit() {
        this.route.params.subscribe(params => {
            this.userName = params['userName'];
        });
    }

    createGame() {
        this.startService.createGame(this.userName, this.amountOfBots)
            .subscribe(
            (data) => {
                this.newGameService.setIsNewGame(); 
                this.router.navigate(['/game', data]);
            }
        );
    }
}
