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

    searchGame(): void {
        this.startService.searchGame(this.userName)
            .subscribe(
                (data) => {
                    if (data.isGameExist) {
                        this.router.navigate([`/game/${this.userName}/${data.gameId}/${false}`]);
                    }

                    if (!data.isGameExist) {
                        this.router.navigate(['/create', this.userName]);
                    }
                }
            );
    }
}
