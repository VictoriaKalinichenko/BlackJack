export class TakeCardRoundView {
    roundResult: string;
    human: GamePlayerTakeCardRoundViewItem;
    dealer: GamePlayerTakeCardRoundViewItem;
    bots: GamePlayerTakeCardRoundViewItem[];
}

export class GamePlayerTakeCardRoundViewItem {
    cardScore: number;
    cards: string[];
}