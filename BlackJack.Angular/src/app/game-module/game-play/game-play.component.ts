import { Component } from '@angular/core';
import { Input } from '@angular/core';

@Component({
    selector: 'app-game-play',
    templateUrl: './game-play.component.html'
})

export class GamePlayComponent {
    @Input() name: string;
    @Input() cardScore: number;
    @Input() cards: string[];
}
