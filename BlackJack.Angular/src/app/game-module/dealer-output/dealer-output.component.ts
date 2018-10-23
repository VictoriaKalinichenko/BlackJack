import { Component } from '@angular/core';
import { Input } from '@angular/core';

@Component({
    selector: 'app-dealer-output',
    templateUrl: './dealer-output.component.html'
})
export class DealerOutputComponent {
    @Input() score: number;
    @Input() roundScore: number;
    @Input() cards: string[];
    
    roundFirstPhase: boolean = false;
    roundSecondPhase: boolean = false;

    constructor() { }

    @Input()
    set gameStage(stage: number) {
        if (stage == 1) {
            this.roundFirstPhase = true;
            this.roundSecondPhase = false;
        }

        if (stage == 2) {
            this.roundFirstPhase = false;
            this.roundSecondPhase = true;
        }

        if (stage == 0) {
            this.roundFirstPhase = false;
            this.roundSecondPhase = false;
        }
    }
}
