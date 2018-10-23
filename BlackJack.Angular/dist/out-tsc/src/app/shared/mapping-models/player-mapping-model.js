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
var PlayerMappingModel = /** @class */ (function () {
    function PlayerMappingModel() {
        this.gamePlayerId = void 0;
        this.name = void 0;
        this.score = void 0;
        this.bet = void 0;
        this.roundScore = void 0;
        this.cards = void 0;
    }
    __decorate([
        JsonProperty('Id'),
        __metadata("design:type", Number)
    ], PlayerMappingModel.prototype, "gamePlayerId", void 0);
    __decorate([
        JsonProperty('Name'),
        __metadata("design:type", String)
    ], PlayerMappingModel.prototype, "name", void 0);
    __decorate([
        JsonProperty('Score'),
        __metadata("design:type", Number)
    ], PlayerMappingModel.prototype, "score", void 0);
    __decorate([
        JsonProperty('Bet'),
        __metadata("design:type", Number)
    ], PlayerMappingModel.prototype, "bet", void 0);
    __decorate([
        JsonProperty('RoundScore'),
        __metadata("design:type", Number)
    ], PlayerMappingModel.prototype, "roundScore", void 0);
    __decorate([
        JsonProperty('Cards'),
        __metadata("design:type", Array)
    ], PlayerMappingModel.prototype, "cards", void 0);
    return PlayerMappingModel;
}());
export { PlayerMappingModel };
//# sourceMappingURL=player-mapping-model.js.map