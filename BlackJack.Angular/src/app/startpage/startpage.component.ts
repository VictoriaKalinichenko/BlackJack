import { Component } from '@angular/core';
import { DataService } from '../services/data.service';
import { AuthPlayerViewModel } from '../viewmodels/AuthPlayerViewModel';

@Component({
  selector: 'app-startpage',
  templateUrl: './startpage.component.html',
  styleUrls: ['./startpage.component.css']
})
export class StartpageComponent {
    Player: AuthPlayerViewModel;

    constructor(private dataService: DataService) { }

    authUser(userName: string) {
        this.dataService.postData()
            .subscribe(
                (data: AuthPlayerViewModel) => {
                    this.Player = data;
                },
                (error) => {
                    console.log(error);
                }
            );
    }
}
