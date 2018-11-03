import { Component } from '@angular/core';
import { Input } from '@angular/core';

@Component({
  selector: 'app-player-output',
  templateUrl: './player-output.component.html'
})
export class PlayerOutputComponent {
    @Input() name: string;
    @Input() cardScore: number;
    @Input() cards: string[];
}
