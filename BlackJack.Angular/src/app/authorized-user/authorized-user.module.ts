import { NgModule } from '@angular/core';
import { SharedModule } from '../shared/modules/shared.module';
import { AuthorizedUserRoutingModule } from './authorized-user-routing.module';

import { AuthorizedUserComponent } from './authorized-user/authorized-user.component';
import { StartpageComponent } from './authorized-user/startpage/startpage.component';

@NgModule({
    declarations: [
        AuthorizedUserComponent,
        StartpageComponent
    ],
    imports: [
        AuthorizedUserRoutingModule,
        SharedModule
    ]
})
export class AuthorizedUserModule { }
