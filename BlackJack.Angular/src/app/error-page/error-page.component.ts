import { Component, OnInit } from '@angular/core';
import { ErrorService } from 'app/shared/services/error.service';

@Component({
  selector: 'app-error-page',
  templateUrl: './error-page.component.html'
})
export class ErrorPageComponent implements OnInit {
    Error: string;

    constructor(
        private _errorService: ErrorService
    ) { }

    ngOnInit() {
        this.Error = this._errorService.GetError();
    }
}
