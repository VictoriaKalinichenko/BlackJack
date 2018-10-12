import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomepageComponent } from 'app/homepage/homepage.component';
import { ErrorPageComponent } from 'app/error-page/error-page.component';

const appRoutes: Routes = [
    {
        path: '',
        component: HomepageComponent
    },
    {
        path: 'user/:UserName',
        loadChildren: 'app/authorized-user/authorized-user.module#AuthorizedUserModule'
    },
    {
        path: 'error',
        component: ErrorPageComponent
    }
];

@NgModule({
    imports: [
        RouterModule.forRoot(
            appRoutes,
            { useHash: true }
        )
    ],
    exports: [
        RouterModule
    ]
})
export class AppRoutingModule { }
