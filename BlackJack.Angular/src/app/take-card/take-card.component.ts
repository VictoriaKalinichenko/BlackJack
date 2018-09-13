import { Component, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-take-card',
  templateUrl: './take-card.component.html',
  styleUrls: ['./take-card.component.css']
})
export class TakeCardComponent {
    @Output() TakeCard = new EventEmitter<boolean>();

    constructor() { }

    OnContinue(takeCard: boolean) {
        this.TakeCard.emit(takeCard);
    }
}