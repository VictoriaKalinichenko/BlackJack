import { Component } from '@angular/core';
import { Input } from '@angular/core';

@Component({
  selector: 'app-player-output',
  templateUrl: './player-output.component.html'
})
export class PlayerOutputComponent {
    @Input() score: number;
    @Input() roundScore: number;
    @Input() bet: number;
    @Input() cards: string[];

    roundStart: boolean = true;
    
    @Input()
    set gameStage (stage: number) {
        this.roundStart = (stage == 0);
    }
}
