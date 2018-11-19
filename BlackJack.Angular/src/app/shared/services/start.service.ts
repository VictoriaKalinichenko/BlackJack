import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CreateGameStartView } from 'app/shared/models/create-game-start.view';
import { SearchGameStartView } from 'app/shared/models/search-game-start.view';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class StartService {
    constructor(private httpClient: HttpClient) { }

    searchGame(userName: string): Observable<SearchGameStartView> {
        const options = userName ?
            { params: new HttpParams().set('userName', userName.toString()) } : {};

        return this.httpClient.get<SearchGameStartView>('Start/SearchGame', options);
    }

    createGame(userName: string, amountOfBots: number): Observable<number> {
        const request = new CreateGameStartView();
        request.userName = userName;
        request.amountOfBots = amountOfBots;
        return this.httpClient.post<number>('Start/CreateGame', request);
    }
}