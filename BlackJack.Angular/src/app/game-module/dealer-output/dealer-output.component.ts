import { Component } from '@angular/core';
import { Input } from '@angular/core';

@Component({
    selector: 'app-dealer-output',
    templateUrl: './dealer-output.component.html'
})
export class DealerOutputComponent {
    @Input() Score: number;
    @Input() RoundScore: number;
    @Input() Cards: string[];
    
    RoundFirstPhase: boolean = false;
    RoundSecondPhase: boolean = false;

    constructor() { }

    @Input()
    set GameStage(stage: number) {
        if (stage == 1) {
            this.RoundFirstPhase = true;
            this.RoundSecondPhase = false;
        }

        if (stage == 2) {
            this.RoundFirstPhase = false;
            this.RoundSecondPhase = true;
        }

        if (stage == 0) {
            this.RoundFirstPhase = false;
            this.RoundSecondPhase = false;
        }
    }
}
