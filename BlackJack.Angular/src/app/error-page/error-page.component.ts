import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-error-page',
  templateUrl: './error-page.component.html'
})
export class ErrorPageComponent implements OnInit {
    error: string;

    constructor(private route: ActivatedRoute) { }

    ngOnInit(): void {
        this.route.params.subscribe(params => {
            this.error = params['message'];
        });
    }
}
