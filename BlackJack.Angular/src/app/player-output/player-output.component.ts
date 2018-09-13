import { Component, OnInit } from '@angular/core';
import { Input } from '@angular/core';
import { deserialize } from 'json-typescript-mapper';
import { PlayerViewModel } from '../viewmodels/PlayerViewModel';
import { DataService } from '../services/data.service';

@Component({
  selector: 'app-player-output',
  templateUrl: './player-output.component.html',
  styleUrls: ['./player-output.component.css']
})
export class PlayerOutputComponent implements OnInit {
    @Input() PlayerViewModel: PlayerViewModel;
    @Input() GameStage: number;

    RoundStart: boolean = true;

    constructor(
        private dataService: DataService
    ) { }

    ngOnInit() {
        if (this.GameStage != 0) {
            this.RoundStart = false;
            this.dataService.GetGamePlayer(this.PlayerViewModel.GamePlayerId)
                .subscribe(
                (data) => {
                    this.PlayerViewModel = deserialize(PlayerViewModel, data);
                },
                (error) => {
                    console.log(error);
                }
            );
        }
    }
}
