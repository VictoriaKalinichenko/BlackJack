import { PlayerViewModel } from '../viewmodels/PlayerViewModel';
import { JsonProperty } from 'json-typescript-mapper';

export class GameViewModel {
    @JsonProperty('Id')
    GameId: number;

    @JsonProperty('Stage')
    Stage: number;

    @JsonProperty({ clazz: PlayerViewModel, name: 'Human' })
    Human: PlayerViewModel;

    @JsonProperty({ clazz: PlayerViewModel, name: 'Dealer' })
    Dealer: PlayerViewModel;

    @JsonProperty({ clazz: PlayerViewModel, name: 'Bots' })
    Bots: PlayerViewModel[];

    constructor() {
        this.GameId = void 0;
        this.Stage = void 0;
        this.Human = void 0;
        this.Dealer = void 0;
        this.Bots = void 0;
    }
}