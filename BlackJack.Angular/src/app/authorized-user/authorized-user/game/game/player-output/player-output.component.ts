import { Component } from '@angular/core';
import { Input } from '@angular/core';

@Component({
  selector: 'app-player-output',
  templateUrl: './player-output.component.html',
  styleUrls: ['./player-output.component.css']
})
export class PlayerOutputComponent {
    @Input() Score: number;
    @Input() RoundScore: number;
    @Input() Bet: number;
    @Input() Cards: string[];

    RoundStart: boolean = true;
    
    @Input()
    set GameStage (stage: number) {
        this.RoundStart = (stage == 0);
    }
}
