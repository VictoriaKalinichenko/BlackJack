import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
    selector: 'app-homepage',
    templateUrl: './homepage.component.html'
})
export class HomepageComponent {
    UserName: string;

    constructor(
        private _router: Router
    ) { }

    AuthUser() {
        this._router.navigate(['/user', this.UserName]);
    }
}
