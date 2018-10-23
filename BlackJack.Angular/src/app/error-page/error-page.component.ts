import { Component, OnInit } from '@angular/core';
import { ErrorService } from 'app/shared/services/error.service';

@Component({
  selector: 'app-error-page',
  templateUrl: './error-page.component.html'
})
export class ErrorPageComponent implements OnInit {
    error: string;

    constructor(
        private errorService: ErrorService
    ) { }

    ngOnInit() {
        this.error = this.errorService.getError();
    }
}
