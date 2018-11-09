var PlayerMappingModel = /** @class */ (function () {
    function PlayerMappingModel() {
    }
    PlayerMappingModel.prototype.deserialize = function (input) {
        if (input.Name != null) {
            this.name = input.Name;
        }
        this.cardScore = input.CardScore;
        this.cards = input.Cards;
        return this;
    };
    return PlayerMappingModel;
}());
export { PlayerMappingModel };
//# sourceMappingURL=player-mapping-model.js.map