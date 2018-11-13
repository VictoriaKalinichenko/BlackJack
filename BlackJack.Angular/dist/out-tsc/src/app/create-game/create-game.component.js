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
import { ActivatedRoute, Router } from '@angular/router';
import { StartService } from 'app/shared/services/start.service';
var CreateGameComponent = /** @class */ (function () {
    function CreateGameComponent(startService, route, router) {
        this.startService = startService;
        this.route = route;
        this.router = router;
        this.amountOfBots = 0;
    }
    CreateGameComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.route.params.subscribe(function (params) {
            _this.userName = params['userName'];
        });
    };
    CreateGameComponent.prototype.createGame = function () {
        var _this = this;
        this.startService.createGame(this.userName, this.amountOfBots)
            .subscribe(function (data) {
            _this.router.navigate(['/game/' + data + '/' + true]);
        });
    };
    CreateGameComponent = __decorate([
        Component({
            selector: 'app-create-game',
            templateUrl: './create-game.component.html'
        }),
        __metadata("design:paramtypes", [StartService,
            ActivatedRoute,
            Router])
    ], CreateGameComponent);
    return CreateGameComponent;
}());
export { CreateGameComponent };
//# sourceMappingURL=create-game.component.js.map