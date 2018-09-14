import { Component, OnInit } from '@angular/core';
import { ErrorService } from '../services/error.service';

@Component({
  selector: 'app-error-page',
  templateUrl: './error-page.component.html',
  styleUrls: ['./error-page.component.css']
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
