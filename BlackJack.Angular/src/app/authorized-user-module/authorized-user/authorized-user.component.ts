import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { UserNameService } from 'app/shared/services/user-name.service';

@Component({
    selector: 'app-authorized-user',
    templateUrl: './authorized-user.component.html'
})
export class AuthorizedUserComponent implements OnInit {
    userName: string;

    constructor(
        private userNameService: UserNameService,
        private route: ActivatedRoute
    ) { }

    ngOnInit() {
        this.route.params.subscribe(params => {
            this.userName = params['userName']
        });

        this.userNameService.setUserName(this.userName);
    }
}
