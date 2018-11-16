import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { SearchGameStartView } from 'app/shared/models/search-game-start.view';

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
        const body = { UserName: userName, AmountOfBots: amountOfBots };
        return this.httpClient.post<number>('Start/CreateGame', body);
    }
}