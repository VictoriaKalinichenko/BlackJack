import { PlayerMappingModel } from 'app/shared/mapping-models/player-mapping-model';
var GameMappingModel = /** @class */ (function () {
    function GameMappingModel() {
        this.human = new PlayerMappingModel();
        this.dealer = new PlayerMappingModel();
        this.bots = [];
    }
    GameMappingModel.prototype.deserialize = function (input) {
        this.dealer = this.dealer.deserialize(input.Dealer);
        this.human = this.human.deserialize(input.Human);
        for (var iterator = 0; iterator < input.Bots.length; iterator++) {
            if (this.bots[iterator] == null) {
                this.bots[iterator] = new PlayerMappingModel();
            }
            this.bots[iterator] = this.bots[iterator].deserialize(input.Bots[iterator]);
        }
        this.roundResult = input.RoundResult;
        return this;
    };
    return GameMappingModel;
}());
export { GameMappingModel };
//# sourceMappingURL=game-mapping-model.js.map