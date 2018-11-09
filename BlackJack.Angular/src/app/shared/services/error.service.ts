import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ErrorService {
    private error: string;

    setError(error: string) {
        this.error = error;
    }

    getError() {
        return this.error;
    }
}
