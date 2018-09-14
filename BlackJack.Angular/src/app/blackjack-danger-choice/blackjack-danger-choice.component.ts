import { Component, Output, EventEmitter } from '@angular/core';

@Component({
    selector: 'app-blackjack-danger-choice',
    templateUrl: './blackjack-danger-choice.component.html',
    styleUrls: ['./blackjack-danger-choice.component.css']
})
export class BlackjackDangerChoiceComponent {
    @Output() TakeAward = new EventEmitter<boolean>();

    constructor() { }

    OnContinue(takeAward: boolean) {
        this.TakeAward.emit(takeAward);
    }
}
