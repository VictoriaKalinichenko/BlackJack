import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthorizedUserComponent } from 'app/authorized-user/authorized-user/authorized-user.component';
import { StartpageComponent } from 'app/authorized-user/startpage/startpage.component';

const routes: Routes = [
    {
        path: '',
        component: AuthorizedUserComponent,
        children: [
            {
                path: '',
                component: StartpageComponent
            },
            {
                path: 'game/:Id',
                loadChildren: 'app/authorized-user/game/game.module#GameModule'
            }
        ]
    }
];

@NgModule({
    imports: [
        RouterModule.forChild(routes)
    ],
    exports: [
        RouterModule
    ]
})
export class AuthorizedUserRoutingModule { }
