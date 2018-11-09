import { Deserializable } from "app/shared/interfaces/deserializable";

export class PlayerMappingModel implements Deserializable {

    name: string;
    cardScore: number;
    cards: string[];

    deserialize(input: any) {

        if (input.Name != null) {
            this.name = input.Name;
        }

        this.cardScore = input.CardScore;
        this.cards = input.Cards;

        return this;
    }
}