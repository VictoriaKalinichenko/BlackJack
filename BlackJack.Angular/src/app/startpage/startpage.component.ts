import { Component, OnInit } from '@angular/core';
import { DataService } from '../services/data.service';
import { AuthPlayerViewModel } from '../viewmodels/AuthPlayerViewModel';

@Component({
  selector: 'app-startpage',
  templateUrl: './startpage.component.html',
  styleUrls: ['./startpage.component.css']
})
export class StartpageComponent implements OnInit {
    UserName: string;
    Player: AuthPlayerViewModel;

    constructor (
        private dataService: DataService
    ) { }

    ngOnInit() {
        this.UserName = this.dataService.GetUserName();
        this.AuthUser(this.UserName);
    }

    AuthUser(userName: string) {
        this.dataService.PostData()
            .subscribe(
                (data: AuthPlayerViewModel) => {
                    this.Player = data;
                },
                (error) => {
                    console.log(error);
                }
            );
    }

    StartNewGame(playerId: number) {

    }
}
