export class RestoreRoundView {
    roundResult: string;
    human: GamePlayerRestoreRoundViewItem;
    dealer: GamePlayerRestoreRoundViewItem;
    bots: GamePlayerRestoreRoundViewItem[];
}

export class GamePlayerRestoreRoundViewItem {
    name: string;
    cardScore: number;
    cards: string[];
}