import { PlayerMappingModel } from 'app/shared/mapping-models/player-mapping-model';
import { Deserializable } from "app/shared/interfaces/deserializable";

export class GameMappingModel implements Deserializable {

    roundResult: string;
    human: PlayerMappingModel = new PlayerMappingModel();
    dealer: PlayerMappingModel = new PlayerMappingModel();
    bots: PlayerMappingModel[] = [];

    deserialize(input: any) {

        this.dealer = this.dealer.deserialize(input.Dealer);
        this.human = this.human.deserialize(input.Human);

        for (let iterator = 0; iterator < input.Bots.length; iterator++) {

            if (this.bots[iterator] == null) {
                this.bots[iterator] = new PlayerMappingModel();
            }

            this.bots[iterator] = this.bots[iterator].deserialize(input.Bots[iterator]);
        }

        this.roundResult = input.RoundResult;

        return this;
    }
}