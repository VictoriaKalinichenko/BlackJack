import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class StartService {
    constructor(private httpClient: HttpClient) { }

    searchGame(userName: string) {
        const options = userName ?
            { params: new HttpParams().set('userName', userName.toString()) } : {};

        return this.httpClient.get('Start/SearchGame', options);
    }

    createGame(userName: string, amountOfBots: number) {
        const body = { UserName: userName, AmountOfBots: amountOfBots };
        return this.httpClient.post('Start/CreateGame', body);
    }
}