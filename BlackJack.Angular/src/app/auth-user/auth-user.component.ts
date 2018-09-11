import { Component, OnInit } from '@angular/core';

import { DataService } from '../services/data.service';

@Component({
  selector: 'app-auth-user',
  templateUrl: './auth-user.component.html',
  styleUrls: ['./auth-user.component.css']
})
export class AuthUserComponent implements OnInit {
    UserName: string;

    constructor(private dataService: DataService) { }

    ngOnInit() {
        this.UserName = this.dataService.GetUserName();
    }
}
