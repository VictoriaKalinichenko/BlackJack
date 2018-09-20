import { PlayerView } from '../models/player-view';
import { JsonProperty } from 'json-typescript-mapper';

export class GameView {
    @JsonProperty('Id')
    Id: number;

    @JsonProperty('Stage')
    Stage: number;

    @JsonProperty({ clazz: PlayerView, name: 'Human' })
    Human: PlayerView;

    @JsonProperty({ clazz: PlayerView, name: 'Dealer' })
    Dealer: PlayerView;

    @JsonProperty({ clazz: PlayerView, name: 'Bots' })
    Bots: PlayerView[];

    constructor() {
        this.Id = void 0;
        this.Stage = void 0;
        this.Human = void 0;
        this.Dealer = void 0;
        this.Bots = void 0;
    }
}