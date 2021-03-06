import { NgModule } from '@angular/core';
import { GameRoutingModule } from 'app/game-module/game-routing.module';
import { GameComponent } from 'app/game-module/game/game.component';
import { PlayerOutputComponent } from 'app/game-module/player-output/player-output.component';
import { SharedModule } from 'app/shared/modules/shared.module';
import { RoundService } from 'app/shared/services/round.service';

@NgModule({
    imports: [
        GameRoutingModule,
        SharedModule
    ],
    declarations: [
        GameComponent,
        PlayerOutputComponent
    ],
    providers: [
        RoundService
    ]
})
export class GameModule { }
