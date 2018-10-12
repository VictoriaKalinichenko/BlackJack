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
import { ErrorService } from 'app/shared/services/error-service/error.service';
var ErrorPageComponent = /** @class */ (function () {
    function ErrorPageComponent(_errorService) {
        this._errorService = _errorService;
    }
    ErrorPageComponent.prototype.ngOnInit = function () {
        this.Error = this._errorService.GetError();
    };
    ErrorPageComponent = __decorate([
        Component({
            selector: 'app-error-page',
            templateUrl: './error-page.component.html'
        }),
        __metadata("design:paramtypes", [ErrorService])
    ], ErrorPageComponent);
    return ErrorPageComponent;
}());
export { ErrorPageComponent };
//# sourceMappingURL=error-page.component.js.map