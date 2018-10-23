var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { tap } from "rxjs/operators";
import { ErrorService } from 'app/shared/services/error.service';
var RequestInterceptor = /** @class */ (function () {
    function RequestInterceptor(errorService, router) {
        this.errorService = errorService;
        this.router = router;
    }
    RequestInterceptor.prototype.intercept = function (request, next) {
        var _this = this;
        return next.handle(request).pipe(tap(function (event) { }, function (error) {
            if (error instanceof HttpErrorResponse) {
                console.log(error);
                _this.errorService.SetError(error["error"]["Message"]);
                _this.router.navigate(['/error']);
            }
        }));
    };
    RequestInterceptor = __decorate([
        Injectable(),
        __metadata("design:paramtypes", [ErrorService,
            Router])
    ], RequestInterceptor);
    return RequestInterceptor;
}());
export { RequestInterceptor };
//# sourceMappingURL=request-interceptor.js.map