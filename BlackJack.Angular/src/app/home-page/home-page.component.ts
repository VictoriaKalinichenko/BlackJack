import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'app-home-page',
    templateUrl: './home-page.component.html'
})
export class HomePageComponent {
    UserName: string;

    constructor(
        private _router: Router
    ) { }

    AuthUser() {
        this._router.navigate(['/user', this.UserName]);
    }
}
