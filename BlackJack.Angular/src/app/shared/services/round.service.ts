import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { RestoreRoundView } from 'app/shared/models/restore-round.view';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable()
export class RoundService {
    constructor(private httpClient: HttpClient) { }
    
    startRound(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this.httpClient.get('Round/Start', options);
    }

    endRound(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this.httpClient.get('Round/End', options);
    }

    takeCard(gameId: number) {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this.httpClient.get('Round/TakeCard', options);
    }

    restoreRound(gameId: number): Observable<RestoreRoundView> {
        const options = gameId ?
            { params: new HttpParams().set('gameId', gameId.toString()) } : {};

        return this.httpClient.get<RestoreRoundView>('Round/Restore', options);
    }
    
}