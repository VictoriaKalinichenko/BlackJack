import { JsonProperty } from 'json-typescript-mapper';

export class PlayerMappingModel {
    @JsonProperty('Id')
    gamePlayerId: number;

    @JsonProperty('Name')
    name: string;
    
    @JsonProperty('Cards')
    cards: string[];

    constructor() {
        this.gamePlayerId = void 0;
        this.name = void 0;
        this.cards = void 0;
    }
}