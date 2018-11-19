import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CreateGameComponent } from 'app/create-game/create-game.component';
import { ErrorPageComponent } from 'app/error-page/error-page.component';
import { HomePageComponent } from 'app/home-page/home-page.component';

const appRoutes: Routes = [
    {
        path: '',
        component: HomePageComponent
    },
    {
        path: 'create/:userName',
        component: CreateGameComponent
    },
    {
        path: 'game/:userName/:gameId/:isNewGame',
        loadChildren: 'app/game-module/game.module#GameModule'
    },
    {
        path: 'error/:message',
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
