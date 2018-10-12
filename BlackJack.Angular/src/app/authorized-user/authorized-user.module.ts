import { NgModule } from '@angular/core';
import { SharedModule } from 'app/shared/modules/shared.module';
import { AuthorizedUserRoutingModule } from 'app/authorized-user/authorized-user-routing.module';

import { AuthorizedUserComponent } from 'app/authorized-user/authorized-user/authorized-user.component';
import { StartpageComponent } from 'app/authorized-user/startpage/startpage.component';

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
