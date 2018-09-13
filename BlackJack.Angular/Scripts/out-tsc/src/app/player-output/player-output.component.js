var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component } from '@angular/core';
import { Input } from '@angular/core';
import { deserialize } from 'json-typescript-mapper';
import { PlayerViewModel } from '../viewmodels/PlayerViewModel';
import { DataService } from '../services/data.service';
var PlayerOutputComponent = /** @class */ (function () {
    function PlayerOutputComponent(dataService) {
        this.dataService = dataService;
        this.RoundStart = true;
    }
    Object.defineProperty(PlayerOutputComponent.prototype, "GameStage", {
        set: function (stage) {
            var _this = this;
            if (stage != 0) {
                this.RoundStart = false;
                this.dataService.GetGamePlayer(this.PlayerViewModel.GamePlayerId)
                    .subscribe(function (data) {
                    _this.PlayerViewModel = deserialize(PlayerViewModel, data);
                }, function (error) {
                    console.log(error);
                });
            }
        },
        enumerable: true,
        configurable: true
    });
    __decorate([
        Input(),
        __metadata("design:type", PlayerViewModel)
    ], PlayerOutputComponent.prototype, "PlayerViewModel", void 0);
    __decorate([
        Input(),
        __metadata("design:type", Number),
        __metadata("design:paramtypes", [Number])
    ], PlayerOutputComponent.prototype, "GameStage", null);
    PlayerOutputComponent = __decorate([
        Component({
            selector: 'app-player-output',
            templateUrl: './player-output.component.html',
            styleUrls: ['./player-output.component.css']
        }),
        __metadata("design:paramtypes", [DataService])
    ], PlayerOutputComponent);
    return PlayerOutputComponent;
}());
export { PlayerOutputComponent };
//# sourceMappingURL=player-output.component.js.map