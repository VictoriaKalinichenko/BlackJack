var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { GamePlayerItem } from 'app/shared/models/game-player-item';
import { JsonProperty } from 'json-typescript-mapper';
var GameView = /** @class */ (function () {
    function GameView() {
        this.Id = void 0;
        this.Stage = void 0;
        this.Human = void 0;
        this.Dealer = void 0;
        this.Bots = void 0;
    }
    __decorate([
        JsonProperty('Id'),
        __metadata("design:type", Number)
    ], GameView.prototype, "Id", void 0);
    __decorate([
        JsonProperty('Stage'),
        __metadata("design:type", Number)
    ], GameView.prototype, "Stage", void 0);
    __decorate([
        JsonProperty({ clazz: GamePlayerItem, name: 'Human' }),
        __metadata("design:type", GamePlayerItem)
    ], GameView.prototype, "Human", void 0);
    __decorate([
        JsonProperty({ clazz: GamePlayerItem, name: 'Dealer' }),
        __metadata("design:type", GamePlayerItem)
    ], GameView.prototype, "Dealer", void 0);
    __decorate([
        JsonProperty({ clazz: GamePlayerItem, name: 'Bots' }),
        __metadata("design:type", Array)
    ], GameView.prototype, "Bots", void 0);
    return GameView;
}());
export { GameView };
//# sourceMappingURL=game-view.js.map