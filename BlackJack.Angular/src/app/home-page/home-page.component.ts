import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { StartService } from 'app/shared/services/start.service';

@Component({
    selector: 'app-home-page',
    templateUrl: './home-page.component.html'
})
export class HomePageComponent {
    userName: string;

    constructor(
        private router: Router,
        private startService: StartService
    ) { }

    searchGameForPlayer() {
        this.startService.searchGameForPlayer(this.userName)
            .subscribe(
                (data) => {
                    if (data["IsGameExist"]) {
                        this.router.navigate(['/game', data["GameId"]]);
                    }

                    if (!data["IsGameExist"]) {
                        this.router.navigate(['/create', this.userName]);
                    }
                }
            );

    }
}
