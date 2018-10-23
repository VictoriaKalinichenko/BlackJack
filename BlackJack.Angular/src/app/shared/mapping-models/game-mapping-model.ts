import { PlayerMappingModel } from 'app/shared/mapping-models/player-mapping-model';
import { JsonProperty } from 'json-typescript-mapper';

export class GameMappingModel {
    @JsonProperty('Id')
    id: number;

    @JsonProperty('Stage')
    stage: number;

    @JsonProperty({ clazz: PlayerMappingModel, name: 'Human' })
    human: PlayerMappingModel;

    @JsonProperty({ clazz: PlayerMappingModel, name: 'Dealer' })
    dealer: PlayerMappingModel;

    @JsonProperty({ clazz: PlayerMappingModel, name: 'Bots' })
    bots: PlayerMappingModel[];

    constructor() {
        this.id = void 0;
        this.stage = void 0;
        this.human = void 0;
        this.dealer = void 0;
        this.bots = void 0;
    }
}