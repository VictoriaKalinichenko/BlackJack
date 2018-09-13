import { Component } from '@angular/core';
import { Input } from '@angular/core';
import { deserialize } from 'json-typescript-mapper';
import { PlayerViewModel } from '../viewmodels/PlayerViewModel';
import { DataService } from '../services/data.service';

@Component({
  selector: 'app-player-output',
  templateUrl: './player-output.component.html',
  styleUrls: ['./player-output.component.css']
})
export class PlayerOutputComponent {
    @Input() PlayerViewModel: PlayerViewModel;

    RoundStart: boolean = true;

    constructor(
        private dataService: DataService
    ) { }

    @Input()
    set GameStage (stage: number) {
        if (stage != 0) {
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
