import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { DataService } from '../services/data.service';

@Component({
    selector: 'app-homepage',
    templateUrl: './homepage.component.html',
    styleUrls: ['./homepage.component.css']
})
export class HomepageComponent {
    UserName: string;

    constructor(
        private dataService: DataService,
        private router: Router
    ) { }

    authUser(userName: string) {
        this.dataService.SetUserName(userName);
        this.router.navigate(['/user']);
    }
}
