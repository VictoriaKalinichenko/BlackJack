export class GameMappingModel {
    roundResult: string;
    human: PlayerMappingModel;
    dealer: PlayerMappingModel;
    bots: PlayerMappingModel[];
}

export class PlayerMappingModel {
    name: string;
    cardScore: number;
    cards: string[];
}