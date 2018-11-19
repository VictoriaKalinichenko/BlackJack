import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { EndRoundView } from '../models/end-round.view';
import { RequestStartRoundView } from '../models/request-start-round.view';
import { ResponseStartRoundView } from '../models/response-start-round.view';
import { TakeCardRoundView } from '../models/take-card-round.view';

@Injectable()
export class RoundService {
    constructor(private httpClient: HttpClient) { }

    startRound(gameId: number, isNewRound: boolean): Observable<ResponseStartRoundView> {
        const request = new RequestStartRoundView();
        request.gameId = gameId;
        request.isNewRound = isNewRound;
        return this.httpClient.post<ResponseStartRoundView>('Round/Start', request);
    }

    takeCard(gameId: number): Observable<TakeCardRoundView> {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this.httpClient.get<TakeCardRoundView>('Round/TakeCard', options);
    }

    endRound(gameId: number): Observable<EndRoundView> {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this.httpClient.get<EndRoundView>('Round/End', options);
    }  
}