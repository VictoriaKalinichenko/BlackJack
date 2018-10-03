import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ErrorService {
    Error: string;

    SetError(error: string) {
        this.Error = error;
    }

    GetError() {
        return this.Error;
    }
}
