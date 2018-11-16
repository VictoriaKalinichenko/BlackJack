import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RestoreRoundView } from 'app/shared/models/restore-round.view';
import { StartRoundView } from '../models/start-round.view';
import { EndRoundView } from '../models/end-round.view';
import { TakeCardRoundView } from '../models/take-card-round.view';

@Injectable()
export class RoundService {
    constructor(private httpClient: HttpClient) { }
    
    startRound(gameId: number): Observable<StartRoundView> {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this.httpClient.get<StartRoundView>('Round/Start', options);
    }

    endRound(gameId: number): Observable<EndRoundView> {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this.httpClient.get<EndRoundView>('Round/End', options);
    }

    takeCard(gameId: number): Observable<TakeCardRoundView> {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this.httpClient.get<TakeCardRoundView>('Round/TakeCard', options);
    }    
}