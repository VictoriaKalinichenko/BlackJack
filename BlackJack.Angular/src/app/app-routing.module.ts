import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomePageComponent } from 'app/home-page/home-page.component';
import { ErrorPageComponent } from 'app/error-page/error-page.component';

const appRoutes: Routes = [
    {
        path: '',
        component: HomePageComponent
    },
    {
        path: 'user/:UserName',
        loadChildren: 'app/authorized-user-module/authorized-user.module#AuthorizedUserModule'
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
