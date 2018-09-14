import { Component, EventEmitter, Input, Output } from '@angular/core';
import { HttpService } from '../../../services/http.service';
import { ErrorService } from '../../../services/error.service';
import { Router } from '@angular/router';
import { MessageViewModel } from '../../../viewmodels/MessageViewModel';
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
        private _httpService: HttpService,
        private _errorService: ErrorService,
        private _router: Router
    ) { }

    EnterBet() {
        this._httpService.BetsCreation(this.GameId, this.HumanGamePlayerId, this.Bet)
            .subscribe(
            (data) => {
                this.ValidationMessage = deserialize(MessageViewModel, data);
                this.OnValidate();
            },
            (error) => {
                console.log(error);
                this._errorService.SetError(error["error"]["Message"]);
                this._router.navigate(['/error']);
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
