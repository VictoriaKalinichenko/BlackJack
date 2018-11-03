import { JsonProperty } from 'json-typescript-mapper';

export class PlayerMappingModel {
    @JsonProperty('Id')
    gamePlayerId: number;

    @JsonProperty('Name')
    name: string;

    @JsonProperty('CardScore')
    cardScore: number;
    
    @JsonProperty('Cards')
    cards: string[];

    constructor() {
        this.gamePlayerId = void 0;
        this.name = void 0;
        this.cardScore = void 0;
        this.cards = void 0;
    }
}