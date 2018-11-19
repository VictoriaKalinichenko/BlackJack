import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GameComponent } from 'app/game-module/game/game.component';

export const routes: Routes = [
    {
        path: '',
        component: GameComponent
    }
];

@NgModule({
    imports: [ RouterModule.forChild(routes) ],
    exports: [ RouterModule ]
})
export class GameRoutingModule { }