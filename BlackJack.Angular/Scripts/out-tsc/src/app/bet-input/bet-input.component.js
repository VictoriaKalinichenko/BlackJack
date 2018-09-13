var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { DataService } from '../services/data.service';
import { MessageViewModel } from '../viewmodels/MessageViewModel';
import { deserialize } from 'json-typescript-mapper';
var BetInputComponent = /** @class */ (function () {
    function BetInputComponent(dataService) {
        this.dataService = dataService;
        this.ValidationError = false;
        this.Bet = 50;
        this.BetOut = new EventEmitter();
    }
    BetInputComponent.prototype.EnterBet = function () {
        var _this = this;
        this.dataService.BetsCreation(this.GameId, this.HumanGamePlayerId, this.Bet)
            .subscribe(function (data) {
            _this.ValidationMessage = deserialize(MessageViewModel, data);
            _this.OnValidate();
        }, function (error) {
            console.log(error);
        });
    };
    BetInputComponent.prototype.OnValidate = function () {
        if (this.ValidationMessage.Message != "") {
            this.ValidationError = true;
        }
        if (this.ValidationMessage.Message == "") {
            this.BetOut.emit();
        }
    };
    __decorate([
        Input(),
        __metadata("design:type", Number)
    ], BetInputComponent.prototype, "Score", void 0);
    __decorate([
        Input(),
        __metadata("design:type", Number)
    ], BetInputComponent.prototype, "GameId", void 0);
    __decorate([
        Input(),
        __metadata("design:type", Number)
    ], BetInputComponent.prototype, "HumanGamePlayerId", void 0);
    __decorate([
        Output(),
        __metadata("design:type", Object)
    ], BetInputComponent.prototype, "BetOut", void 0);
    BetInputComponent = __decorate([
        Component({
            selector: 'app-bet-input',
            templateUrl: './bet-input.component.html',
            styleUrls: ['./bet-input.component.css']
        }),
        __metadata("design:paramtypes", [DataService])
    ], BetInputComponent);
    return BetInputComponent;
}());
export { BetInputComponent };
//# sourceMappingURL=bet-input.component.js.map