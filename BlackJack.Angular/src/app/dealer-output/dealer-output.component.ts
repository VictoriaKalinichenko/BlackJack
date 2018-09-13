import { Component, OnInit } from '@angular/core';
import { Input } from '@angular/core';
import { deserialize } from 'json-typescript-mapper';
import { PlayerViewModel } from '../viewmodels/PlayerViewModel';
import { DataService } from '../services/data.service';

@Component({
    selector: 'app-dealer-output',
    templateUrl: './dealer-output.component.html',
    styleUrls: ['./dealer-output.component.css']
})
export class DealerOutputComponent implements OnInit {
    @Input() PlayerViewModel: PlayerViewModel;
    @Input() GameStage: number;
    
    RoundFirstPhase: boolean = false;
    RoundSecondPhase: boolean = false;

    constructor(
        private dataService: DataService
    ) { }

    ngOnInit() {
        if (this.GameStage == 1) {
            this.RoundFirstPhase = true;
            this.RoundSecondPhase = false;
            this.dataService.GetDealerFirstPhase(this.PlayerViewModel.GamePlayerId)
                .subscribe(
                    (data) => {
                        this.PlayerViewModel = deserialize(PlayerViewModel, data);
                    },
                    (error) => {
                        console.log(error);
                    }
                );
        }

        if (this.GameStage == 2) {
            this.RoundFirstPhase = false;
            this.RoundSecondPhase = true;
            this.dataService.GetDealerSecondPhase(this.PlayerViewModel.GamePlayerId)
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
