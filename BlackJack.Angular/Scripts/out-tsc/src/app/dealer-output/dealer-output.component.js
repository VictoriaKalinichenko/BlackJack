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
var DealerOutputComponent = /** @class */ (function () {
    function DealerOutputComponent(dataService) {
        this.dataService = dataService;
        this.RoundFirstPhase = false;
        this.RoundSecondPhase = false;
    }
    Object.defineProperty(DealerOutputComponent.prototype, "GameStage", {
        set: function (stage) {
            var _this = this;
            if (stage == 1) {
                this.RoundFirstPhase = true;
                this.RoundSecondPhase = false;
                this.dataService.GetDealerFirstPhase(this.PlayerViewModel.GamePlayerId)
                    .subscribe(function (data) {
                    _this.PlayerViewModel = deserialize(PlayerViewModel, data);
                }, function (error) {
                    console.log(error);
                });
            }
            if (stage == 2) {
                this.RoundFirstPhase = false;
                this.RoundSecondPhase = true;
                this.dataService.GetDealerSecondPhase(this.PlayerViewModel.GamePlayerId)
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
    ], DealerOutputComponent.prototype, "PlayerViewModel", void 0);
    __decorate([
        Input(),
        __metadata("design:type", Number),
        __metadata("design:paramtypes", [Number])
    ], DealerOutputComponent.prototype, "GameStage", null);
    DealerOutputComponent = __decorate([
        Component({
            selector: 'app-dealer-output',
            templateUrl: './dealer-output.component.html',
            styleUrls: ['./dealer-output.component.css']
        }),
        __metadata("design:paramtypes", [DataService])
    ], DealerOutputComponent);
    return DealerOutputComponent;
}());
export { DealerOutputComponent };
//# sourceMappingURL=dealer-output.component.js.map