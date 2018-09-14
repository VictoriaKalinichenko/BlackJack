var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Component, Output, EventEmitter } from '@angular/core';
var TakeCardComponent = /** @class */ (function () {
    function TakeCardComponent() {
        this.TakeCard = new EventEmitter();
    }
    TakeCardComponent.prototype.OnContinue = function (takeCard) {
        this.TakeCard.emit(takeCard);
    };
    __decorate([
        Output(),
        __metadata("design:type", Object)
    ], TakeCardComponent.prototype, "TakeCard", void 0);
    TakeCardComponent = __decorate([
        Component({
            selector: 'app-take-card',
            templateUrl: './take-card.component.html',
            styleUrls: ['./take-card.component.css']
        }),
        __metadata("design:paramtypes", [])
    ], TakeCardComponent);
    return TakeCardComponent;
}());
export { TakeCardComponent };
//# sourceMappingURL=take-card.component.js.map