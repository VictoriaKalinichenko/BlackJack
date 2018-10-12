import { NgModule } from '@angular/core';
import { SharedModule } from 'app/shared/modules/shared.module';
import { GameRoutingModule } from 'app/authorized-user/game/game-routing.module';

import { GameComponent } from 'app/authorized-user/game/game/game.component';
import { PlayerOutputComponent } from 'app/authorized-user/game/player-output/player-output.component';
import { DealerOutputComponent } from 'app/authorized-user/game/dealer-output/dealer-output.component';

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
