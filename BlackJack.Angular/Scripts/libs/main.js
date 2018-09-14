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
/* harmony import */ var _auth_user_startpage_startpage_component__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! ./auth-user/startpage/startpage.component */ "./src/app/auth-user/startpage/startpage.component.ts");
/* harmony import */ var _auth_user_auth_user_component__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! ./auth-user/auth-user.component */ "./src/app/auth-user/auth-user.component.ts");
/* harmony import */ var _auth_user_game_game_component__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! ./auth-user/game/game.component */ "./src/app/auth-user/game/game.component.ts");
/* harmony import */ var _auth_user_game_player_output_player_output_component__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! ./auth-user/game/player-output/player-output.component */ "./src/app/auth-user/game/player-output/player-output.component.ts");
/* harmony import */ var _auth_user_game_dealer_output_dealer_output_component__WEBPACK_IMPORTED_MODULE_12__ = __webpack_require__(/*! ./auth-user/game/dealer-output/dealer-output.component */ "./src/app/auth-user/game/dealer-output/dealer-output.component.ts");
/* harmony import */ var _auth_user_game_bet_input_bet_input_component__WEBPACK_IMPORTED_MODULE_13__ = __webpack_require__(/*! ./auth-user/game/bet-input/bet-input.component */ "./src/app/auth-user/game/bet-input/bet-input.component.ts");
/* harmony import */ var _auth_user_game_take_card_take_card_component__WEBPACK_IMPORTED_MODULE_14__ = __webpack_require__(/*! ./auth-user/game/take-card/take-card.component */ "./src/app/auth-user/game/take-card/take-card.component.ts");
/* harmony import */ var _auth_user_game_end_round_end_round_component__WEBPACK_IMPORTED_MODULE_15__ = __webpack_require__(/*! ./auth-user/game/end-round/end-round.component */ "./src/app/auth-user/game/end-round/end-round.component.ts");
/* harmony import */ var _error_page_error_page_component__WEBPACK_IMPORTED_MODULE_16__ = __webpack_require__(/*! ./error-page/error-page.component */ "./src/app/error-page/error-page.component.ts");
/* harmony import */ var _auth_user_game_blackjack_danger_choice_blackjack_danger_choice_component__WEBPACK_IMPORTED_MODULE_17__ = __webpack_require__(/*! ./auth-user/game/blackjack-danger-choice/blackjack-danger-choice.component */ "./src/app/auth-user/game/blackjack-danger-choice/blackjack-danger-choice.component.ts");
/* harmony import */ var _services_data_service__WEBPACK_IMPORTED_MODULE_18__ = __webpack_require__(/*! ./services/data.service */ "./src/app/services/data.service.ts");
/* harmony import */ var _services_error_service__WEBPACK_IMPORTED_MODULE_19__ = __webpack_require__(/*! ./services/error.service */ "./src/app/services/error.service.ts");
/* harmony import */ var _services_http_service__WEBPACK_IMPORTED_MODULE_20__ = __webpack_require__(/*! ./services/http.service */ "./src/app/services/http.service.ts");
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
        component: _auth_user_auth_user_component__WEBPACK_IMPORTED_MODULE_9__["AuthUserComponent"],
        children: [
            { path: '', component: _auth_user_startpage_startpage_component__WEBPACK_IMPORTED_MODULE_8__["StartpageComponent"] },
            { path: 'game/:Id', component: _auth_user_game_game_component__WEBPACK_IMPORTED_MODULE_10__["GameComponent"] }
        ]
    },
    { path: 'error', component: _error_page_error_page_component__WEBPACK_IMPORTED_MODULE_16__["ErrorPageComponent"] }
];
var AppModule = /** @class */ (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["NgModule"])({
            declarations: [
                _app_component__WEBPACK_IMPORTED_MODULE_6__["AppComponent"],
                _homepage_homepage_component__WEBPACK_IMPORTED_MODULE_7__["HomepageComponent"],
                _auth_user_startpage_startpage_component__WEBPACK_IMPORTED_MODULE_8__["StartpageComponent"],
                _auth_user_auth_user_component__WEBPACK_IMPORTED_MODULE_9__["AuthUserComponent"],
                _auth_user_game_game_component__WEBPACK_IMPORTED_MODULE_10__["GameComponent"],
                _auth_user_game_player_output_player_output_component__WEBPACK_IMPORTED_MODULE_11__["PlayerOutputComponent"],
                _auth_user_game_dealer_output_dealer_output_component__WEBPACK_IMPORTED_MODULE_12__["DealerOutputComponent"],
                _auth_user_game_bet_input_bet_input_component__WEBPACK_IMPORTED_MODULE_13__["BetInputComponent"],
                _auth_user_game_take_card_take_card_component__WEBPACK_IMPORTED_MODULE_14__["TakeCardComponent"],
                _auth_user_game_end_round_end_round_component__WEBPACK_IMPORTED_MODULE_15__["EndRoundComponent"],
                _error_page_error_page_component__WEBPACK_IMPORTED_MODULE_16__["ErrorPageComponent"],
                _auth_user_game_blackjack_danger_choice_blackjack_danger_choice_component__WEBPACK_IMPORTED_MODULE_17__["BlackjackDangerChoiceComponent"]
            ],
            imports: [
                _angular_platform_browser__WEBPACK_IMPORTED_MODULE_0__["BrowserModule"],
                _angular_forms__WEBPACK_IMPORTED_MODULE_2__["FormsModule"],
                _angular_common_http__WEBPACK_IMPORTED_MODULE_3__["HttpClientModule"],
                _angular_router__WEBPACK_IMPORTED_MODULE_4__["RouterModule"].forRoot(appRoutes, { useHash: true })
            ],
            providers: [
                _services_data_service__WEBPACK_IMPORTED_MODULE_18__["DataService"],
                _services_error_service__WEBPACK_IMPORTED_MODULE_19__["ErrorService"],
                _services_http_service__WEBPACK_IMPORTED_MODULE_20__["HttpService"],
                { provide: _angular_common__WEBPACK_IMPORTED_MODULE_5__["APP_BASE_HREF"], useValue: '/' }
            ],
            bootstrap: [
                _app_component__WEBPACK_IMPORTED_MODULE_6__["AppComponent"],
                _homepage_homepage_component__WEBPACK_IMPORTED_MODULE_7__["HomepageComponent"],
                _auth_user_startpage_startpage_component__WEBPACK_IMPORTED_MODULE_8__["StartpageComponent"],
                _auth_user_auth_user_component__WEBPACK_IMPORTED_MODULE_9__["AuthUserComponent"],
                _auth_user_game_game_component__WEBPACK_IMPORTED_MODULE_10__["GameComponent"],
                _auth_user_game_player_output_player_output_component__WEBPACK_IMPORTED_MODULE_11__["PlayerOutputComponent"],
                _auth_user_game_dealer_output_dealer_output_component__WEBPACK_IMPORTED_MODULE_12__["DealerOutputComponent"],
                _auth_user_game_bet_input_bet_input_component__WEBPACK_IMPORTED_MODULE_13__["BetInputComponent"],
                _auth_user_game_take_card_take_card_component__WEBPACK_IMPORTED_MODULE_14__["TakeCardComponent"],
                _auth_user_game_end_round_end_round_component__WEBPACK_IMPORTED_MODULE_15__["EndRoundComponent"],
                _error_page_error_page_component__WEBPACK_IMPORTED_MODULE_16__["ErrorPageComponent"],
                _auth_user_game_blackjack_danger_choice_blackjack_danger_choice_component__WEBPACK_IMPORTED_MODULE_17__["BlackjackDangerChoiceComponent"]
            ]
        })
    ], AppModule);
    return AppModule;
}());



/***/ }),

/***/ "./src/app/auth-user/auth-user.component.css":
/*!***************************************************!*\
  !*** ./src/app/auth-user/auth-user.component.css ***!
  \***************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ""

/***/ }),

/***/ "./src/app/auth-user/auth-user.component.html":
/*!****************************************************!*\
  !*** ./src/app/auth-user/auth-user.component.html ***!
  \****************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<nav class=\"navbar navbar-default\">\r\n    <div class=\"container\">\r\n        <div class=\"navbar-header\">\r\n            <button type=\"button\" class=\"navbar-toggle\" data-toggle=\"collapse\" data-target=\"#myNavbar\">\r\n                <span class=\"icon-bar\"></span>\r\n                <span class=\"icon-bar\"></span>\r\n                <span class=\"icon-bar\"></span>\r\n            </button>\r\n            <a class=\"navbar-brand\" [routerLink]=\"['/user', UserName]\">\r\n                Home\r\n            </a>\r\n        </div>\r\n\r\n        <div class=\"collapse navbar-collapse\" id=\"myNavbar\">\r\n            <ul class=\"nav navbar-nav\">\r\n                <li class=\"navbar-text\">\r\n                    <b>Log in as: {{UserName}}</b>\r\n                </li>\r\n                <li class=\"nav-item\">\r\n                    <a routerLink=\"/\">\r\n                        Log out\r\n                    </a>\r\n                </li>\r\n            </ul>\r\n        </div>\r\n    </div>\r\n</nav>\r\n<router-outlet></router-outlet>  "

/***/ }),

/***/ "./src/app/auth-user/auth-user.component.ts":
/*!**************************************************!*\
  !*** ./src/app/auth-user/auth-user.component.ts ***!
  \**************************************************/
/*! exports provided: AuthUserComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AuthUserComponent", function() { return AuthUserComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _services_data_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../services/data.service */ "./src/app/services/data.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



var AuthUserComponent = /** @class */ (function () {
    function AuthUserComponent(_dataService, _route) {
        this._dataService = _dataService;
        this._route = _route;
    }
    AuthUserComponent.prototype.ngOnInit = function () {
        var _this = this;
        this._route.params.subscribe(function (params) {
            _this.UserName = params['UserName'];
        });
        this._dataService.SetUserName(this.UserName);
    };
    AuthUserComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-auth-user',
            template: __webpack_require__(/*! ./auth-user.component.html */ "./src/app/auth-user/auth-user.component.html"),
            styles: [__webpack_require__(/*! ./auth-user.component.css */ "./src/app/auth-user/auth-user.component.css")]
        }),
        __metadata("design:paramtypes", [_services_data_service__WEBPACK_IMPORTED_MODULE_2__["DataService"],
            _angular_router__WEBPACK_IMPORTED_MODULE_1__["ActivatedRoute"]])
    ], AuthUserComponent);
    return AuthUserComponent;
}());



/***/ }),

/***/ "./src/app/auth-user/game/bet-input/bet-input.component.css":
/*!******************************************************************!*\
  !*** ./src/app/auth-user/game/bet-input/bet-input.component.css ***!
  \******************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ""

/***/ }),

/***/ "./src/app/auth-user/game/bet-input/bet-input.component.html":
/*!*******************************************************************!*\
  !*** ./src/app/auth-user/game/bet-input/bet-input.component.html ***!
  \*******************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<label class=\"control-label\">Enter your bet: </label>\r\n<input [(ngModel)]=\"Bet\" type=\"number\" \r\n       class=\"form-control\" step=\"50\" min=\"50\" value=\"50\" max={{Score}}  />\r\n<br />\r\n<div *ngIf=\"ValidationError\">\r\n    <div class=\"alert alert-danger\">{{ValidationMessage}}</div>\r\n</div>\r\n<button class=\"btn btn-primary\" (click)=\"EnterBet()\">Enter</button>\r\n"

/***/ }),

/***/ "./src/app/auth-user/game/bet-input/bet-input.component.ts":
/*!*****************************************************************!*\
  !*** ./src/app/auth-user/game/bet-input/bet-input.component.ts ***!
  \*****************************************************************/
/*! exports provided: BetInputComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "BetInputComponent", function() { return BetInputComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _services_http_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../../../services/http.service */ "./src/app/services/http.service.ts");
/* harmony import */ var _services_error_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../../services/error.service */ "./src/app/services/error.service.ts");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};




var BetInputComponent = /** @class */ (function () {
    function BetInputComponent(_httpService, _errorService, _router) {
        this._httpService = _httpService;
        this._errorService = _errorService;
        this._router = _router;
        this.ValidationError = false;
        this.Bet = 50;
        this.BetOut = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
    }
    BetInputComponent.prototype.EnterBet = function () {
        var _this = this;
        this._httpService.BetsCreation(this.GameId, this.HumanGamePlayerId, this.Bet)
            .subscribe(function (data) {
            _this.ValidationMessage = data["Message"];
            _this.OnValidate();
        }, function (error) {
            console.log(error);
            _this._errorService.SetError(error["error"]["Message"]);
            _this._router.navigate(['/error']);
        });
    };
    BetInputComponent.prototype.OnValidate = function () {
        if (this.ValidationMessage != "") {
            this.ValidationError = true;
        }
        if (this.ValidationMessage == "") {
            this.BetOut.emit();
        }
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Number)
    ], BetInputComponent.prototype, "Score", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Number)
    ], BetInputComponent.prototype, "GameId", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Number)
    ], BetInputComponent.prototype, "HumanGamePlayerId", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Output"])(),
        __metadata("design:type", Object)
    ], BetInputComponent.prototype, "BetOut", void 0);
    BetInputComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-bet-input',
            template: __webpack_require__(/*! ./bet-input.component.html */ "./src/app/auth-user/game/bet-input/bet-input.component.html"),
            styles: [__webpack_require__(/*! ./bet-input.component.css */ "./src/app/auth-user/game/bet-input/bet-input.component.css")]
        }),
        __metadata("design:paramtypes", [_services_http_service__WEBPACK_IMPORTED_MODULE_1__["HttpService"],
            _services_error_service__WEBPACK_IMPORTED_MODULE_2__["ErrorService"],
            _angular_router__WEBPACK_IMPORTED_MODULE_3__["Router"]])
    ], BetInputComponent);
    return BetInputComponent;
}());



/***/ }),

/***/ "./src/app/auth-user/game/blackjack-danger-choice/blackjack-danger-choice.component.css":
/*!**********************************************************************************************!*\
  !*** ./src/app/auth-user/game/blackjack-danger-choice/blackjack-danger-choice.component.css ***!
  \**********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ""

/***/ }),

/***/ "./src/app/auth-user/game/blackjack-danger-choice/blackjack-danger-choice.component.html":
/*!***********************************************************************************************!*\
  !*** ./src/app/auth-user/game/blackjack-danger-choice/blackjack-danger-choice.component.html ***!
  \***********************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<p>You have BlackJack and dealer has BlackJack-danger</p>\r\n<button class=\"btn btn-primary\" (click)=\"OnContinue(false)\">Continue round</button>\r\n<button class=\"btn btn-primary\" (click)=\"OnContinue(true)\">Take award (1:1)</button>"

/***/ }),

/***/ "./src/app/auth-user/game/blackjack-danger-choice/blackjack-danger-choice.component.ts":
/*!*********************************************************************************************!*\
  !*** ./src/app/auth-user/game/blackjack-danger-choice/blackjack-danger-choice.component.ts ***!
  \*********************************************************************************************/
/*! exports provided: BlackjackDangerChoiceComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "BlackjackDangerChoiceComponent", function() { return BlackjackDangerChoiceComponent; });
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

var BlackjackDangerChoiceComponent = /** @class */ (function () {
    function BlackjackDangerChoiceComponent() {
        this.TakeAward = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
    }
    BlackjackDangerChoiceComponent.prototype.OnContinue = function (takeAward) {
        this.TakeAward.emit(takeAward);
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Output"])(),
        __metadata("design:type", Object)
    ], BlackjackDangerChoiceComponent.prototype, "TakeAward", void 0);
    BlackjackDangerChoiceComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-blackjack-danger-choice',
            template: __webpack_require__(/*! ./blackjack-danger-choice.component.html */ "./src/app/auth-user/game/blackjack-danger-choice/blackjack-danger-choice.component.html"),
            styles: [__webpack_require__(/*! ./blackjack-danger-choice.component.css */ "./src/app/auth-user/game/blackjack-danger-choice/blackjack-danger-choice.component.css")]
        }),
        __metadata("design:paramtypes", [])
    ], BlackjackDangerChoiceComponent);
    return BlackjackDangerChoiceComponent;
}());



/***/ }),

/***/ "./src/app/auth-user/game/dealer-output/dealer-output.component.css":
/*!**************************************************************************!*\
  !*** ./src/app/auth-user/game/dealer-output/dealer-output.component.css ***!
  \**************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ""

/***/ }),

/***/ "./src/app/auth-user/game/dealer-output/dealer-output.component.html":
/*!***************************************************************************!*\
  !*** ./src/app/auth-user/game/dealer-output/dealer-output.component.html ***!
  \***************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<p>Score: {{Score}}</p>\r\n\r\n<div *ngIf=\"RoundFirstPhase\">\r\n    <p>Card:</p>\r\n    <ul>\r\n        <li *ngFor=\"let card of Cards\">{{card}}</li>\r\n    </ul>\r\n</div>\r\n\r\n<div *ngIf=\"RoundSecondPhase\">\r\n    <p>CardScore: {{RoundScore}}</p>\r\n    <p>Cards:</p>\r\n    <ul>\r\n        <li *ngFor=\"let card of Cards\">{{card}}</li>\r\n    </ul>\r\n</div>"

/***/ }),

/***/ "./src/app/auth-user/game/dealer-output/dealer-output.component.ts":
/*!*************************************************************************!*\
  !*** ./src/app/auth-user/game/dealer-output/dealer-output.component.ts ***!
  \*************************************************************************/
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
            template: __webpack_require__(/*! ./dealer-output.component.html */ "./src/app/auth-user/game/dealer-output/dealer-output.component.html"),
            styles: [__webpack_require__(/*! ./dealer-output.component.css */ "./src/app/auth-user/game/dealer-output/dealer-output.component.css")]
        }),
        __metadata("design:paramtypes", [])
    ], DealerOutputComponent);
    return DealerOutputComponent;
}());



/***/ }),

/***/ "./src/app/auth-user/game/end-round/end-round.component.css":
/*!******************************************************************!*\
  !*** ./src/app/auth-user/game/end-round/end-round.component.css ***!
  \******************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ""

/***/ }),

/***/ "./src/app/auth-user/game/end-round/end-round.component.html":
/*!*******************************************************************!*\
  !*** ./src/app/auth-user/game/end-round/end-round.component.html ***!
  \*******************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div *ngIf=\"IsEndRound\">\r\n    <p>{{RoundResult}}</p>\r\n    <button class=\"btn btn-primary\" (click)=\"EndRound()\">End round</button>\r\n</div>\r\n\r\n<div *ngIf=\"IsGameOver\">\r\n    <p>{{GameOver}}</p>\r\n    <button class=\"btn btn-primary\" (click)=\"StartNewGame()\">End round</button>\r\n</div>"

/***/ }),

/***/ "./src/app/auth-user/game/end-round/end-round.component.ts":
/*!*****************************************************************!*\
  !*** ./src/app/auth-user/game/end-round/end-round.component.ts ***!
  \*****************************************************************/
/*! exports provided: EndRoundComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "EndRoundComponent", function() { return EndRoundComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _services_http_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../../../services/http.service */ "./src/app/services/http.service.ts");
/* harmony import */ var _services_error_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../../services/error.service */ "./src/app/services/error.service.ts");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};




var EndRoundComponent = /** @class */ (function () {
    function EndRoundComponent(_httpService, _errorService, _router) {
        this._httpService = _httpService;
        this._errorService = _errorService;
        this._router = _router;
        this.Reload = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
        this.IsEndRound = true;
        this.IsGameOver = false;
    }
    EndRoundComponent.prototype.ngOnInit = function () {
        var _this = this;
        this._httpService.HumanRoundResult(this.GameId)
            .subscribe(function (data) {
            _this.RoundResult = data["RoundResult"];
        }, function (error) {
            console.log(error);
            _this._errorService.SetError(error["error"]["Message"]);
            _this._router.navigate(['/error']);
        });
    };
    EndRoundComponent.prototype.EndRound = function () {
        var _this = this;
        this._httpService.UpdateGamePlayersForNewRound(this.GameId)
            .subscribe(function (data) {
            if (data["IsGameOver"] != "") {
                _this.IsGameOver = true;
                _this.IsEndRound = false;
                _this.GameOver = data["IsGameOver"];
            }
            if (data["IsGameOver"] == "") {
                _this.Reload.emit();
            }
        }, function (error) {
            console.log(error);
            _this._errorService.SetError(error["error"]["Message"]);
            _this._router.navigate(['/error']);
        });
    };
    EndRoundComponent.prototype.StartNewGame = function () {
        this._router.navigate(['/user/' + this.UserName]);
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", String)
    ], EndRoundComponent.prototype, "UserName", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Input"])(),
        __metadata("design:type", Number)
    ], EndRoundComponent.prototype, "GameId", void 0);
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Output"])(),
        __metadata("design:type", Object)
    ], EndRoundComponent.prototype, "Reload", void 0);
    EndRoundComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-end-round',
            template: __webpack_require__(/*! ./end-round.component.html */ "./src/app/auth-user/game/end-round/end-round.component.html"),
            styles: [__webpack_require__(/*! ./end-round.component.css */ "./src/app/auth-user/game/end-round/end-round.component.css")]
        }),
        __metadata("design:paramtypes", [_services_http_service__WEBPACK_IMPORTED_MODULE_1__["HttpService"],
            _services_error_service__WEBPACK_IMPORTED_MODULE_2__["ErrorService"],
            _angular_router__WEBPACK_IMPORTED_MODULE_3__["Router"]])
    ], EndRoundComponent);
    return EndRoundComponent;
}());



/***/ }),

/***/ "./src/app/auth-user/game/game.component.css":
/*!***************************************************!*\
  !*** ./src/app/auth-user/game/game.component.css ***!
  \***************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ""

/***/ }),

/***/ "./src/app/auth-user/game/game.component.html":
/*!****************************************************!*\
  !*** ./src/app/auth-user/game/game.component.html ***!
  \****************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<div class=\"row row-flex\">\r\n    <div class=\"col-lg-4 col-md-4 col-sm-4 col-xs-12 well\">\r\n        <h4><span class=\"label label-danger\">Dealer</span></h4>\r\n        <p>Name: {{GameViewModel.Dealer.Name}}</p>\r\n        <app-dealer-output [Score]=\"GameViewModel.Dealer.Score\" [RoundScore]=\"GameViewModel.Dealer.RoundScore\" [Cards]=\"GameViewModel.Dealer.Cards\" [GameStage]=\"GameViewModel.Stage\"></app-dealer-output>\r\n    </div>\r\n\r\n    <div class=\"col-lg-8 col-md-8 col-sm-8 col-xs-12 well\">\r\n        <div class=\"row\">\r\n            <div class=\"col-lg-6 col-md-6 col-sm-6 col-xs-12\">\r\n                <h4><span class=\"label label-primary\">Human</span></h4>\r\n                <p>Name: {{GameViewModel.Human.Name}}</p>\r\n                <app-player-output [Score]=\"GameViewModel.Human.Score\" [Bet]=\"GameViewModel.Human.Bet\" [RoundScore]=\"GameViewModel.Human.RoundScore\" [Cards]=\"GameViewModel.Human.Cards\" [GameStage]=\"GameViewModel.Stage\"></app-player-output>\r\n            </div>\r\n            <div class=\"col-lg-6 col-md-6 col-sm-6 col-xs-12\">\r\n                <div *ngIf=\"BetInput\">\r\n                    <app-bet-input [Score]=\"GameViewModel.Human.Score\" [GameId]=\"GameId\" [HumanGamePlayerId]=\"GameViewModel.Human.GamePlayerId\" (BetOut)=\"OnBetsCreation()\"></app-bet-input>\r\n                </div>\r\n                <div *ngIf=\"TakeCard\">\r\n                    <app-take-card (TakeCard)=\"OnTakingCard($event)\"></app-take-card>\r\n                </div>\r\n                <div *ngIf=\"BlackJackDangerChoice\">\r\n                    <app-blackjack-danger-choice (TakeAward)=\"OnBlackJackDangerChoice($event)\"></app-blackjack-danger-choice>\r\n                </div>\r\n                <div *ngIf=\"EndRound\">\r\n                    <app-end-round [GameId]=\"GameId\" [UserName]=\"GameViewModel.Human.Name\" (Reload)=\"Reload()\"></app-end-round>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n<div class=\"row row-flex\">\r\n    <div *ngFor=\"let bot of GameViewModel.Bots\" class=\"col-lg-2 col-md-4 col-sm-4 col-xs-6 well\">\r\n        <h4><span class=\"label label-default\">Bot</span></h4>\r\n        <p>Name: {{bot.Name}}</p>\r\n        <app-player-output [Score]=\"bot.Score\" [RoundScore]=\"bot.RoundScore\" [Bet]=\"bot.Bet\" [Cards]=\"bot.Cards\" [GameStage]=\"GameViewModel.Stage\"></app-player-output>\r\n    </div>\r\n</div>\r\n"

/***/ }),

/***/ "./src/app/auth-user/game/game.component.ts":
/*!**************************************************!*\
  !*** ./src/app/auth-user/game/game.component.ts ***!
  \**************************************************/
/*! exports provided: GameComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GameComponent", function() { return GameComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _services_http_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../services/http.service */ "./src/app/services/http.service.ts");
/* harmony import */ var _services_error_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../../services/error.service */ "./src/app/services/error.service.ts");
/* harmony import */ var json_typescript_mapper__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! json-typescript-mapper */ "./node_modules/json-typescript-mapper/index.js");
/* harmony import */ var json_typescript_mapper__WEBPACK_IMPORTED_MODULE_4___default = /*#__PURE__*/__webpack_require__.n(json_typescript_mapper__WEBPACK_IMPORTED_MODULE_4__);
/* harmony import */ var _viewmodels_GameViewModel__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../../viewmodels/GameViewModel */ "./src/app/viewmodels/GameViewModel.ts");
/* harmony import */ var _viewmodels_PlayerViewModel__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! ../../viewmodels/PlayerViewModel */ "./src/app/viewmodels/PlayerViewModel.ts");
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
        this.BetInput = false;
        this.TakeCard = false;
        this.BlackJackDangerChoice = false;
        this.EndRound = false;
    }
    GameComponent.prototype.ngOnInit = function () {
        var _this = this;
        this._route.params.subscribe(function (params) {
            _this.GameId = params['Id'];
            _this.GetGame();
        });
    };
    GameComponent.prototype.GamePlayInitializer = function () {
        if (this.GameViewModel.Stage == 0) {
            this.GamePlayBetInput();
        }
        if (this.GameViewModel.Stage == 1) {
            this.HumanUpdate();
            this.BotsUpdate();
            this.DealerFirstPhaseUpdate();
            this.FirstPhaseGamePlay();
        }
        if (this.GameViewModel.Stage == 2) {
            this.HumanUpdate();
            this.BotsUpdate();
            this.DealerSecondPhaseUpdate();
            this.GamePlayEndRound();
        }
    };
    GameComponent.prototype.GetGame = function () {
        var _this = this;
        this._httpService.GetGame(this.GameId)
            .subscribe(function (data) {
            _this.GameViewModel = Object(json_typescript_mapper__WEBPACK_IMPORTED_MODULE_4__["deserialize"])(_viewmodels_GameViewModel__WEBPACK_IMPORTED_MODULE_5__["GameViewModel"], data);
            _this.GamePlayInitializer();
        }, function (error) {
            console.log(error);
            _this._errorService.SetError(error["error"]["Message"]);
            _this._router.navigate(['/error']);
        });
    };
    GameComponent.prototype.OnBetsCreation = function () {
        this.FirstPhase();
    };
    GameComponent.prototype.OnBlackJackDangerChoice = function (takeAward) {
        var _this = this;
        if (!takeAward) {
            this._httpService.BlackJackDangerContinueRound(this.GameId)
                .subscribe(function (error) {
                console.log(error);
                _this._errorService.SetError(error["error"]["Message"]);
                _this._router.navigate(['/error']);
            });
        }
        this.SecondPhase();
    };
    GameComponent.prototype.OnTakingCard = function (takeCard) {
        var _this = this;
        if (takeCard) {
            this._httpService.AddOneMoreCardToHuman(this.GameId)
                .subscribe(function (data) {
                _this.HumanUpdate();
                _this.GamePlayInitializer();
            }, function (error) {
                console.log(error);
                _this._errorService.SetError(error["error"]["Message"]);
                _this._router.navigate(['/error']);
            });
        }
        if (!takeCard) {
            this.SecondPhase();
        }
    };
    GameComponent.prototype.FirstPhaseGamePlay = function () {
        var _this = this;
        this._httpService.FirstPhaseGamePlay(this.GameId)
            .subscribe(function (data) {
            _this.HumanUpdate();
            _this.BotsUpdate();
            _this.DealerFirstPhaseUpdate();
            if (data["HumanBlackJackAndDealerBlackJackDanger"]) {
                _this.GamePlayBlackJackDangerChoice();
            }
            if (data["CanHumanTakeOneMoreCard"]) {
                _this.GamePlayTakeCard();
            }
            if (!(data["HumanBlackJackAndDealerBlackJackDanger"]) && !(data["CanHumanTakeOneMoreCard"])) {
                _this.SecondPhase();
            }
        }, function (error) {
            console.log(error);
            _this._errorService.SetError(error["error"]["Message"]);
            _this._router.navigate(['/error']);
        });
    };
    GameComponent.prototype.FirstPhase = function () {
        var _this = this;
        this._httpService.RoundStart(this.GameId)
            .subscribe(function (data) {
            _this.HumanUpdate();
            _this.BotsUpdate();
            _this.DealerFirstPhaseUpdate();
            _this.GameViewModel.Stage = 1;
            _this.GamePlayInitializer();
        }, function (error) {
            console.log(error);
            _this._errorService.SetError(error["error"]["Message"]);
            _this._router.navigate(['/error']);
        });
    };
    GameComponent.prototype.SecondPhase = function () {
        var _this = this;
        this._httpService.SecondPhase(this.GameId)
            .subscribe(function (data) {
            _this.HumanUpdate();
            _this.BotsUpdate();
            _this.DealerSecondPhaseUpdate();
            _this.GameViewModel.Stage = 2;
            _this.GamePlayInitializer();
        }, function (error) {
            console.log(error);
            _this._errorService.SetError(error["error"]["Message"]);
            _this._router.navigate(['/error']);
        });
    };
    GameComponent.prototype.HumanUpdate = function () {
        var _this = this;
        this._httpService.GetGamePlayer(this.GameViewModel.Human.GamePlayerId)
            .subscribe(function (data) {
            var name = _this.GameViewModel.Human.Name;
            _this.GameViewModel.Human = Object(json_typescript_mapper__WEBPACK_IMPORTED_MODULE_4__["deserialize"])(_viewmodels_PlayerViewModel__WEBPACK_IMPORTED_MODULE_6__["PlayerViewModel"], data);
            _this.GameViewModel.Human.Name = name;
        }, function (error) {
            console.log(error);
            _this._errorService.SetError(error["error"]["Message"]);
            _this._router.navigate(['/error']);
        });
    };
    GameComponent.prototype.DealerFirstPhaseUpdate = function () {
        var _this = this;
        this._httpService.GetDealerFirstPhase(this.GameViewModel.Dealer.GamePlayerId)
            .subscribe(function (data) {
            var name = _this.GameViewModel.Dealer.Name;
            _this.GameViewModel.Dealer = Object(json_typescript_mapper__WEBPACK_IMPORTED_MODULE_4__["deserialize"])(_viewmodels_PlayerViewModel__WEBPACK_IMPORTED_MODULE_6__["PlayerViewModel"], data);
            _this.GameViewModel.Dealer.Name = name;
        }, function (error) {
            console.log(error);
            _this._errorService.SetError(error["error"]["Message"]);
            _this._router.navigate(['/error']);
        });
    };
    GameComponent.prototype.DealerSecondPhaseUpdate = function () {
        var _this = this;
        this._httpService.GetDealerSecondPhase(this.GameViewModel.Dealer.GamePlayerId)
            .subscribe(function (data) {
            var name = _this.GameViewModel.Dealer.Name;
            _this.GameViewModel.Dealer = Object(json_typescript_mapper__WEBPACK_IMPORTED_MODULE_4__["deserialize"])(_viewmodels_PlayerViewModel__WEBPACK_IMPORTED_MODULE_6__["PlayerViewModel"], data);
            _this.GameViewModel.Dealer.Name = name;
        }, function (error) {
            console.log(error);
            _this._errorService.SetError(error["error"]["Message"]);
            _this._router.navigate(['/error']);
        });
    };
    GameComponent.prototype.BotsUpdate = function () {
        var _this = this;
        this.GameViewModel.Bots.forEach(function (bot) {
            _this._httpService.GetGamePlayer(bot.GamePlayerId)
                .subscribe(function (data) {
                var inBot = Object(json_typescript_mapper__WEBPACK_IMPORTED_MODULE_4__["deserialize"])(_viewmodels_PlayerViewModel__WEBPACK_IMPORTED_MODULE_6__["PlayerViewModel"], data);
                bot.Bet = inBot.Bet;
                bot.Score = inBot.Score;
                bot.RoundScore = inBot.RoundScore;
                bot.Cards = inBot.Cards;
            }, function (error) {
                console.log(error);
                _this._errorService.SetError(error["error"]["Message"]);
                _this._router.navigate(['/error']);
            });
        });
    };
    GameComponent.prototype.GamePlayBetInput = function () {
        this.BetInput = true;
        this.TakeCard = false;
        this.BlackJackDangerChoice = false;
        this.EndRound = false;
    };
    GameComponent.prototype.GamePlayTakeCard = function () {
        this.BetInput = false;
        this.TakeCard = true;
        this.BlackJackDangerChoice = false;
        this.EndRound = false;
    };
    GameComponent.prototype.GamePlayBlackJackDangerChoice = function () {
        this.BetInput = false;
        this.TakeCard = false;
        this.BlackJackDangerChoice = true;
        this.EndRound = false;
    };
    GameComponent.prototype.GamePlayEndRound = function () {
        this.BetInput = false;
        this.TakeCard = false;
        this.BlackJackDangerChoice = false;
        this.EndRound = true;
    };
    GameComponent.prototype.Reload = function () {
        this.GetGame();
    };
    GameComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-game',
            template: __webpack_require__(/*! ./game.component.html */ "./src/app/auth-user/game/game.component.html"),
            styles: [__webpack_require__(/*! ./game.component.css */ "./src/app/auth-user/game/game.component.css")]
        }),
        __metadata("design:paramtypes", [_angular_router__WEBPACK_IMPORTED_MODULE_1__["ActivatedRoute"],
            _angular_router__WEBPACK_IMPORTED_MODULE_1__["Router"],
            _services_http_service__WEBPACK_IMPORTED_MODULE_2__["HttpService"],
            _services_error_service__WEBPACK_IMPORTED_MODULE_3__["ErrorService"]])
    ], GameComponent);
    return GameComponent;
}());



/***/ }),

/***/ "./src/app/auth-user/game/player-output/player-output.component.css":
/*!**************************************************************************!*\
  !*** ./src/app/auth-user/game/player-output/player-output.component.css ***!
  \**************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ""

/***/ }),

/***/ "./src/app/auth-user/game/player-output/player-output.component.html":
/*!***************************************************************************!*\
  !*** ./src/app/auth-user/game/player-output/player-output.component.html ***!
  \***************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<p>Score: {{Score}}</p>\r\n\r\n<div *ngIf=\"!RoundStart\">\r\n    <p>Bet: {{Bet}}</p>\r\n    <p>CardScore: {{RoundScore}}</p>\r\n    <p>Cards:</p>\r\n    <ul>\r\n        <li *ngFor=\"let card of Cards\">{{card}}</li>\r\n    </ul>\r\n</div>"

/***/ }),

/***/ "./src/app/auth-user/game/player-output/player-output.component.ts":
/*!*************************************************************************!*\
  !*** ./src/app/auth-user/game/player-output/player-output.component.ts ***!
  \*************************************************************************/
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
            template: __webpack_require__(/*! ./player-output.component.html */ "./src/app/auth-user/game/player-output/player-output.component.html"),
            styles: [__webpack_require__(/*! ./player-output.component.css */ "./src/app/auth-user/game/player-output/player-output.component.css")]
        }),
        __metadata("design:paramtypes", [])
    ], PlayerOutputComponent);
    return PlayerOutputComponent;
}());



/***/ }),

/***/ "./src/app/auth-user/game/take-card/take-card.component.css":
/*!******************************************************************!*\
  !*** ./src/app/auth-user/game/take-card/take-card.component.css ***!
  \******************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ""

/***/ }),

/***/ "./src/app/auth-user/game/take-card/take-card.component.html":
/*!*******************************************************************!*\
  !*** ./src/app/auth-user/game/take-card/take-card.component.html ***!
  \*******************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<button class=\"btn btn-primary\" (click)=\"OnContinue(true)\">Take card</button>\r\n<button class=\"btn btn-primary\" (click)=\"OnContinue(false)\">Don't take</button>"

/***/ }),

/***/ "./src/app/auth-user/game/take-card/take-card.component.ts":
/*!*****************************************************************!*\
  !*** ./src/app/auth-user/game/take-card/take-card.component.ts ***!
  \*****************************************************************/
/*! exports provided: TakeCardComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "TakeCardComponent", function() { return TakeCardComponent; });
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

var TakeCardComponent = /** @class */ (function () {
    function TakeCardComponent() {
        this.TakeCard = new _angular_core__WEBPACK_IMPORTED_MODULE_0__["EventEmitter"]();
    }
    TakeCardComponent.prototype.OnContinue = function (takeCard) {
        this.TakeCard.emit(takeCard);
    };
    __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Output"])(),
        __metadata("design:type", Object)
    ], TakeCardComponent.prototype, "TakeCard", void 0);
    TakeCardComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-take-card',
            template: __webpack_require__(/*! ./take-card.component.html */ "./src/app/auth-user/game/take-card/take-card.component.html"),
            styles: [__webpack_require__(/*! ./take-card.component.css */ "./src/app/auth-user/game/take-card/take-card.component.css")]
        }),
        __metadata("design:paramtypes", [])
    ], TakeCardComponent);
    return TakeCardComponent;
}());



/***/ }),

/***/ "./src/app/auth-user/startpage/startpage.component.css":
/*!*************************************************************!*\
  !*** ./src/app/auth-user/startpage/startpage.component.css ***!
  \*************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ""

/***/ }),

/***/ "./src/app/auth-user/startpage/startpage.component.html":
/*!**************************************************************!*\
  !*** ./src/app/auth-user/startpage/startpage.component.html ***!
  \**************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<h2>Main page</h2>\r\n<hr />\r\n\r\n<div class=\"row row-flex\">\r\n    <div *ngIf=\"Player.ResumeGame\" class=\"col-lg-6 col-md-6 col-sm-6 col-xs-12 well\">\r\n        <h3>Game resuming</h3>\r\n        <p>You can resume your last game</p>\r\n        <div class=\"form-group\">\r\n            <a class=\"btn btn-primary\" (click)=\"ResumeGame()\">Resume game</a>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"col-lg-6 col-md-6 col-sm-6 col-xs-12 well\">\r\n        <h3>Start new game</h3>\r\n        <label class=\"control-label col-md-4\">Amount of bots:</label>\r\n        <div class=\"col-md-8\">\r\n            <input name=\"amountOfBots\" [(ngModel)]=\"AmountOfBots\" class=\"form-control\"\r\n                   type=\"number\" value=\"0\" min=\"0\" max=\"5\"\r\n                   #amountOfBots=\"ngModel\" pattern=\"[0-5]\" />\r\n            <div [hidden]=\"amountOfBots.valid\" class=\"alert alert-danger\">\r\n                Amount of bots must be more than or equals to 0 and less than or equals to 5.\r\n            </div>\r\n            <br />\r\n            <button [disabled]=\"amountOfBots.invalid\" class=\"btn btn-primary\" (click)=\"StartNewGame()\">Start new game</button>\r\n        </div>\r\n    </div>\r\n</div>"

/***/ }),

/***/ "./src/app/auth-user/startpage/startpage.component.ts":
/*!************************************************************!*\
  !*** ./src/app/auth-user/startpage/startpage.component.ts ***!
  \************************************************************/
/*! exports provided: StartpageComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "StartpageComponent", function() { return StartpageComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _services_data_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../../services/data.service */ "./src/app/services/data.service.ts");
/* harmony import */ var _services_http_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../../services/http.service */ "./src/app/services/http.service.ts");
/* harmony import */ var _services_error_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../../services/error.service */ "./src/app/services/error.service.ts");
/* harmony import */ var _viewmodels_AuthPlayerViewModel__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../../viewmodels/AuthPlayerViewModel */ "./src/app/viewmodels/AuthPlayerViewModel.ts");
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
    function StartpageComponent(_dataService, _httpService, _errorService, _router) {
        this._dataService = _dataService;
        this._httpService = _httpService;
        this._errorService = _errorService;
        this._router = _router;
        this.Player = new _viewmodels_AuthPlayerViewModel__WEBPACK_IMPORTED_MODULE_5__["AuthPlayerViewModel"]();
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
            template: __webpack_require__(/*! ./startpage.component.html */ "./src/app/auth-user/startpage/startpage.component.html"),
            styles: [__webpack_require__(/*! ./startpage.component.css */ "./src/app/auth-user/startpage/startpage.component.css")]
        }),
        __metadata("design:paramtypes", [_services_data_service__WEBPACK_IMPORTED_MODULE_2__["DataService"],
            _services_http_service__WEBPACK_IMPORTED_MODULE_3__["HttpService"],
            _services_error_service__WEBPACK_IMPORTED_MODULE_4__["ErrorService"],
            _angular_router__WEBPACK_IMPORTED_MODULE_1__["Router"]])
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

module.exports = "<h1>Error</h1>\n<p>\n  {{Error}}\n</p>\n"

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
/* harmony import */ var _services_error_service__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../services/error.service */ "./src/app/services/error.service.ts");
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
        __metadata("design:paramtypes", [_services_error_service__WEBPACK_IMPORTED_MODULE_1__["ErrorService"]])
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

/***/ "./src/app/services/data.service.ts":
/*!******************************************!*\
  !*** ./src/app/services/data.service.ts ***!
  \******************************************/
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

/***/ "./src/app/services/error.service.ts":
/*!*******************************************!*\
  !*** ./src/app/services/error.service.ts ***!
  \*******************************************/
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

/***/ "./src/app/services/http.service.ts":
/*!******************************************!*\
  !*** ./src/app/services/http.service.ts ***!
  \******************************************/
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
        var body = { UserName: userName };
        return this.http.post('StartGame/GetAuthorizedPlayer', body);
    };
    HttpService.prototype.CreateNewGame = function (playerId, amountOfBots) {
        var body = { PlayerId: playerId, AmountOfBots: amountOfBots };
        return this.http.post('StartGame/CreateNewGame', body);
    };
    HttpService.prototype.ResumeGame = function (playerId) {
        return this.http.get('StartGame/ResumeGame?playerId=' + playerId);
    };
    HttpService.prototype.GetGame = function (gameId) {
        return this.http.get('StartGame/GetGame?gameId=' + gameId);
    };
    HttpService.prototype.GetGamePlayer = function (gamePlayerId) {
        return this.http.get('PlayerLogic/GetPlayer?gamePlayerId=' + gamePlayerId);
    };
    HttpService.prototype.GetDealerFirstPhase = function (gamePlayerId) {
        return this.http.get('PlayerLogic/GetDealerInFirstPhase?gamePlayerId=' + gamePlayerId);
    };
    HttpService.prototype.GetDealerSecondPhase = function (gamePlayerId) {
        return this.http.get('PlayerLogic/GetDealerInSecondPhase?gamePlayerId=' + gamePlayerId);
    };
    HttpService.prototype.BetsCreation = function (gameId, humanGamePlayerId, bet) {
        var body = { InGameId: gameId, Bet: bet, HumanGamePlayerId: humanGamePlayerId };
        return this.http.post('PlayerLogic/BetsCreation', body);
    };
    HttpService.prototype.RoundStart = function (gameId) {
        return this.http.get('GameLogic/RoundStart?inGameId=' + gameId);
    };
    HttpService.prototype.FirstPhaseGamePlay = function (gameId) {
        return this.http.get('GameLogic/FirstPhaseGamePlay?inGameId=' + gameId);
    };
    HttpService.prototype.SecondPhase = function (gameId) {
        return this.http.get('GameLogic/SecondPhase?inGameId=' + gameId);
    };
    HttpService.prototype.BlackJackDangerContinueRound = function (gameId) {
        return this.http.get('GameLogic/BlackJackDangerContinueRound?inGameId=' + gameId);
    };
    HttpService.prototype.AddOneMoreCardToHuman = function (gameId) {
        return this.http.get('GameLogic/AddOneMoreCardToHuman?inGameId=' + gameId);
    };
    HttpService.prototype.HumanRoundResult = function (gameId) {
        return this.http.get('PlayerLogic/HumanRoundResult?inGameId=' + gameId);
    };
    HttpService.prototype.UpdateGamePlayersForNewRound = function (gameId) {
        return this.http.get('PlayerLogic/UpdateGamePlayersForNewRound?inGameId=' + gameId);
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

/***/ "./src/app/viewmodels/AuthPlayerViewModel.ts":
/*!***************************************************!*\
  !*** ./src/app/viewmodels/AuthPlayerViewModel.ts ***!
  \***************************************************/
/*! exports provided: AuthPlayerViewModel */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AuthPlayerViewModel", function() { return AuthPlayerViewModel; });
var AuthPlayerViewModel = /** @class */ (function () {
    function AuthPlayerViewModel() {
    }
    return AuthPlayerViewModel;
}());



/***/ }),

/***/ "./src/app/viewmodels/GameViewModel.ts":
/*!*********************************************!*\
  !*** ./src/app/viewmodels/GameViewModel.ts ***!
  \*********************************************/
/*! exports provided: GameViewModel */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GameViewModel", function() { return GameViewModel; });
/* harmony import */ var _viewmodels_PlayerViewModel__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! ../viewmodels/PlayerViewModel */ "./src/app/viewmodels/PlayerViewModel.ts");
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


var GameViewModel = /** @class */ (function () {
    function GameViewModel() {
        this.GameId = void 0;
        this.Stage = void 0;
        this.Human = void 0;
        this.Dealer = void 0;
        this.Bots = void 0;
    }
    __decorate([
        Object(json_typescript_mapper__WEBPACK_IMPORTED_MODULE_1__["JsonProperty"])('Id'),
        __metadata("design:type", Number)
    ], GameViewModel.prototype, "GameId", void 0);
    __decorate([
        Object(json_typescript_mapper__WEBPACK_IMPORTED_MODULE_1__["JsonProperty"])('Stage'),
        __metadata("design:type", Number)
    ], GameViewModel.prototype, "Stage", void 0);
    __decorate([
        Object(json_typescript_mapper__WEBPACK_IMPORTED_MODULE_1__["JsonProperty"])({ clazz: _viewmodels_PlayerViewModel__WEBPACK_IMPORTED_MODULE_0__["PlayerViewModel"], name: 'Human' }),
        __metadata("design:type", _viewmodels_PlayerViewModel__WEBPACK_IMPORTED_MODULE_0__["PlayerViewModel"])
    ], GameViewModel.prototype, "Human", void 0);
    __decorate([
        Object(json_typescript_mapper__WEBPACK_IMPORTED_MODULE_1__["JsonProperty"])({ clazz: _viewmodels_PlayerViewModel__WEBPACK_IMPORTED_MODULE_0__["PlayerViewModel"], name: 'Dealer' }),
        __metadata("design:type", _viewmodels_PlayerViewModel__WEBPACK_IMPORTED_MODULE_0__["PlayerViewModel"])
    ], GameViewModel.prototype, "Dealer", void 0);
    __decorate([
        Object(json_typescript_mapper__WEBPACK_IMPORTED_MODULE_1__["JsonProperty"])({ clazz: _viewmodels_PlayerViewModel__WEBPACK_IMPORTED_MODULE_0__["PlayerViewModel"], name: 'Bots' }),
        __metadata("design:type", Array)
    ], GameViewModel.prototype, "Bots", void 0);
    return GameViewModel;
}());



/***/ }),

/***/ "./src/app/viewmodels/PlayerViewModel.ts":
/*!***********************************************!*\
  !*** ./src/app/viewmodels/PlayerViewModel.ts ***!
  \***********************************************/
/*! exports provided: PlayerViewModel */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "PlayerViewModel", function() { return PlayerViewModel; });
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

var PlayerViewModel = /** @class */ (function () {
    function PlayerViewModel() {
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
    ], PlayerViewModel.prototype, "GamePlayerId", void 0);
    __decorate([
        Object(json_typescript_mapper__WEBPACK_IMPORTED_MODULE_0__["JsonProperty"])('Name'),
        __metadata("design:type", String)
    ], PlayerViewModel.prototype, "Name", void 0);
    __decorate([
        Object(json_typescript_mapper__WEBPACK_IMPORTED_MODULE_0__["JsonProperty"])('Score'),
        __metadata("design:type", Number)
    ], PlayerViewModel.prototype, "Score", void 0);
    __decorate([
        Object(json_typescript_mapper__WEBPACK_IMPORTED_MODULE_0__["JsonProperty"])('Bet'),
        __metadata("design:type", Number)
    ], PlayerViewModel.prototype, "Bet", void 0);
    __decorate([
        Object(json_typescript_mapper__WEBPACK_IMPORTED_MODULE_0__["JsonProperty"])('RoundScore'),
        __metadata("design:type", Number)
    ], PlayerViewModel.prototype, "RoundScore", void 0);
    __decorate([
        Object(json_typescript_mapper__WEBPACK_IMPORTED_MODULE_0__["JsonProperty"])('Cards'),
        __metadata("design:type", Array)
    ], PlayerViewModel.prototype, "Cards", void 0);
    return PlayerViewModel;
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