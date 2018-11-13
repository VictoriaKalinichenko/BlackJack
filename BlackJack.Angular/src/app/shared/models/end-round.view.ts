export class EndRoundView {
    roundResult: string;
    dealer: GamePlayerEndRoundViewItem;
}

export class GamePlayerEndRoundViewItem {
    cardScore: number;
    cards: string[];
}