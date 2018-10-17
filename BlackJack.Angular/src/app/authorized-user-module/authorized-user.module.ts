import { NgModule } from '@angular/core';
import { SharedModule } from 'app/shared/modules/shared.module';
import { AuthorizedUserRoutingModule } from 'app/authorized-user-module/authorized-user-routing.module';

import { AuthorizedUserComponent } from 'app/authorized-user-module/authorized-user/authorized-user.component';
import { StartPageComponent } from 'app/authorized-user-module/start-page/start-page.component';

@NgModule({
    declarations: [
        AuthorizedUserComponent,
        StartPageComponent
    ],
    imports: [
        AuthorizedUserRoutingModule,
        SharedModule
    ]
})
export class AuthorizedUserModule { }
