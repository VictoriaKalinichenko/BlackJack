import { GamePlayerTakeCardRoundViewItem } from 'app/shared/models/game-player-take-card-round-view-item';

export class TakeCardRoundView {
    roundResult: string;
    human: GamePlayerTakeCardRoundViewItem;
    dealer: GamePlayerTakeCardRoundViewItem;
    bots: GamePlayerTakeCardRoundViewItem[];
}