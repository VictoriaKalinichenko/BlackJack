import { NgModule } from '@angular/core';
import { SharedModule } from 'app/shared/modules/shared.module';
import { GameRoutingModule } from 'app/game-module/game-routing.module';

import { GameComponent } from 'app/game-module/game/game.component';
import { PlayerOutputComponent } from 'app/game-module/player-output/player-output.component';
import { GamePlayComponent } from 'app/game-module/game-play/game-play.component';

import { RoundService } from 'app/shared/services/round.service';

@NgModule({
    imports: [
        GameRoutingModule,
        SharedModule
    ],
    declarations: [
        GameComponent,
        PlayerOutputComponent,
        GamePlayComponent
    ],
    providers: [
        RoundService
    ]
})
export class GameModule { }
