import { PlayerMappingModel } from 'app/shared/mapping-models/player-mapping-model';
import { JsonProperty } from 'json-typescript-mapper';

export class GameMappingModel {
    @JsonProperty('Id')
    Id: number;

    @JsonProperty('Stage')
    Stage: number;

    @JsonProperty({ clazz: PlayerMappingModel, name: 'Human' })
    Human: PlayerMappingModel;

    @JsonProperty({ clazz: PlayerMappingModel, name: 'Dealer' })
    Dealer: PlayerMappingModel;

    @JsonProperty({ clazz: PlayerMappingModel, name: 'Bots' })
    Bots: PlayerMappingModel[];

    constructor() {
        this.Id = void 0;
        this.Stage = void 0;
        this.Human = void 0;
        this.Dealer = void 0;
        this.Bots = void 0;
    }
}