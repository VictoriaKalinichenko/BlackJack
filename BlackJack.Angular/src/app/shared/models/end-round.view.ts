import { GamePlayerEndRoundViewItem } from 'app/shared/models/game-player-end-round-view-item';

export class EndRoundView {
    roundResult: string;
    human: GamePlayerEndRoundViewItem;
    dealer: GamePlayerEndRoundViewItem;
    bots: GamePlayerEndRoundViewItem[];
}