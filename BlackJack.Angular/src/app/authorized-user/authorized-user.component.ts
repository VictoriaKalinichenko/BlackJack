import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { DataService } from '../shared/services/data.service';

@Component({
    selector: 'app-authorized-user',
    templateUrl: './authorized-user.component.html',
    styleUrls: ['./authorized-user.component.css']
})
export class AuthorizedUserComponent implements OnInit {
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
