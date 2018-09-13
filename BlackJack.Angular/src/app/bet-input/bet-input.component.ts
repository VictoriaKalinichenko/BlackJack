import { Component, EventEmitter, Input, Output } from '@angular/core';
import { DataService } from '../services/data.service';
import { MessageViewModel } from '../viewmodels/MessageViewModel';
import { deserialize } from 'json-typescript-mapper';

@Component({
  selector: 'app-bet-input',
  templateUrl: './bet-input.component.html',
  styleUrls: ['./bet-input.component.css']
})
export class BetInputComponent {
    @Input() Score: number;
    @Input() GameId: number;
    @Input() HumanGamePlayerId: number;
    ValidationMessage: MessageViewModel;
    ValidationError: boolean = false;
    Bet: number = 50;

    @Output() BetOut = new EventEmitter();
    
    constructor(
        private dataService: DataService
    ) { }

    EnterBet() {
        this.dataService.BetsCreation(this.GameId, this.HumanGamePlayerId, this.Bet)
            .subscribe(
                (data) => {
                    this.ValidationMessage = deserialize(MessageViewModel, data);
                    this.OnValidate();
                },
                (error) => {
                    console.log(error);
                }
            );
    }

    OnValidate() {
        if (this.ValidationMessage.Message != "") {
            this.ValidationError = true;
        }

        if (this.ValidationMessage.Message == "") {
            this.BetOut.emit();
        }
    }
}
