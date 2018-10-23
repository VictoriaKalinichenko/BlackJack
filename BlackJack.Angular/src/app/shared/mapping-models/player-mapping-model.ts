import { JsonProperty } from 'json-typescript-mapper';

export class PlayerMappingModel {
    @JsonProperty('Id')
    gamePlayerId: number;

    @JsonProperty('Name')
    name: string;

    @JsonProperty('Score')
    score: number;

    @JsonProperty('Bet')
    bet: number;

    @JsonProperty('RoundScore')
    roundScore: number;

    @JsonProperty('Cards')
    cards: string[];

    constructor() {
        this.gamePlayerId = void 0;
        this.name = void 0;
        this.score = void 0;
        this.bet = void 0;
        this.roundScore = void 0;
        this.cards = void 0;
    }
}