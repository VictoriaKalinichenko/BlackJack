export class GameModel {
    roundResult: string;
    human: PlayerModel;
    dealer: PlayerModel;
    bots: PlayerModel[];
}

export class PlayerModel {
    cardScore: number;
    cards: string[];
}