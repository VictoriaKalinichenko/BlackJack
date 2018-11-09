import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class NewGameService {

    private isNewGame: boolean = false;

    setIsNewGame() {
        this.isNewGame = true;
    }

    getIsNewGame() {
        return this.isNewGame;
    }
}