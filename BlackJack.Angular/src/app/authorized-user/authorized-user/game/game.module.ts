import { NgModule } from '@angular/core';
import { SharedModule } from '../../../shared/modules/shared.module';
import { GameRoutingModule } from './game-routing.module';

import { GameComponent } from './game/game.component';
import { PlayerOutputComponent } from './game/player-output/player-output.component';
import { DealerOutputComponent } from './game/dealer-output/dealer-output.component';

@NgModule({
    imports: [
        GameRoutingModule,
        SharedModule
    ],
    declarations: [
        GameComponent,
        PlayerOutputComponent,
        DealerOutputComponent
    ]
})
export class GameModule { }
