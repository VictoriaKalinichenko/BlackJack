import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class DataService {
    UserName: string;
    
    SetUserName(userName: string) {
        this.UserName = userName;
    }

    GetUserName() {
        return this.UserName;
    }
}
