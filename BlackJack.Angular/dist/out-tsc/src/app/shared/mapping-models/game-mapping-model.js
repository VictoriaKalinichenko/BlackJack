var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { PlayerMappingModel } from 'app/shared/mapping-models/player-mapping-model';
import { JsonProperty } from 'json-typescript-mapper';
var GameMappingModel = /** @class */ (function () {
    function GameMappingModel() {
        this.id = void 0;
        this.roundResult = void 0;
        this.human = void 0;
        this.dealer = void 0;
        this.bots = void 0;
    }
    __decorate([
        JsonProperty('Id'),
        __metadata("design:type", Number)
    ], GameMappingModel.prototype, "id", void 0);
    __decorate([
        JsonProperty('RoundResult'),
        __metadata("design:type", String)
    ], GameMappingModel.prototype, "roundResult", void 0);
    __decorate([
        JsonProperty({ clazz: PlayerMappingModel, name: 'Human' }),
        __metadata("design:type", PlayerMappingModel)
    ], GameMappingModel.prototype, "human", void 0);
    __decorate([
        JsonProperty({ clazz: PlayerMappingModel, name: 'Dealer' }),
        __metadata("design:type", PlayerMappingModel)
    ], GameMappingModel.prototype, "dealer", void 0);
    __decorate([
        JsonProperty({ clazz: PlayerMappingModel, name: 'Bots' }),
        __metadata("design:type", Array)
    ], GameMappingModel.prototype, "bots", void 0);
    return GameMappingModel;
}());
export { GameMappingModel };
//# sourceMappingURL=game-mapping-model.js.map