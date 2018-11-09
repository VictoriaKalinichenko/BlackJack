(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["app-game-module-game-module"],{

/***/ "./src/app/game-module/game-routing.module.ts":
/*!****************************************************!*\
  !*** ./src/app/game-module/game-routing.module.ts ***!
  \****************************************************/
/*! exports provided: routes, GameRoutingModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "routes", function() { return routes; });
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GameRoutingModule", function() { return GameRoutingModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var app_game_module_game_game_component__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! app/game-module/game/game.component */ "./src/app/game-module/game/game.component.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};



var routes = [
    {
        path: '',
        component: app_game_module_game_game_component__WEBPACK_IMPORTED_MODULE_2__["GameComponent"]
    }
];
var GameRoutingModule = /** @class */ (function () {
    function GameRoutingModule() {
    }
    GameRoutingModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"])({
            imports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"].forChild(routes)],
            exports: [_angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"]]
        })
    ], GameRoutingModule);
    return GameRoutingModule;
}());



/***/ }),

/***/ "./src/app/game-module/game.module.ts":
/*!********************************************!*\
  !*** ./src/app/game-module/game.module.ts ***!
  \********************************************/
/*! exports provided: GameModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GameModule", function() { return GameModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var app_shared_modules_shared_module__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! app/shared/modules/shared.module */ "./src/app/shared/modules/shared.module.ts");
/* harmony import */ var app_game_module_game_routing_module__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! app/game-module/game-routing.module */ "./src/app/game-module/game-routing.module.ts");
/* harmony import */ var app_game_module_game_game_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! app/game-module/game/game.component */ "./src/app/game-module/game/game.component.ts");
/* harmony import */ var app_game_module_player_output_player_output_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! app/game-module/player-output/player-output.component */ "./src/app/game-module/player-output/player-output.component.ts");
/* harmony import */ var app_shared_services_round_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! app/shared/services/round.service */ "./src/app/shared/services/round.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};






var GameModule = /** @class */ (function () {
    function GameModule() {
    }
    GameModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"])({
            imports: [
                app_game_module_game_routing_module__WEBPACK_IMPORTED_MODULE_2__["GameRoutingModule"],
                app_shared_modules_shared_module__WEBPACK_IMPORTED_MODULE_1__["SharedModule"]
            ],
            declarations: [
                app_game_module_game_game_component__WEBPACK_IMPORTED_MODULE_3__["GameComponent"],
                app_game_module_player_output_player_output_component__WEBPACK_IMPORTED_MODULE_4__["PlayerOutputComponent"]
            ],
            providers: [
                app_shared_services_round_service__WEBPACK_IMPORTED_MODULE_5__["RoundService"]
            ]
        })
    ], GameModule);
    return GameModule;
}());



/***/ }),

/***/ "./src/app/game-module/game/game.component.html":
/*!******************************************************!*\
  !*** ./src/app/game-module/game/game.component.html ***!
  \******************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"row row-flex\">\r\n    <div class=\"col-lg-4 col-md-4 col-sm-4 col-xs-12 well\">\r\n        <h4><span class=\"label label-danger\">Dealer</span></h4>\r\n        <app-player-output [cards]=\"game.dealer.cards\" [name]=\"game.dealer.name\" [cardScore]=\"game.dealer.cardScore\"></app-player-output>\r\n    </div>\r\n\r\n    <div class=\"col-lg-8 col-md-8 col-sm-8 col-xs-12 well\">\r\n        <div class=\"row\">\r\n            <div class=\"col-lg-6 col-md-6 col-sm-6 col-xs-12\">\r\n                <h4><span class=\"label label-primary\">Human</span></h4>\r\n                <app-player-output [cards]=\"game.human.cards\" [name]=\"game.human.name\" [cardScore]=\"game.human.cardScore\"></app-player-output>\r\n            </div>\r\n\r\n            <div class=\"col-lg-6 col-md-6 col-sm-6 col-xs-12\">\r\n                <div *ngIf=\"takeCard\">\r\n                    <button class=\"btn btn-primary\" (click)=\"onTakeCard()\">Take card</button>\r\n                    <button class=\"btn btn-primary\" (click)=\"onContinueRound()\">Don't take</button>\r\n                </div>\r\n\r\n                <div *ngIf=\"endRound\">\r\n                    <p>{{game.roundResult}}</p>\r\n                    <button class=\"btn btn-primary\" (click)=\"onStartRound()\">End round</button>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n<div class=\"row row-flex\">\r\n    <div *ngFor=\"let bot of game.bots\" class=\"col-lg-2 col-md-4 col-sm-4 col-xs-6 well\">\r\n        <h4><span class=\"label label-default\">Bot</span></h4>\r\n        <app-player-output [cards]=\"bot.cards\" [name]=\"bot.name\" [cardScore]=\"bot.cardScore\"></app-player-output>\r\n    </div>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/game-module/game/game.component.ts":
/*!****************************************************!*\
  !*** ./src/app/game-module/game/game.component.ts ***!
  \****************************************************/
/*! exports provided: GameComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GameComponent", function() { return GameComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var app_shared_services_round_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! app/shared/services/round.service */ "./src/app/shared/services/round.service.ts");
/* harmony import */ var app_shared_services_start_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! app/shared/services/start.service */ "./src/app/shared/services/start.service.ts");
/* harmony import */ var app_shared_services_new_game_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! app/shared/services/new-game.service */ "./src/app/shared/services/new-game.service.ts");
/* harmony import */ var app_shared_mapping_models_game_mapping_model__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! app/shared/mapping-models/game-mapping-model */ "./src/app/shared/mapping-models/game-mapping-model.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};






var GameComponent = /** @class */ (function () {
    function GameComponent(route, roundService, startService, newGameService) {
        this.route = route;
        this.roundService = roundService;
        this.startService = startService;
        this.newGameService = newGameService;
        this.game = new app_shared_mapping_models_game_mapping_model__WEBPACK_IMPORTED_MODULE_5__["GameMappingModel"]();
        this.takeCard = false;
        this.endRound = false;
    }
    GameComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.route.params.subscribe(function (params) {
            _this.gameId = params['gameId'];
            var isNewGame = _this.newGameService.getIsNewGame();
            if (isNewGame) {
                _this.onStartRound();
            }
            if (!isNewGame) {
                _this.onRestoreRound();
            }
        });
    };
    GameComponent.prototype.onStartRound = function () {
        var _this = this;
        this.roundService.startRound(this.gameId)
            .subscribe(function (data) {
            _this.game = _this.game.deserialize(data);
            _this.setGamePlay(_this.game.roundResult);
        });
    };
    GameComponent.prototype.onTakeCard = function () {
        var _this = this;
        this.roundService.takeCard(this.gameId)
            .subscribe(function (data) {
            _this.game = _this.game.deserialize(data);
            _this.setGamePlay(_this.game.roundResult);
        });
    };
    GameComponent.prototype.onContinueRound = function () {
        var _this = this;
        this.roundService.endRound(this.gameId)
            .subscribe(function (data) {
            _this.game.roundResult = data;
            _this.setGamePlay(_this.game.roundResult);
        });
    };
    GameComponent.prototype.onRestoreRound = function () {
        var _this = this;
        this.roundService.restoreRound(this.gameId)
            .subscribe(function (data) {
            _this.game = _this.game.deserialize(data);
            _this.setGamePlay(_this.game.roundResult);
        });
    };
    GameComponent.prototype.setGamePlay = function (roundResult) {
        if (this.game.roundResult == "") {
            this.endRound = false;
            this.takeCard = true;
        }
        if (this.game.roundResult != "") {
            this.endRound = true;
            this.takeCard = false;
        }
    };
    GameComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-game',
            template: __webpack_require__(/*! ./game.component.html */ "./src/app/game-module/game/game.component.html")
        }),
        __metadata("design:paramtypes", [_angular_router__WEBPACK_IMPORTED_MODULE_1__["ActivatedRoute"],
            app_shared_services_round_service__WEBPACK_IMPORTED_MODULE_2__["RoundService"],
            app_shared_services_start_service__WEBPACK_IMPORTED_MODULE_3__["StartService"],
            app_shared_services_new_game_service__WEBPACK_IMPORTED_MODULE_4__["NewGameService"]])
    ], GameComponent);
    return GameComponent;
}());



/***/ }),

/***/ "./src/app/game-module/player-output/player-output.component.html":
/*!************************************************************************!*\
  !*** ./src/app/game-module/player-output/player-output.component.html ***!
  \************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<p>Name: {{name}}</p>\r\n<p>CardScore: {{cardScore}}</p>\r\n\r\n<p>Cards:</p>\r\n<ul>\r\n    <li *ngFor=\"let card of cards\">{{card}}</li>\r\n</ul>"

/***/ }),

/***/ "./src/app/game-module/player-output/player-output.component.ts":
/*!**********************************************************************!*\
  !*** ./src/app/game-module/player-output/player-output.component.ts ***!
  \**********************************************************************/
/*! exports provided: PlayerOutputComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "PlayerOutputComponent", function() { return PlayerOutputComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var PlayerOutputComponent = /** @class */ (function () {
    function PlayerOutputComponent() {
    }
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", String)
    ], PlayerOutputComponent.prototype, "name", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Number)
    ], PlayerOutputComponent.prototype, "cardScore", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Array)
    ], PlayerOutputComponent.prototype, "cards", void 0);
    PlayerOutputComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-player-output',
            template: __webpack_require__(/*! ./player-output.component.html */ "./src/app/game-module/player-output/player-output.component.html")
        })
    ], PlayerOutputComponent);
    return PlayerOutputComponent;
}());



/***/ }),

/***/ "./src/app/shared/mapping-models/game-mapping-model.ts":
/*!*************************************************************!*\
  !*** ./src/app/shared/mapping-models/game-mapping-model.ts ***!
  \*************************************************************/
/*! exports provided: GameMappingModel */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GameMappingModel", function() { return GameMappingModel; });
/* harmony import */ var app_shared_mapping_models_player_mapping_model__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! app/shared/mapping-models/player-mapping-model */ "./src/app/shared/mapping-models/player-mapping-model.ts");

var GameMappingModel = /** @class */ (function () {
    function GameMappingModel() {
        this.human = new app_shared_mapping_models_player_mapping_model__WEBPACK_IMPORTED_MODULE_0__["PlayerMappingModel"]();
        this.dealer = new app_shared_mapping_models_player_mapping_model__WEBPACK_IMPORTED_MODULE_0__["PlayerMappingModel"]();
        this.bots = [];
    }
    GameMappingModel.prototype.deserialize = function (input) {
        this.dealer = this.dealer.deserialize(input.Dealer);
        this.human = this.human.deserialize(input.Human);
        for (var iterator = 0; iterator < input.Bots.length; iterator++) {
            if (this.bots[iterator] == null) {
                this.bots[iterator] = new app_shared_mapping_models_player_mapping_model__WEBPACK_IMPORTED_MODULE_0__["PlayerMappingModel"]();
            }
            this.bots[iterator] = this.bots[iterator].deserialize(input.Bots[iterator]);
        }
        this.roundResult = input.RoundResult;
        return this;
    };
    return GameMappingModel;
}());



/***/ }),

/***/ "./src/app/shared/mapping-models/player-mapping-model.ts":
/*!***************************************************************!*\
  !*** ./src/app/shared/mapping-models/player-mapping-model.ts ***!
  \***************************************************************/
/*! exports provided: PlayerMappingModel */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "PlayerMappingModel", function() { return PlayerMappingModel; });
var PlayerMappingModel = /** @class */ (function () {
    function PlayerMappingModel() {
    }
    PlayerMappingModel.prototype.deserialize = function (input) {
        if (input.Name != null) {
            this.name = input.Name;
        }
        this.cardScore = input.CardScore;
        this.cards = input.Cards;
        return this;
    };
    return PlayerMappingModel;
}());



/***/ }),

/***/ "./src/app/shared/services/round.service.ts":
/*!**************************************************!*\
  !*** ./src/app/shared/services/round.service.ts ***!
  \**************************************************/
/*! exports provided: RoundService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "RoundService", function() { return RoundService; });
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var RoundService = /** @class */ (function () {
    function RoundService(httpClient) {
        this.httpClient = httpClient;
    }
    RoundService.prototype.startRound = function (gameId) {
        var options = gameId ?
            { params: new _angular_common_http__WEBPACK_IMPORTED_MODULE_0__["HttpParams"]().set('gameId', gameId.toString()) } : {};
        return this.httpClient.get('Round/Start', options);
    };
    RoundService.prototype.endRound = function (gameId) {
        var options = gameId ?
            { params: new _angular_common_http__WEBPACK_IMPORTED_MODULE_0__["HttpParams"]().set('gameId', gameId.toString()) } : {};
        return this.httpClient.get('Round/End', options);
    };
    RoundService.prototype.takeCard = function (gameId) {
        var options = gameId ?
            { params: new _angular_common_http__WEBPACK_IMPORTED_MODULE_0__["HttpParams"]().set('gameId', gameId.toString()) } : {};
        return this.httpClient.get('Round/TakeCard', options);
    };
    RoundService.prototype.restoreRound = function (gameId) {
        var options = gameId ?
            { params: new _angular_common_http__WEBPACK_IMPORTED_MODULE_0__["HttpParams"]().set('gameId', gameId.toString()) } : {};
        return this.httpClient.get('Round/Restore', options);
    };
    RoundService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Injectable"])(),
        __metadata("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_0__["HttpClient"]])
    ], RoundService);
    return RoundService;
}());



/***/ })

}]);
//# sourceMappingURL=app-game-module-game-module.js.map