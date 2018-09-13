import { Component, OnInit } from '@angular/core';
import { Input } from '@angular/core';
import { PlayerViewModel } from '../viewmodels/PlayerViewModel';

@Component({
  selector: 'app-player-round-start',
  templateUrl: './player-round-start.component.html',
  styleUrls: ['./player-round-start.component.css']
})
export class PlayerRoundStartComponent implements OnInit {
    @Input() PlayerViewModel: PlayerViewModel;

    constructor() { }

    ngOnInit() {
    }
}
