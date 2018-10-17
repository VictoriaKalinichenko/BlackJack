import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { AuthorizedUserComponent } from 'app/authorized-user-module/authorized-user/authorized-user.component';
import { StartPageComponent } from 'app/authorized-user-module/start-page/start-page.component';

const routes: Routes = [
    {
        path: '',
        component: AuthorizedUserComponent,
        children: [
            {
                path: '',
                component: StartPageComponent
            },
            {
                path: 'game/:Id',
                loadChildren: 'app/game-module/game.module#GameModule'
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
