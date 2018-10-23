import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserNameService {
    userName: string;
    
    SetUserName(userName: string) {
        this.userName = userName;
    }

    GetUserName() {
        return this.userName;
    }
}
