import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'app-home-page',
    templateUrl: './home-page.component.html'
})
export class HomePageComponent {
    userName: string;

    constructor(
        private router: Router
    ) { }

    AuthUser() {
        this.router.navigate(['/user', this.userName]);
    }
}
