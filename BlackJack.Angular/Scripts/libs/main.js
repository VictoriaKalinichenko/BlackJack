(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["main"],{

/***/ "./src/$$_lazy_route_resource lazy recursive":
/*!**********************************************************!*\
  !*** ./src/$$_lazy_route_resource lazy namespace object ***!
  \**********************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

function webpackEmptyAsyncContext(req) {
	// Here Promise.resolve().then() is used instead of new Promise() to prevent
	// uncaught exception popping up in devtools
	return Promise.resolve().then(function() {
		var e = new Error("Cannot find module '" + req + "'");
		e.code = 'MODULE_NOT_FOUND';
		throw e;
	});
}
webpackEmptyAsyncContext.keys = function() { return []; };
webpackEmptyAsyncContext.resolve = webpackEmptyAsyncContext;
module.exports = webpackEmptyAsyncContext;
webpackEmptyAsyncContext.id = "./src/$$_lazy_route_resource lazy recursive";

/***/ }),

/***/ "./src/app/app.component.css":
/*!***********************************!*\
  !*** ./src/app/app.component.css ***!
  \***********************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ""

/***/ }),

/***/ "./src/app/app.component.html":
/*!************************************!*\
  !*** ./src/app/app.component.html ***!
  \************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<router-outlet></router-outlet>"

/***/ }),

/***/ "./src/app/app.component.ts":
/*!**********************************!*\
  !*** ./src/app/app.component.ts ***!
  \**********************************/
/*! exports provided: AppComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AppComponent", function() { return AppComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};

var AppComponent = /** @class */ (function () {
    function AppComponent() {
    }
    AppComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-root',
            template: __webpack_require__(/*! ./app.component.html */ "./src/app/app.component.html"),
            styles: [__webpack_require__(/*! ./app.component.css */ "./src/app/app.component.css")]
        })
    ], AppComponent);
    return AppComponent;
}());



/***/ }),

/***/ "./src/app/app.module.ts":
/*!*******************************!*\
  !*** ./src/app/app.module.ts ***!
  \*******************************/
/*! exports provided: AppModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AppModule", function() { return AppModule; });
/* harmony import */ var _angular_platform_browser__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/platform-browser */ "./node_modules/@angular/platform-browser/fesm5/platform-browser.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm5/forms.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @angular/common */ "./node_modules/@angular/common/fesm5/common.js");
/* harmony import */ var _app_component__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ./app.component */ "./src/app/app.component.ts");
/* harmony import */ var _homepage_homepage_component__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ./homepage/homepage.component */ "./src/app/homepage/homepage.component.ts");
/* harmony import */ var _authorized_user_startpage_startpage_component__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! ./authorized-user/startpage/startpage.component */ "./src/app/authorized-user/startpage/startpage.component.ts");
/* harmony import */ var _authorized_user_authorized_user_component__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! ./authorized-user/authorized-user.component */ "./src/app/authorized-user/authorized-user.component.ts");
/* harmony import */ var _authorized_user_game_game_component__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! ./authorized-user/game/game.component */ "./src/app/authorized-user/game/game.component.ts");
/* harmony import */ var _authorized_user_game_player_output_player_output_component__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! ./authorized-user/game/player-output/player-output.component */ "./src/app/authorized-user/game/player-output/player-output.component.ts");
/* harmony import */ var _authorized_user_game_dealer_output_dealer_output_component__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! ./authorized-user/game/dealer-output/dealer-output.component */ "./src/app/authorized-user/game/dealer-output/dealer-output.component.ts");
/* harmony import */ var _error_page_error_page_component__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! ./error-page/error-page.component */ "./src/app/error-page/error-page.component.ts");
/* harmony import */ var _shared_services_data_service__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! ./shared/services/data.service */ "./src/app/shared/services/data.service.ts");
/* harmony import */ var _shared_services_error_service__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! ./shared/services/error.service */ "./src/app/shared/services/error.service.ts");
/* harmony import */ var _shared_services_http_service__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! ./shared/services/http.service */ "./src/app/shared/services/http.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};

















var appRoutes = [
    { path: '', component: _homepage_homepage_component__WEBPACK_IMPORTED_MODULE_7__["HomepageComponent"] },
    {
        path: 'user/:UserName',
        component: _authorized_user_authorized_user_component__WEBPACK_IMPORTED_MODULE_9__["AuthorizedUserComponent"],
        children: [
            { path: '', component: _authorized_user_startpage_startpage_component__WEBPACK_IMPORTED_MODULE_8__["StartpageComponent"] },
            { path: 'game/:Id', component: _authorized_user_game_game_component__WEBPACK_IMPORTED_MODULE_10__["GameComponent"] }
        ]
    },
    { path: 'error', component: _error_page_error_page_component__WEBPACK_IMPORTED_MODULE_13__["ErrorPageComponent"] }
];
var AppModule = /** @class */ (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["NgModule"])({
            declarations: [
                _app_component__WEBPACK_IMPORTED_MODULE_6__["AppComponent"],
                _homepage_homepage_component__WEBPACK_IMPORTED_MODULE_7__["HomepageComponent"],
                _authorized_user_startpage_startpage_component__WEBPACK_IMPORTED_MODULE_8__["StartpageComponent"],
                _authorized_user_authorized_user_component__WEBPACK_IMPORTED_MODULE_9__["AuthorizedUserComponent"],
                _authorized_user_game_game_component__WEBPACK_IMPORTED_MODULE_10__["GameComponent"],
                _authorized_user_game_player_output_player_output_component__WEBPACK_IMPORTED_MODULE_11__["PlayerOutputComponent"],
                _authorized_user_game_dealer_output_dealer_output_component__WEBPACK_IMPORTED_MODULE_12__["DealerOutputComponent"],
                _error_page_error_page_component__WEBPACK_IMPORTED_MODULE_13__["ErrorPageComponent"]
            ],
            imports: [
                _angular_platform_browser__WEBPACK_IMPORTED_MODULE_0__["BrowserModule"],
                _angular_forms__WEBPACK_IMPORTED_MODULE_2__["FormsModule"],
                _angular_common_http__WEBPACK_IMPORTED_MODULE_3__["HttpClientModule"],
                _angular_router__WEBPACK_IMPORTED_MODULE_4__["RouterModule"].forRoot(appRoutes, { useHash: true })
            ],
            providers: [
                _shared_services_data_service__WEBPACK_IMPORTED_MODULE_14__["DataService"],
                _shared_services_error_service__WEBPACK_IMPORTED_MODULE_15__["ErrorService"],
                _shared_services_http_service__WEBPACK_IMPORTED_MODULE_16__["HttpService"],
                { provide: _angular_common__WEBPACK_IMPORTED_MODULE_5__["APP_BASE_HREF"], useValue: '/' }
            ],
            bootstrap: [
                _app_component__WEBPACK_IMPORTED_MODULE_6__["AppComponent"],
                _homepage_homepage_component__WEBPACK_IMPORTED_MODULE_7__["HomepageComponent"],
                _authorized_user_startpage_startpage_component__WEBPACK_IMPORTED_MODULE_8__["StartpageComponent"],
                _authorized_user_authorized_user_component__WEBPACK_IMPORTED_MODULE_9__["AuthorizedUserComponent"],
                _authorized_user_game_game_component__WEBPACK_IMPORTED_MODULE_10__["GameComponent"],
                _authorized_user_game_player_output_player_output_component__WEBPACK_IMPORTED_MODULE_11__["PlayerOutputComponent"],
                _authorized_user_game_dealer_output_dealer_output_component__WEBPACK_IMPORTED_MODULE_12__["DealerOutputComponent"],
                _error_page_error_page_component__WEBPACK_IMPORTED_MODULE_13__["ErrorPageComponent"]
            ]
        })
    ], AppModule);
    return AppModule;
}());



/***/ }),

/***/ "./src/app/authorized-user/authorized-user.component.css":
/*!***************************************************************!*\
  !*** ./src/app/authorized-user/authorized-user.component.css ***!
  \***************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ""

/***/ }),

/***/ "./src/app/authorized-user/authorized-user.component.html":
/*!****************************************************************!*\
  !*** ./src/app/authorized-user/authorized-user.component.html ***!
  \****************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<nav class=\"navbar navbar-default\">\r\n    <div class=\"container\">\r\n        <div class=\"navbar-header\">\r\n            <button type=\"button\" class=\"navbar-toggle\" data-toggle=\"collapse\" data-target=\"#myNavbar\">\r\n                <span class=\"icon-bar\"></span>\r\n                <span class=\"icon-bar\"></span>\r\n                <span class=\"icon-bar\"></span>\r\n            </button>\r\n            <a class=\"navbar-brand\" [routerLink]=\"['/user', UserName]\">\r\n                Home\r\n            </a>\r\n        </div>\r\n\r\n        <div class=\"collapse navbar-collapse\" id=\"myNavbar\">\r\n            <ul class=\"nav navbar-nav\">\r\n                <li class=\"navbar-text\">\r\n                    <b>Log in as: {{UserName}}</b>\r\n                </li>\r\n                <li class=\"nav-item\">\r\n                    <a routerLink=\"/\">\r\n                        Log out\r\n                    </a>\r\n                </li>\r\n            </ul>\r\n        </div>\r\n    </div>\r\n</nav>\r\n<router-outlet></router-outlet>  "

/***/ }),

/***/ "./src/app/authorized-user/authorized-user.component.ts":
/*!**************************************************************!*\
  !*** ./src/app/authorized-user/authorized-user.component.ts ***!
  \**************************************************************/
/*! exports provided: AuthorizedUserComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AuthorizedUserComponent", function() { return AuthorizedUserComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _shared_services_data_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../shared/services/data.service */ "./src/app/shared/services/data.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



var AuthorizedUserComponent = /** @class */ (function () {
    function AuthorizedUserComponent(_dataService, _route) {
        this._dataService = _dataService;
        this._route = _route;
    }
    AuthorizedUserComponent.prototype.ngOnInit = function () {
        var _this = this;
        this._route.params.subscribe(function (params) {
            _this.UserName = params['UserName'];
        });
        this._dataService.SetUserName(this.UserName);
    };
    AuthorizedUserComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-authorized-user',
            template: __webpack_require__(/*! ./authorized-user.component.html */ "./src/app/authorized-user/authorized-user.component.html"),
            styles: [__webpack_require__(/*! ./authorized-user.component.css */ "./src/app/authorized-user/authorized-user.component.css")]
        }),
        __metadata("design:paramtypes", [_shared_services_data_service__WEBPACK_IMPORTED_MODULE_2__["DataService"],
            _angular_router__WEBPACK_IMPORTED_MODULE_1__["ActivatedRoute"]])
    ], AuthorizedUserComponent);
    return AuthorizedUserComponent;
}());



/***/ }),

/***/ "./src/app/authorized-user/game/dealer-output/dealer-output.component.css":
/*!********************************************************************************!*\
  !*** ./src/app/authorized-user/game/dealer-output/dealer-output.component.css ***!
  \********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ""

/***/ }),

/***/ "./src/app/authorized-user/game/dealer-output/dealer-output.component.html":
/*!*********************************************************************************!*\
  !*** ./src/app/authorized-user/game/dealer-output/dealer-output.component.html ***!
  \*********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<p>Score: {{Score}}</p>\r\n\r\n<div *ngIf=\"RoundFirstPhase\">\r\n    <p>Card:</p>\r\n    <ul>\r\n        <li *ngFor=\"let card of Cards\">{{card}}</li>\r\n    </ul>\r\n</div>\r\n\r\n<div *ngIf=\"RoundSecondPhase\">\r\n    <p>CardScore: {{RoundScore}}</p>\r\n    <p>Cards:</p>\r\n    <ul>\r\n        <li *ngFor=\"let card of Cards\">{{card}}</li>\r\n    </ul>\r\n</div>"

/***/ }),

/***/ "./src/app/authorized-user/game/dealer-output/dealer-output.component.ts":
/*!*******************************************************************************!*\
  !*** ./src/app/authorized-user/game/dealer-output/dealer-output.component.ts ***!
  \*******************************************************************************/
/*! exports provided: DealerOutputComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "DealerOutputComponent", function() { return DealerOutputComponent; });
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


var DealerOutputComponent = /** @class */ (function () {
    function DealerOutputComponent() {
        this.RoundFirstPhase = false;
        this.RoundSecondPhase = false;
    }
    Object.defineProperty(DealerOutputComponent.prototype, "GameStage", {
        set: function (stage) {
            if (stage == 1) {
                this.RoundFirstPhase = true;
                this.RoundSecondPhase = false;
            }
            if (stage == 2) {
                this.RoundFirstPhase = false;
                this.RoundSecondPhase = true;
            }
            if (stage == 0) {
                this.RoundFirstPhase = false;
                this.RoundSecondPhase = false;
            }
        },
        enumerable: true,
        configurable: true
    });
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Number)
    ], DealerOutputComponent.prototype, "Score", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Number)
    ], DealerOutputComponent.prototype, "RoundScore", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Array)
    ], DealerOutputComponent.prototype, "Cards", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Number),
        __metadata("design:paramtypes", [Number])
    ], DealerOutputComponent.prototype, "GameStage", null);
    DealerOutputComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-dealer-output',
            template: __webpack_require__(/*! ./dealer-output.component.html */ "./src/app/authorized-user/game/dealer-output/dealer-output.component.html"),
            styles: [__webpack_require__(/*! ./dealer-output.component.css */ "./src/app/authorized-user/game/dealer-output/dealer-output.component.css")]
        }),
        __metadata("design:paramtypes", [])
    ], DealerOutputComponent);
    return DealerOutputComponent;
}());



/***/ }),

/***/ "./src/app/authorized-user/game/game.component.css":
/*!*********************************************************!*\
  !*** ./src/app/authorized-user/game/game.component.css ***!
  \*********************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ""

/***/ }),

/***/ "./src/app/authorized-user/game/game.component.html":
/*!**********************************************************!*\
  !*** ./src/app/authorized-user/game/game.component.html ***!
  \**********************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"row row-flex\">\r\n    <div class=\"col-lg-4 col-md-4 col-sm-4 col-xs-12 well\">\r\n        <h4><span class=\"label label-danger\">Dealer</span></h4>\r\n        <p>Name: {{Game.Dealer.Name}}</p>\r\n        <app-dealer-output [Score]=\"Game.Dealer.Score\" [RoundScore]=\"Game.Dealer.RoundScore\" [Cards]=\"Game.Dealer.Cards\" [GameStage]=\"Game.Stage\"></app-dealer-output>\r\n    </div>\r\n\r\n    <div class=\"col-lg-8 col-md-8 col-sm-8 col-xs-12 well\">\r\n        <div class=\"row\">\r\n            <div class=\"col-lg-6 col-md-6 col-sm-6 col-xs-12\">\r\n                <h4><span class=\"label label-primary\">Human</span></h4>\r\n                <p>Name: {{Game.Human.Name}}</p>\r\n                <app-player-output [Score]=\"Game.Human.Score\" [Bet]=\"Game.Human.Bet\" [RoundScore]=\"Game.Human.RoundScore\" [Cards]=\"Game.Human.Cards\" [GameStage]=\"Game.Stage\"></app-player-output>\r\n            </div>\r\n\r\n            <div class=\"col-lg-6 col-md-6 col-sm-6 col-xs-12\">\r\n                <div *ngIf=\"BetInput\">\r\n                    <label class=\"control-label\">Enter your bet: </label>\r\n                    <input [(ngModel)]=\"Bet\" type=\"number\"\r\n                           class=\"form-control\" step=\"50\" min=\"50\" value=\"50\" max={{Game.Human.Score}} />\r\n                    <br />\r\n                    <div *ngIf=\"BetValidationError\">\r\n                        <div class=\"alert alert-danger\">{{BetValidationMessage}}</div>\r\n                    </div>\r\n                    <button class=\"btn btn-primary\" (click)=\"DoRoundFirstPhase()\">Enter</button>\r\n                </div>\r\n\r\n                <div *ngIf=\"TakeCard\">\r\n                    <button class=\"btn btn-primary\" (click)=\"AddCardToHuman(true)\">Take card</button>\r\n                    <button class=\"btn btn-primary\" (click)=\"AddCardToHuman(false)\">Don't take</button>\r\n                </div>\r\n\r\n                <div *ngIf=\"BlackJackDangerChoice\">\r\n                    <p>You have BlackJack and dealer has BlackJack-danger</p>\r\n                    <button class=\"btn btn-primary\" (click)=\"DoRoundSecondPhase(true)\">Continue round</button>\r\n                    <button class=\"btn btn-primary\" (click)=\"DoRoundSecondPhase(false)\">Take award (1:1)</button>\r\n                </div>\r\n\r\n                <div *ngIf=\"EndRound\">\r\n                    <p>{{RoundResult}}</p>\r\n                    <button class=\"btn btn-primary\" (click)=\"StartNewRound()\">End round</button>\r\n                </div>\r\n\r\n                <div *ngIf=\"EndGame\">\r\n                    <p>{{GameResult}}</p>\r\n                    <button class=\"btn btn-primary\" (click)=\"StartNewGame()\">End game</button>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n<div class=\"row row-flex\">\r\n    <div *ngFor=\"let bot of Game.Bots\" class=\"col-lg-2 col-md-4 col-sm-4 col-xs-6 well\">\r\n        <h4><span class=\"label label-default\">Bot</span></h4>\r\n        <p>Name: {{bot.Name}}</p>\r\n        <app-player-output [Score]=\"bot.Score\" [RoundScore]=\"bot.RoundScore\" [Bet]=\"bot.Bet\" [Cards]=\"bot.Cards\" [GameStage]=\"Game.Stage\"></app-player-output>\r\n    </div>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/authorized-user/game/game.component.ts":
/*!********************************************************!*\
  !*** ./src/app/authorized-user/game/game.component.ts ***!
  \********************************************************/
/*! exports provided: GameComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GameComponent", function() { return GameComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _shared_services_http_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../shared/services/http.service */ "./src/app/shared/services/http.service.ts");
/* harmony import */ var _shared_services_error_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../../shared/services/error.service */ "./src/app/shared/services/error.service.ts");
/* harmony import */ var json_typescript_mapper__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! json-typescript-mapper */ "./node_modules/json-typescript-mapper/index.js");
/* harmony import */ var json_typescript_mapper__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(json_typescript_mapper__WEBPACK_IMPORTED_MODULE_4__);
/* harmony import */ var _shared_models_game_view__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../../shared/models/game-view */ "./src/app/shared/models/game-view.ts");
/* harmony import */ var _shared_models_player_view__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ../../shared/models/player-view */ "./src/app/shared/models/player-view.ts");
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
    function GameComponent(_route, _router, _httpService, _errorService) {
        this._route = _route;
        this._router = _router;
        this._httpService = _httpService;
        this._errorService = _errorService;
        this.Game = new _shared_models_game_view__WEBPACK_IMPORTED_MODULE_5__["GameView"]();
        this.BetValidationError = false;
        this.Bet = 50;
        this.BetInput = false;
        this.TakeCard = false;
        this.BlackJackDangerChoice = false;
        this.EndRound = false;
        this.EndGame = false;
    }
    GameComponent.prototype.ngOnInit = function () {
        var _this = this;
        this._route.params.subscribe(function (params) {
            _this.GameId = params['Id'];
            _this.GetGame();
        });
    };
    GameComponent.prototype.GamePlayInitializer = function () {
        if (this.Game.Stage == 0) {
            this.GamePlayBetInput();
        }
        if (this.Game.Stage == 1) {
            this.ResumeGameAfterRoundFirstPhase();
        }
        if (this.Game.Stage == 2) {
            this.ResumeGameAfterRoundSecondPhase();
        }
    };
    GameComponent.prototype.GetGame = function () {
        var _this = this;
        this._httpService.GetGame(this.GameId)
            .subscribe(function (data) {
            _this.Game = Object(json_typescript_mapper__WEBPACK_IMPORTED_MODULE_4__["deserialize"])(_shared_models_game_view__WEBPACK_IMPORTED_MODULE_5__["GameView"], data);
            if (data["IsGameOver"] != "") {
                _this.GameResult = data["IsGameOver"];
                _this.GamePlayEndGame();
            }
            _this.GamePlayInitializer();
        }, function (error) {
            console.log(error);
            _this._errorService.SetError(error["error"]["Message"]);
            _this._router.navigate(['/error']);
        });
    };
    GameComponent.prototype.ResumeGameAfterRoundFirstPhase = function () {
        var _this = this;
        this._httpService.ResumeGameAfterRoundFirstPhase(this.Game.Id)
            .subscribe(function (data) {
            _this.Game = Object(json_typescript_mapper__WEBPACK_IMPORTED_MODULE_4__["deserialize"])(_shared_models_game_view__WEBPACK_IMPORTED_MODULE_5__["GameView"], data);
            _this.FirstPhaseGamePlay(data["HumanBlackJackAndDealerBlackJackDanger"], data["CanHumanTakeOneMoreCard"]);
        }, function (error) {
            console.log(error);
            _this._errorService.SetError(error["error"]["Message"]);
            _this._router.navigate(['/error']);
        });
    };
    GameComponent.prototype.ResumeGameAfterRoundSecondPhase = function () {
        var _this = this;
        this._httpService.ResumeGameAfterRoundSecondPhase(this.Game.Id)
            .subscribe(function (data) {
            _this.Game = Object(json_typescript_mapper__WEBPACK_IMPORTED_MODULE_4__["deserialize"])(_shared_models_game_view__WEBPACK_IMPORTED_MODULE_5__["GameView"], data);
            _this.RoundResult = data["RoundResult"];
            _this.GamePlayEndRound();
        }, function (error) {
            console.log(error);
            _this._errorService.SetError(error["error"]["Message"]);
            _this._router.navigate(['/error']);
        });
    };
    GameComponent.prototype.AddCardToHuman = function (takeCard) {
        var _this = this;
        if (takeCard) {
            this._httpService.AddOneMoreCardToHuman(this.Game.Id)
                .subscribe(function (data) {
                if (data["CanHumanTakeOneMoreCard"]) {
                    _this.Game.Human = Object(json_typescript_mapper__WEBPACK_IMPORTED_MODULE_4__["deserialize"])(_shared_models_player_view__WEBPACK_IMPORTED_MODULE_6__["PlayerView"], data);
                }
                if (!data["CanHumanTakeOneMoreCard"]) {
                    _this.DoRoundSecondPhase(false);
                }
            }, function (error) {
                console.log(error);
                _this._errorService.SetError(error["error"]["Message"]);
                _this._router.navigate(['/error']);
            });
        }
        if (!takeCard) {
            this.DoRoundSecondPhase(false);
        }
    };
    GameComponent.prototype.FirstPhaseGamePlay = function (humanBlackJackAndDealerBlackJackDanger, canHumanTakeOneMoreCard) {
        this.Game.Stage = 1;
        this.BetValidationError = false;
        if (humanBlackJackAndDealerBlackJackDanger) {
            this.GamePlayBlackJackDangerChoice();
        }
        if (canHumanTakeOneMoreCard) {
            this.GamePlayTakeCard();
        }
        if (!humanBlackJackAndDealerBlackJackDanger && !canHumanTakeOneMoreCard) {
            this.DoRoundSecondPhase(false);
        }
    };
    GameComponent.prototype.DoRoundFirstPhase = function () {
        var _this = this;
        this._httpService.DoRoundFirstPhase(this.Game.Id, this.Game.Human.GamePlayerId, this.Bet)
            .subscribe(function (data) {
            _this.Game = Object(json_typescript_mapper__WEBPACK_IMPORTED_MODULE_4__["deserialize"])(_shared_models_game_view__WEBPACK_IMPORTED_MODULE_5__["GameView"], data["Data"]);
            if (data["Message"] != "") {
                _this.ShowValidationMessage(data["Message"]);
            }
            if (data["Message"] == "") {
                _this.FirstPhaseGamePlay(data["Data"]["HumanBlackJackAndDealerBlackJackDanger"], data["Data"]["CanHumanTakeOneMoreCard"]);
            }
        }, function (error) {
            console.log(error);
            _this._errorService.SetError(error["error"]["Message"]);
            _this._router.navigate(['/error']);
        });
    };
    GameComponent.prototype.ShowValidationMessage = function (validationMessage) {
        this.BetValidationError = true;
        this.BetValidationMessage = validationMessage;
    };
    GameComponent.prototype.DoRoundSecondPhase = function (humanBlackJackContinueRound) {
        var _this = this;
        this._httpService.DoRoundSecondPhase(this.Game.Id, humanBlackJackContinueRound)
            .subscribe(function (data) {
            _this.Game = Object(json_typescript_mapper__WEBPACK_IMPORTED_MODULE_4__["deserialize"])(_shared_models_game_view__WEBPACK_IMPORTED_MODULE_5__["GameView"], data);
            _this.RoundResult = data["RoundResult"];
            _this.GamePlayEndRound();
        }, function (error) {
            console.log(error);
            _this._errorService.SetError(error["error"]["Message"]);
            _this._router.navigate(['/error']);
        });
    };
    GameComponent.prototype.StartNewGame = function () {
        var _this = this;
        this._httpService.EndGame(this.Game.Id, this.GameResult)
            .subscribe(function (data) {
            _this._router.navigate(['/user/' + _this.Game.Human.Name]);
        }, function (error) {
            console.log(error);
            _this._errorService.SetError(error["error"]["Message"]);
            _this._router.navigate(['/error']);
        });
    };
    GameComponent.prototype.StartNewRound = function () {
        var _this = this;
        this._httpService.EndRound(this.Game.Id)
            .subscribe(function (data) {
            _this.GetGame();
        }, function (error) {
            console.log(error);
            _this._errorService.SetError(error["error"]["Message"]);
            _this._router.navigate(['/error']);
        });
    };
    GameComponent.prototype.GamePlayBetInput = function () {
        this.BetInput = true;
        this.TakeCard = false;
        this.BlackJackDangerChoice = false;
        this.EndRound = false;
        this.EndGame = false;
    };
    GameComponent.prototype.GamePlayTakeCard = function () {
        this.BetInput = false;
        this.TakeCard = true;
        this.BlackJackDangerChoice = false;
        this.EndRound = false;
        this.EndGame = false;
    };
    GameComponent.prototype.GamePlayBlackJackDangerChoice = function () {
        this.BetInput = false;
        this.TakeCard = false;
        this.BlackJackDangerChoice = true;
        this.EndRound = false;
        this.EndGame = false;
    };
    GameComponent.prototype.GamePlayEndRound = function () {
        this.Game.Stage = 2;
        this.BetInput = false;
        this.TakeCard = false;
        this.BlackJackDangerChoice = false;
        this.EndRound = true;
        this.EndGame = false;
    };
    GameComponent.prototype.GamePlayEndGame = function () {
        this.BetInput = false;
        this.TakeCard = false;
        this.BlackJackDangerChoice = false;
        this.EndRound = false;
        this.EndGame = true;
    };
    GameComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-game',
            template: __webpack_require__(/*! ./game.component.html */ "./src/app/authorized-user/game/game.component.html"),
            styles: [__webpack_require__(/*! ./game.component.css */ "./src/app/authorized-user/game/game.component.css")]
        }),
        __metadata("design:paramtypes", [_angular_router__WEBPACK_IMPORTED_MODULE_1__["ActivatedRoute"],
            _angular_router__WEBPACK_IMPORTED_MODULE_1__["Router"],
            _shared_services_http_service__WEBPACK_IMPORTED_MODULE_2__["HttpService"],
            _shared_services_error_service__WEBPACK_IMPORTED_MODULE_3__["ErrorService"]])
    ], GameComponent);
    return GameComponent;
}());



/***/ }),

/***/ "./src/app/authorized-user/game/player-output/player-output.component.css":
/*!********************************************************************************!*\
  !*** ./src/app/authorized-user/game/player-output/player-output.component.css ***!
  \********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ""

/***/ }),

/***/ "./src/app/authorized-user/game/player-output/player-output.component.html":
/*!*********************************************************************************!*\
  !*** ./src/app/authorized-user/game/player-output/player-output.component.html ***!
  \*********************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<p>Score: {{Score}}</p>\r\n\r\n<div *ngIf=\"!RoundStart\">\r\n    <p>Bet: {{Bet}}</p>\r\n    <p>CardScore: {{RoundScore}}</p>\r\n    <p>Cards:</p>\r\n    <ul>\r\n        <li *ngFor=\"let card of Cards\">{{card}}</li>\r\n    </ul>\r\n</div>"

/***/ }),

/***/ "./src/app/authorized-user/game/player-output/player-output.component.ts":
/*!*******************************************************************************!*\
  !*** ./src/app/authorized-user/game/player-output/player-output.component.ts ***!
  \*******************************************************************************/
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
        this.RoundStart = true;
    }
    Object.defineProperty(PlayerOutputComponent.prototype, "GameStage", {
        set: function (stage) {
            this.RoundStart = (stage == 0);
        },
        enumerable: true,
        configurable: true
    });
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Number)
    ], PlayerOutputComponent.prototype, "Score", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Number)
    ], PlayerOutputComponent.prototype, "RoundScore", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Number)
    ], PlayerOutputComponent.prototype, "Bet", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Array)
    ], PlayerOutputComponent.prototype, "Cards", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Number),
        __metadata("design:paramtypes", [Number])
    ], PlayerOutputComponent.prototype, "GameStage", null);
    PlayerOutputComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-player-output',
            template: __webpack_require__(/*! ./player-output.component.html */ "./src/app/authorized-user/game/player-output/player-output.component.html"),
            styles: [__webpack_require__(/*! ./player-output.component.css */ "./src/app/authorized-user/game/player-output/player-output.component.css")]
        }),
        __metadata("design:paramtypes", [])
    ], PlayerOutputComponent);
    return PlayerOutputComponent;
}());



/***/ }),

/***/ "./src/app/authorized-user/startpage/startpage.component.css":
/*!*******************************************************************!*\
  !*** ./src/app/authorized-user/startpage/startpage.component.css ***!
  \*******************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ""

/***/ }),

/***/ "./src/app/authorized-user/startpage/startpage.component.html":
/*!********************************************************************!*\
  !*** ./src/app/authorized-user/startpage/startpage.component.html ***!
  \********************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<h2>Main page</h2>\r\n<hr />\r\n\r\n<div class=\"row row-flex\">\r\n    <div *ngIf=\"Player.ResumeGame\" class=\"col-lg-6 col-md-6 col-sm-6 col-xs-12 well\">\r\n        <h3>Game resuming</h3>\r\n        <p>You can resume your last game</p>\r\n        <div class=\"form-group\">\r\n            <a class=\"btn btn-primary\" (click)=\"ResumeGame()\">Resume game</a>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"col-lg-6 col-md-6 col-sm-6 col-xs-12 well\">\r\n        <h3>Start new game</h3>\r\n        <label class=\"control-label col-md-4\">Amount of bots:</label>\r\n        <div class=\"col-md-8\">\r\n            <input name=\"amountOfBots\" [(ngModel)]=\"AmountOfBots\" class=\"form-control\"\r\n                   type=\"number\" value=\"0\" min=\"0\" max=\"5\"\r\n                   #amountOfBots=\"ngModel\" pattern=\"[0-5]\" />\r\n            <div [hidden]=\"amountOfBots.valid\" class=\"alert alert-danger\">\r\n                Amount of bots must be more than or equals to 0 and less than or equals to 5.\r\n            </div>\r\n            <br />\r\n            <button [disabled]=\"amountOfBots.invalid\" class=\"btn btn-primary\" (click)=\"StartNewGame()\">Start new game</button>\r\n        </div>\r\n    </div>\r\n</div>"

/***/ }),

/***/ "./src/app/authorized-user/startpage/startpage.component.ts":
/*!******************************************************************!*\
  !*** ./src/app/authorized-user/startpage/startpage.component.ts ***!
  \******************************************************************/
/*! exports provided: StartpageComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "StartpageComponent", function() { return StartpageComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _shared_services_data_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../shared/services/data.service */ "./src/app/shared/services/data.service.ts");
/* harmony import */ var _shared_services_http_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../../shared/services/http.service */ "./src/app/shared/services/http.service.ts");
/* harmony import */ var _shared_services_error_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../../shared/services/error.service */ "./src/app/shared/services/error.service.ts");
/* harmony import */ var _shared_models_authorized_user_view__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../../shared/models/authorized-user-view */ "./src/app/shared/models/authorized-user-view.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};






var StartpageComponent = /** @class */ (function () {
    function StartpageComponent(_dataService, _httpService, _errorService, _router, _route) {
        this._dataService = _dataService;
        this._httpService = _httpService;
        this._errorService = _errorService;
        this._router = _router;
        this._route = _route;
        this.Player = new _shared_models_authorized_user_view__WEBPACK_IMPORTED_MODULE_5__["AuthorizedUserView"]();
        this.AmountOfBots = 0;
    }
    StartpageComponent.prototype.ngOnInit = function () {
        this.UserName = this._dataService.GetUserName();
        this.AuthUser(this.UserName);
    };
    StartpageComponent.prototype.AuthUser = function (userName) {
        var _this = this;
        this._httpService.GetAuthorizedPlayer(this.UserName)
            .subscribe(function (data) {
            _this.Player.Name = data.Name;
            _this.Player.PlayerId = data.PlayerId;
            _this.Player.ResumeGame = data.ResumeGame;
        }, function (error) {
            console.log(error);
            _this._errorService.SetError(error["error"]["Message"]);
            _this._router.navigate(['/error']);
        });
    };
    StartpageComponent.prototype.StartNewGame = function () {
        var _this = this;
        this._httpService.CreateNewGame(this.Player.PlayerId, this.AmountOfBots)
            .subscribe(function (data) {
            _this.GameId = data["GameId"];
            _this._router.navigate(['/user/' + _this.UserName + '/game/' + _this.GameId]);
        }, function (error) {
            console.log(error);
            _this._errorService.SetError(error["error"]["Message"]);
            _this._router.navigate(['/error']);
        });
    };
    StartpageComponent.prototype.ResumeGame = function () {
        var _this = this;
        this._httpService.ResumeGame(this.Player.PlayerId)
            .subscribe(function (data) {
            _this.GameId = data["GameId"];
            _this._router.navigate(['/user/' + _this.UserName + '/game/' + _this.GameId]);
        }, function (error) {
            console.log(error);
            _this._errorService.SetError(error["error"]["Message"]);
            _this._router.navigate(['/error']);
        });
    };
    StartpageComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-startpage',
            template: __webpack_require__(/*! ./startpage.component.html */ "./src/app/authorized-user/startpage/startpage.component.html"),
            styles: [__webpack_require__(/*! ./startpage.component.css */ "./src/app/authorized-user/startpage/startpage.component.css")]
        }),
        __metadata("design:paramtypes", [_shared_services_data_service__WEBPACK_IMPORTED_MODULE_2__["DataService"],
            _shared_services_http_service__WEBPACK_IMPORTED_MODULE_3__["HttpService"],
            _shared_services_error_service__WEBPACK_IMPORTED_MODULE_4__["ErrorService"],
            _angular_router__WEBPACK_IMPORTED_MODULE_1__["Router"],
            _angular_router__WEBPACK_IMPORTED_MODULE_1__["ActivatedRoute"]])
    ], StartpageComponent);
    return StartpageComponent;
}());



/***/ }),

/***/ "./src/app/error-page/error-page.component.css":
/*!*****************************************************!*\
  !*** ./src/app/error-page/error-page.component.css ***!
  \*****************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ""

/***/ }),

/***/ "./src/app/error-page/error-page.component.html":
/*!******************************************************!*\
  !*** ./src/app/error-page/error-page.component.html ***!
  \******************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<h1>Error</h1>\r\n<p>\r\n  {{Error}}\r\n</p>\r\n"

/***/ }),

/***/ "./src/app/error-page/error-page.component.ts":
/*!****************************************************!*\
  !*** ./src/app/error-page/error-page.component.ts ***!
  \****************************************************/
/*! exports provided: ErrorPageComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ErrorPageComponent", function() { return ErrorPageComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _shared_services_error_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../shared/services/error.service */ "./src/app/shared/services/error.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var ErrorPageComponent = /** @class */ (function () {
    function ErrorPageComponent(_errorService) {
        this._errorService = _errorService;
    }
    ErrorPageComponent.prototype.ngOnInit = function () {
        this.Error = this._errorService.GetError();
    };
    ErrorPageComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-error-page',
            template: __webpack_require__(/*! ./error-page.component.html */ "./src/app/error-page/error-page.component.html"),
            styles: [__webpack_require__(/*! ./error-page.component.css */ "./src/app/error-page/error-page.component.css")]
        }),
        __metadata("design:paramtypes", [_shared_services_error_service__WEBPACK_IMPORTED_MODULE_1__["ErrorService"]])
    ], ErrorPageComponent);
    return ErrorPageComponent;
}());



/***/ }),

/***/ "./src/app/homepage/homepage.component.css":
/*!*************************************************!*\
  !*** ./src/app/homepage/homepage.component.css ***!
  \*************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ""

/***/ }),

/***/ "./src/app/homepage/homepage.component.html":
/*!**************************************************!*\
  !*** ./src/app/homepage/homepage.component.html ***!
  \**************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"jumbotron\">\r\n    <h1>BlackJack</h1>\r\n\r\n    <div class=\"form-group\">\r\n        <label>User name: </label>\r\n        <input class=\"form-control\" name=\"name\" [(ngModel)]=\"UserName\" #name=\"ngModel\" required />\r\n    </div>\r\n\r\n    <div class=\"form-group\">\r\n        <button [disabled]=\"name.invalid\" class=\"btn btn-primary\" (click)=\"AuthUser()\">Enter</button>\r\n    </div>\r\n</div>"

/***/ }),

/***/ "./src/app/homepage/homepage.component.ts":
/*!************************************************!*\
  !*** ./src/app/homepage/homepage.component.ts ***!
  \************************************************/
/*! exports provided: HomepageComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "HomepageComponent", function() { return HomepageComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var HomepageComponent = /** @class */ (function () {
    function HomepageComponent(_router) {
        this._router = _router;
    }
    HomepageComponent.prototype.AuthUser = function () {
        this._router.navigate(['/user', this.UserName]);
    };
    HomepageComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-homepage',
            template: __webpack_require__(/*! ./homepage.component.html */ "./src/app/homepage/homepage.component.html"),
            styles: [__webpack_require__(/*! ./homepage.component.css */ "./src/app/homepage/homepage.component.css")]
        }),
        __metadata("design:paramtypes", [_angular_router__WEBPACK_IMPORTED_MODULE_1__["Router"]])
    ], HomepageComponent);
    return HomepageComponent;
}());



/***/ }),

/***/ "./src/app/shared/models/authorized-user-view.ts":
/*!*******************************************************!*\
  !*** ./src/app/shared/models/authorized-user-view.ts ***!
  \*******************************************************/
/*! exports provided: AuthorizedUserView */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AuthorizedUserView", function() { return AuthorizedUserView; });
var AuthorizedUserView = /** @class */ (function () {
    function AuthorizedUserView() {
    }
    return AuthorizedUserView;
}());



/***/ }),

/***/ "./src/app/shared/models/game-view.ts":
/*!********************************************!*\
  !*** ./src/app/shared/models/game-view.ts ***!
  \********************************************/
/*! exports provided: GameView */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GameView", function() { return GameView; });
/* harmony import */ var _models_player_view__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ../models/player-view */ "./src/app/shared/models/player-view.ts");
/* harmony import */ var json_typescript_mapper__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! json-typescript-mapper */ "./node_modules/json-typescript-mapper/index.js");
/* harmony import */ var json_typescript_mapper__WEBPACK_IMPORTED_MODULE_1___default = /*#__PURE__*/__webpack_require__.n(json_typescript_mapper__WEBPACK_IMPORTED_MODULE_1__);
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var GameView = /** @class */ (function () {
    function GameView() {
        this.Id = void 0;
        this.Stage = void 0;
        this.Human = void 0;
        this.Dealer = void 0;
        this.Bots = void 0;
    }
    __decorate([
        Object(json_typescript_mapper__WEBPACK_IMPORTED_MODULE_1__["JsonProperty"])('Id'),
        __metadata("design:type", Number)
    ], GameView.prototype, "Id", void 0);
    __decorate([
        Object(json_typescript_mapper__WEBPACK_IMPORTED_MODULE_1__["JsonProperty"])('Stage'),
        __metadata("design:type", Number)
    ], GameView.prototype, "Stage", void 0);
    __decorate([
        Object(json_typescript_mapper__WEBPACK_IMPORTED_MODULE_1__["JsonProperty"])({ clazz: _models_player_view__WEBPACK_IMPORTED_MODULE_0__["PlayerView"], name: 'Human' }),
        __metadata("design:type", _models_player_view__WEBPACK_IMPORTED_MODULE_0__["PlayerView"])
    ], GameView.prototype, "Human", void 0);
    __decorate([
        Object(json_typescript_mapper__WEBPACK_IMPORTED_MODULE_1__["JsonProperty"])({ clazz: _models_player_view__WEBPACK_IMPORTED_MODULE_0__["PlayerView"], name: 'Dealer' }),
        __metadata("design:type", _models_player_view__WEBPACK_IMPORTED_MODULE_0__["PlayerView"])
    ], GameView.prototype, "Dealer", void 0);
    __decorate([
        Object(json_typescript_mapper__WEBPACK_IMPORTED_MODULE_1__["JsonProperty"])({ clazz: _models_player_view__WEBPACK_IMPORTED_MODULE_0__["PlayerView"], name: 'Bots' }),
        __metadata("design:type", Array)
    ], GameView.prototype, "Bots", void 0);
    return GameView;
}());



/***/ }),

/***/ "./src/app/shared/models/player-view.ts":
/*!**********************************************!*\
  !*** ./src/app/shared/models/player-view.ts ***!
  \**********************************************/
/*! exports provided: PlayerView */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "PlayerView", function() { return PlayerView; });
/* harmony import */ var json_typescript_mapper__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! json-typescript-mapper */ "./node_modules/json-typescript-mapper/index.js");
/* harmony import */ var json_typescript_mapper__WEBPACK_IMPORTED_MODULE_0___default = /*#__PURE__*/__webpack_require__.n(json_typescript_mapper__WEBPACK_IMPORTED_MODULE_0__);
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};

var PlayerView = /** @class */ (function () {
    function PlayerView() {
        this.GamePlayerId = void 0;
        this.Name = void 0;
        this.Score = void 0;
        this.Bet = void 0;
        this.RoundScore = void 0;
        this.Cards = void 0;
    }
    __decorate([
        Object(json_typescript_mapper__WEBPACK_IMPORTED_MODULE_0__["JsonProperty"])('Id'),
        __metadata("design:type", Number)
    ], PlayerView.prototype, "GamePlayerId", void 0);
    __decorate([
        Object(json_typescript_mapper__WEBPACK_IMPORTED_MODULE_0__["JsonProperty"])('Name'),
        __metadata("design:type", String)
    ], PlayerView.prototype, "Name", void 0);
    __decorate([
        Object(json_typescript_mapper__WEBPACK_IMPORTED_MODULE_0__["JsonProperty"])('Score'),
        __metadata("design:type", Number)
    ], PlayerView.prototype, "Score", void 0);
    __decorate([
        Object(json_typescript_mapper__WEBPACK_IMPORTED_MODULE_0__["JsonProperty"])('Bet'),
        __metadata("design:type", Number)
    ], PlayerView.prototype, "Bet", void 0);
    __decorate([
        Object(json_typescript_mapper__WEBPACK_IMPORTED_MODULE_0__["JsonProperty"])('RoundScore'),
        __metadata("design:type", Number)
    ], PlayerView.prototype, "RoundScore", void 0);
    __decorate([
        Object(json_typescript_mapper__WEBPACK_IMPORTED_MODULE_0__["JsonProperty"])('Cards'),
        __metadata("design:type", Array)
    ], PlayerView.prototype, "Cards", void 0);
    return PlayerView;
}());



/***/ }),

/***/ "./src/app/shared/services/data.service.ts":
/*!*************************************************!*\
  !*** ./src/app/shared/services/data.service.ts ***!
  \*************************************************/
/*! exports provided: DataService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "DataService", function() { return DataService; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};

var DataService = /** @class */ (function () {
    function DataService() {
    }
    DataService.prototype.SetUserName = function (userName) {
        this.UserName = userName;
    };
    DataService.prototype.GetUserName = function () {
        return this.UserName;
    };
    DataService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])({
            providedIn: 'root'
        })
    ], DataService);
    return DataService;
}());



/***/ }),

/***/ "./src/app/shared/services/error.service.ts":
/*!**************************************************!*\
  !*** ./src/app/shared/services/error.service.ts ***!
  \**************************************************/
/*! exports provided: ErrorService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "ErrorService", function() { return ErrorService; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};

var ErrorService = /** @class */ (function () {
    function ErrorService() {
    }
    ErrorService.prototype.SetError = function (error) {
        this.Error = error;
    };
    ErrorService.prototype.GetError = function () {
        return this.Error;
    };
    ErrorService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])({
            providedIn: 'root'
        })
    ], ErrorService);
    return ErrorService;
}());



/***/ }),

/***/ "./src/app/shared/services/http.service.ts":
/*!*************************************************!*\
  !*** ./src/app/shared/services/http.service.ts ***!
  \*************************************************/
/*! exports provided: HttpService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "HttpService", function() { return HttpService; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};


var HttpService = /** @class */ (function () {
    function HttpService(http) {
        this.http = http;
    }
    HttpService.prototype.GetAuthorizedPlayer = function (userName) {
        var options = userName ?
            { params: new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpParams"]().set('userName', userName.toString()) } : {};
        return this.http.get('StartGame/AuthorizePlayer', options);
    };
    HttpService.prototype.CreateNewGame = function (playerId, amountOfBots) {
        var body = { PlayerId: playerId, AmountOfBots: amountOfBots };
        return this.http.post('StartGame/CreateNewGame', body);
    };
    HttpService.prototype.ResumeGame = function (playerId) {
        var options = playerId ?
            { params: new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpParams"]().set('playerId', playerId.toString()) } : {};
        return this.http.get('StartGame/ResumeGame', options);
    };
    HttpService.prototype.GetGame = function (gameId) {
        var options = gameId ?
            { params: new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpParams"]().set('gameId', gameId.toString()) } : {};
        return this.http.get('StartGame/StartRound', options);
    };
    HttpService.prototype.DoRoundFirstPhase = function (gameId, humanGamePlayerId, bet) {
        var body = { GameId: gameId, Bet: bet, GamePlayerId: humanGamePlayerId };
        return this.http.post('GameLogic/DoRoundFirstPhase', body);
    };
    HttpService.prototype.DoRoundSecondPhase = function (gameId, humanBlackJackContinueRound) {
        var body = { GameId: gameId, HumanBlackJackAndDealerBlackJackDanger: humanBlackJackContinueRound };
        return this.http.post('GameLogic/DoRoundSecondPhase', body);
    };
    HttpService.prototype.AddOneMoreCardToHuman = function (gameId) {
        var options = gameId ?
            { params: new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpParams"]().set('gameId', gameId.toString()) } : {};
        return this.http.get('GameLogic/AddOneMoreCardToHuman', options);
    };
    HttpService.prototype.ResumeGameAfterRoundFirstPhase = function (gameId) {
        var options = gameId ?
            { params: new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpParams"]().set('gameId', gameId.toString()) } : {};
        return this.http.get('GameLogic/ResumeGameAfterRoundFirstPhase', options);
    };
    HttpService.prototype.ResumeGameAfterRoundSecondPhase = function (gameId) {
        var options = gameId ?
            { params: new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpParams"]().set('gameId', gameId.toString()) } : {};
        return this.http.get('GameLogic/ResumeGameAfterRoundSecondPhase', options);
    };
    HttpService.prototype.EndRound = function (gameId) {
        var options = gameId ?
            { params: new _angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpParams"]().set('gameId', gameId.toString()) } : {};
        return this.http.get('GameLogic/EndRound', options);
    };
    HttpService.prototype.EndGame = function (gameId, gameResult) {
        var body = { GameId: gameId, GameResult: gameResult };
        return this.http.post('GameLogic/EndGame', body);
    };
    HttpService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])({
            providedIn: 'root'
        }),
        __metadata("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpClient"]])
    ], HttpService);
    return HttpService;
}());



/***/ }),

/***/ "./src/environments/environment.ts":
/*!*****************************************!*\
  !*** ./src/environments/environment.ts ***!
  \*****************************************/
/*! exports provided: environment */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "environment", function() { return environment; });
// This file can be replaced during build by using the `fileReplacements` array.
// `ng build ---prod` replaces `environment.ts` with `environment.prod.ts`.
// The list of file replacements can be found in `angular.json`.
var environment = {
    production: false
};
/*
 * In development mode, for easier debugging, you can ignore zone related error
 * stack frames such as `zone.run`/`zoneDelegate.invokeTask` by importing the
 * below file. Don't forget to comment it out in production mode
 * because it will have a performance impact when errors are thrown
 */
// import 'zone.js/dist/zone-error';  // Included with Angular CLI.


/***/ }),

/***/ "./src/main.ts":
/*!*********************!*\
  !*** ./src/main.ts ***!
  \*********************/
/*! no exports provided */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_platform_browser_dynamic__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/platform-browser-dynamic */ "./node_modules/@angular/platform-browser-dynamic/fesm5/platform-browser-dynamic.js");
/* harmony import */ var _app_app_module__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./app/app.module */ "./src/app/app.module.ts");
/* harmony import */ var _environments_environment__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./environments/environment */ "./src/environments/environment.ts");




if (_environments_environment__WEBPACK_IMPORTED_MODULE_3__["environment"].production) {
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["enableProdMode"])();
}
Object(_angular_platform_browser_dynamic__WEBPACK_IMPORTED_MODULE_1__["platformBrowserDynamic"])().bootstrapModule(_app_app_module__WEBPACK_IMPORTED_MODULE_2__["AppModule"])
    .catch(function (err) { return console.log(err); });


/***/ }),

/***/ 0:
/*!***************************!*\
  !*** multi ./src/main.ts ***!
  \***************************/
/*! no static exports found */
/***/ (function(module, exports, __webpack_require__) {

module.exports = __webpack_require__(/*! C:\Users\Anuitex\source\repos\BlackJack\BlackJack.Angular\src\main.ts */"./src/main.ts");


/***/ })

},[[0,"runtime","vendor"]]]);
//# sourceMappingURL=main.js.map