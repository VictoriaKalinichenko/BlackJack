import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DataService {
    UserName: string;

    constructor(private http: HttpClient) { }

    setUserName(userName: string) {
        this.UserName = userName;
    }

    postData() {
        const body = { UserName: this.UserName };
        return this.http.post('http://localhost:55953/StartGame/GetAuthorizedPlayer', body);
    }
}
