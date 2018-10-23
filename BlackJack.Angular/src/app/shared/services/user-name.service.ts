import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserNameService {
    userName: string;
    
    setUserName(userName: string) {
        this.userName = userName;
    }

    getUserName() {
        return this.userName;
    }
}
