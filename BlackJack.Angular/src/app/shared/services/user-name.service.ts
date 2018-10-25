import { Injectable } from '@angular/core';
import { AuthorizedUserModule } from 'app/authorized-user-module/authorized-user.module';

@Injectable({
    providedIn: AuthorizedUserModule
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
