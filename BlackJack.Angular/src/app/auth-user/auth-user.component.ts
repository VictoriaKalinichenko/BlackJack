import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { DataService } from '../services/data.service';

@Component({
  selector: 'app-auth-user',
  templateUrl: './auth-user.component.html',
  styleUrls: ['./auth-user.component.css']
})
export class AuthUserComponent implements OnInit {
    UserName: string;

    constructor(
        private _dataService: DataService,
        private _route: ActivatedRoute
    ) { }

    ngOnInit() {
        this._route.params.subscribe(params => {
            this.UserName = params['UserName']
        });

        this._dataService.SetUserName(this.UserName);
    }
}
