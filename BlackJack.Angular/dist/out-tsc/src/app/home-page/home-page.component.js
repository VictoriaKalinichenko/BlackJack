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
import { Router } from '@angular/router';
import { StartService } from 'app/shared/services/start.service';
var HomePageComponent = /** @class */ (function () {
    function HomePageComponent(router, startService) {
        this.router = router;
        this.startService = startService;
    }
    HomePageComponent.prototype.searchGame = function () {
        var _this = this;
        this.startService.searchGame(this.userName)
            .subscribe(function (data) {
            if (data["IsGameExist"]) {
                _this.router.navigate(['/game/' + data["GameId"] + '/' + false]);
            }
            if (!data["IsGameExist"]) {
                _this.router.navigate(['/create', _this.userName]);
            }
        });
    };
    HomePageComponent = __decorate([
        Component({
            selector: 'app-home-page',
            templateUrl: './home-page.component.html'
        }),
        __metadata("design:paramtypes", [Router,
            StartService])
    ], HomePageComponent);
    return HomePageComponent;
}());
export { HomePageComponent };
//# sourceMappingURL=home-page.component.js.map