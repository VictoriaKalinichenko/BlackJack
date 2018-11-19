export class ResponseStartRoundView {
    roundResult: string;
    human: GamePlayerStartRoundViewItem;
    dealer: GamePlayerStartRoundViewItem;
    bots: GamePlayerStartRoundViewItem[];
}

export class GamePlayerStartRoundViewItem {
    name: string;
    cardScore: number;
    cards: string[];
}