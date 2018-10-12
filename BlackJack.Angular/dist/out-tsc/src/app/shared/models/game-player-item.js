var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { JsonProperty } from 'json-typescript-mapper';
var GamePlayerItem = /** @class */ (function () {
    function GamePlayerItem() {
        this.GamePlayerId = void 0;
        this.Name = void 0;
        this.Score = void 0;
        this.Bet = void 0;
        this.RoundScore = void 0;
        this.Cards = void 0;
    }
    __decorate([
        JsonProperty('Id'),
        __metadata("design:type", Number)
    ], GamePlayerItem.prototype, "GamePlayerId", void 0);
    __decorate([
        JsonProperty('Name'),
        __metadata("design:type", String)
    ], GamePlayerItem.prototype, "Name", void 0);
    __decorate([
        JsonProperty('Score'),
        __metadata("design:type", Number)
    ], GamePlayerItem.prototype, "Score", void 0);
    __decorate([
        JsonProperty('Bet'),
        __metadata("design:type", Number)
    ], GamePlayerItem.prototype, "Bet", void 0);
    __decorate([
        JsonProperty('RoundScore'),
        __metadata("design:type", Number)
    ], GamePlayerItem.prototype, "RoundScore", void 0);
    __decorate([
        JsonProperty('Cards'),
        __metadata("design:type", Array)
    ], GamePlayerItem.prototype, "Cards", void 0);
    return GamePlayerItem;
}());
export { GamePlayerItem };
//# sourceMappingURL=game-player-item.js.map