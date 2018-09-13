import { Component, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-bj-danger-choice',
  templateUrl: './bj-danger-choice.component.html',
  styleUrls: ['./bj-danger-choice.component.css']
})
export class BjDangerChoiceComponent {
    @Output() TakeAward = new EventEmitter<boolean>();

    constructor() { }

    OnContinue(takeAward: boolean) {
        this.TakeAward.emit(takeAward);
    }
}
