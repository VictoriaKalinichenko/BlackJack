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
import { ActivatedRoute } from '@angular/router';
import { GameViewModel } from '../viewmodels/GameViewModel';
import { deserialize } from 'json-typescript-mapper';
import { DataService } from '../services/data.service';
var GameComponent = /** @class */ (function () {
    function GameComponent(route, dataService) {
        this.route = route;
        this.dataService = dataService;
    }
    GameComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.route.params.subscribe(function (params) {
            _this.GameId = params['Id'];
            _this.dataService.GetGame(_this.GameId)
                .subscribe(function (data) {
                _this.GameViewModel = deserialize(GameViewModel, data);
            }, function (error) {
                console.log(error);
            });
        });
    };
    GameComponent = __decorate([
        Component({
            selector: 'app-game',
            templateUrl: './game.component.html',
            styleUrls: ['./game.component.css']
        }),
        __metadata("design:paramtypes", [ActivatedRoute,
            DataService])
    ], GameComponent);
    return GameComponent;
}());
export { GameComponent };
//# sourceMappingURL=game.component.js.map