import { NgModule } from '@angular/core';
import { SharedModule } from 'app/shared/modules/shared.module';
import { GameRoutingModule } from 'app/game-module/game-routing.module';

import { GameComponent } from 'app/game-module/game/game.component';
import { PlayerOutputComponent } from 'app/game-module/player-output/player-output.component';
import { DealerOutputComponent } from 'app/game-module/dealer-output/dealer-output.component';

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
