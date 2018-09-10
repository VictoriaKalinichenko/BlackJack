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

    constructor(private router: Router, private dataService: DataService) { }

    authUser(userName: string) {
        this.dataService.setUserName(userName);
        this.router.navigateByUrl('../startpage/startpage.component.ts');
    }
}
