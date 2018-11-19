import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { StartService } from 'app/shared/services/start.service';

@Component({
    selector: 'app-create-game',
    templateUrl: './create-game.component.html'
})
export class CreateGameComponent implements OnInit {
    userName: string;
    amountOfBots: number = 0;

    constructor (
        private startService: StartService,
        private route: ActivatedRoute,
        private router: Router
    ) { }

    ngOnInit(): void {
        this.route.params.subscribe(params => {
            this.userName = params['userName'];
        });
    }

    createGame(): void {
        this.startService.createGame(this.userName, this.amountOfBots)
            .subscribe(
            (data) => {
                this.router.navigate([`/game/${this.userName}/${data}/${true}`]);
            }
        );
    }
}
