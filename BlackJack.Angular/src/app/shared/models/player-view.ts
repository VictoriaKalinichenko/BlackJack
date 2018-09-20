import { JsonProperty } from 'json-typescript-mapper';

export class PlayerView {
    @JsonProperty('Id')
    GamePlayerId: number;

    @JsonProperty('Name')
    Name: string;

    @JsonProperty('Score')
    Score: number;

    @JsonProperty('Bet')
    Bet: number;

    @JsonProperty('RoundScore')
    RoundScore: number;

    @JsonProperty('Cards')
    Cards: string[];

    constructor() {
        this.GamePlayerId = void 0;
        this.Name = void 0;
        this.Score = void 0;
        this.Bet = void 0;
        this.RoundScore = void 0;
        this.Cards = void 0;
    }
}