import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomepageComponent } from './homepage/homepage.component';
import { ErrorPageComponent } from './error-page/error-page.component';

const appRoutes: Routes = [
    {
        path: '',
        redirectTo: '',
        pathMatch: 'full',
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
