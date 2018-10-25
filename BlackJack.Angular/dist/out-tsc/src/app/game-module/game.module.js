var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { NgModule } from '@angular/core';
import { SharedModule } from 'app/shared/modules/shared.module';
import { GameRoutingModule } from 'app/game-module/game-routing.module';
import { GameComponent } from 'app/game-module/game/game.component';
import { PlayerOutputComponent } from 'app/game-module/player-output/player-output.component';
import { DealerOutputComponent } from 'app/game-module/dealer-output/dealer-output.component';
import { RoundService } from 'app/shared/services/round.service';
var GameModule = /** @class */ (function () {
    function GameModule() {
    }
    GameModule = __decorate([
        NgModule({
            imports: [
                GameRoutingModule,
                SharedModule
            ],
            declarations: [
                GameComponent,
                PlayerOutputComponent,
                DealerOutputComponent
            ],
            providers: [
                RoundService
            ]
        })
    ], GameModule);
    return GameModule;
}());
export { GameModule };
//# sourceMappingURL=game.module.js.map