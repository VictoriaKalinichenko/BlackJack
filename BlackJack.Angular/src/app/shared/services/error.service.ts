import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ErrorService {
    error: string;

    SetError(error: string) {
        this.error = error;
    }

    GetError() {
        return this.error;
    }
}
