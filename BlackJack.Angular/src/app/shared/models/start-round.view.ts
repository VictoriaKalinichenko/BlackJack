import { GamePlayerStartRoundViewItem } from 'app/shared/models/game-player-start-round-view-item';

export class StartRoundView {
    roundResult: string;
    human: GamePlayerStartRoundViewItem;
    dealer: GamePlayerStartRoundViewItem;
    bots: GamePlayerStartRoundViewItem[];
}