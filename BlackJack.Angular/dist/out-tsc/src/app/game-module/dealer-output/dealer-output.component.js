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
var DealerOutputComponent = /** @class */ (function () {
    function DealerOutputComponent() {
        this.roundFirstPhase = false;
        this.roundSecondPhase = false;
    }
    Object.defineProperty(DealerOutputComponent.prototype, "gameStage", {
        set: function (stage) {
            if (stage == 1) {
                this.roundFirstPhase = true;
                this.roundSecondPhase = false;
            }
            if (stage == 2) {
                this.roundFirstPhase = false;
                this.roundSecondPhase = true;
            }
            if (stage == 0) {
                this.roundFirstPhase = false;
                this.roundSecondPhase = false;
            }
        },
        enumerable: true,
        configurable: true
    });
    __decorate([
        Input(),
        __metadata("design:type", Number)
    ], DealerOutputComponent.prototype, "score", void 0);
    __decorate([
        Input(),
        __metadata("design:type", Number)
    ], DealerOutputComponent.prototype, "roundScore", void 0);
    __decorate([
        Input(),
        __metadata("design:type", Array)
    ], DealerOutputComponent.prototype, "cards", void 0);
    __decorate([
        Input(),
        __metadata("design:type", Number),
        __metadata("design:paramtypes", [Number])
    ], DealerOutputComponent.prototype, "gameStage", null);
    DealerOutputComponent = __decorate([
        Component({
            selector: 'app-dealer-output',
            templateUrl: './dealer-output.component.html'
        }),
        __metadata("design:paramtypes", [])
    ], DealerOutputComponent);
    return DealerOutputComponent;
}());
export { DealerOutputComponent };
//# sourceMappingURL=dealer-output.component.js.map