import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { GameViewModel } from '../viewmodels/GameViewModel';
import { PlayerViewModel } from '../viewmodels/PlayerViewModel';

@Injectable({
  providedIn: 'root'
})
export class DataService {
    UserName: string;

    constructor(private http: HttpClient) { }

    SetUserName(userName: string) {
        this.UserName = userName;
    }

    GetUserName() {
        return this.UserName;
    }

    GetAuthorizedPlayer() {
        const body = { UserName: this.UserName };
        return this.http.post('http://localhost:55953/StartGame/GetAuthorizedPlayer', body);
    }

    CreateNewGame(playerId: number, amountOfBots: number) {
        const body = { PlayerId: playerId, AmountOfBots: amountOfBots };
        return this.http.post('http://localhost:55953/StartGame/CreateNewGame', body);
    }

    ResumeGame(playerId: number) {
        return this.http.get('http://localhost:55953/StartGame/ResumeGame?playerId=' + playerId);
    }

    GetGame(gameId: number) {
        return this.http.get('http://localhost:55953/StartGame/GetGame?gameId=' + gameId);
    }
}
