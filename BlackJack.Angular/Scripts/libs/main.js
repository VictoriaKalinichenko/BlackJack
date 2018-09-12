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
/* harmony import */ var _startpage_startpage_component__WEBPACK_IMPORTED_MODULE_8__ = __webpack_require__(/*! ./startpage/startpage.component */ "./src/app/startpage/startpage.component.ts");
/* harmony import */ var _auth_user_auth_user_component__WEBPACK_IMPORTED_MODULE_9__ = __webpack_require__(/*! ./auth-user/auth-user.component */ "./src/app/auth-user/auth-user.component.ts");
/* harmony import */ var _services_data_service__WEBPACK_IMPORTED_MODULE_10__ = __webpack_require__(/*! ./services/data.service */ "./src/app/services/data.service.ts");
/* harmony import */ var _game_game_component__WEBPACK_IMPORTED_MODULE_11__ = __webpack_require__(/*! ./game/game.component */ "./src/app/game/game.component.ts");
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
            { path: '', component: _startpage_startpage_component__WEBPACK_IMPORTED_MODULE_8__["StartpageComponent"] },
            { path: 'game/:Id', component: _game_game_component__WEBPACK_IMPORTED_MODULE_11__["GameComponent"] }
        ]
    }
];
var AppModule = /** @class */ (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["NgModule"])({
            declarations: [
                _app_component__WEBPACK_IMPORTED_MODULE_6__["AppComponent"],
                _homepage_homepage_component__WEBPACK_IMPORTED_MODULE_7__["HomepageComponent"],
                _startpage_startpage_component__WEBPACK_IMPORTED_MODULE_8__["StartpageComponent"],
                _auth_user_auth_user_component__WEBPACK_IMPORTED_MODULE_9__["AuthUserComponent"],
                _game_game_component__WEBPACK_IMPORTED_MODULE_11__["GameComponent"]
            ],
            imports: [
                _angular_platform_browser__WEBPACK_IMPORTED_MODULE_0__["BrowserModule"],
                _angular_forms__WEBPACK_IMPORTED_MODULE_2__["FormsModule"],
                _angular_common_http__WEBPACK_IMPORTED_MODULE_3__["HttpClientModule"],
                _angular_router__WEBPACK_IMPORTED_MODULE_4__["RouterModule"].forRoot(appRoutes, { useHash: true })
            ],
            providers: [
                _services_data_service__WEBPACK_IMPORTED_MODULE_10__["DataService"],
                { provide: _angular_common__WEBPACK_IMPORTED_MODULE_5__["APP_BASE_HREF"], useValue: '/' }
            ],
            bootstrap: [
                _app_component__WEBPACK_IMPORTED_MODULE_6__["AppComponent"],
                _homepage_homepage_component__WEBPACK_IMPORTED_MODULE_7__["HomepageComponent"],
                _startpage_startpage_component__WEBPACK_IMPORTED_MODULE_8__["StartpageComponent"],
                _auth_user_auth_user_component__WEBPACK_IMPORTED_MODULE_9__["AuthUserComponent"],
                _game_game_component__WEBPACK_IMPORTED_MODULE_11__["GameComponent"]
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
    function AuthUserComponent(dataService, route) {
        this.dataService = dataService;
        this.route = route;
    }
    AuthUserComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.route.params.subscribe(function (params) {
            _this.UserName = params['UserName'];
        });
        this.dataService.SetUserName(this.UserName);
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

/***/ "./src/app/game/game.component.css":
/*!*****************************************!*\
  !*** ./src/app/game/game.component.css ***!
  \*****************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ""

/***/ }),

/***/ "./src/app/game/game.component.html":
/*!******************************************!*\
  !*** ./src/app/game/game.component.html ***!
  \******************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<p>\r\n    {{GameViewModel.GameId}}\r\n</p>\r\n<p>\r\n    {{GameViewModel.Stage}}\r\n</p>\r\n<p>\r\n    {{GameViewModel.Human.GamePlayerId}}\r\n</p>\r\n<p>\r\n    {{GameViewModel.Human.Name}}\r\n</p>\r\n<p>\r\n    {{GameViewModel.Human.Score}}\r\n</p>\r\n<p>\r\n    {{GameViewModel.Dealer.GamePlayerId}}\r\n</p>\r\n<p>\r\n    {{GameViewModel.Dealer.Name}}\r\n</p>\r\n<p>\r\n    {{GameViewModel.Dealer.Score}}\r\n</p>\r\n<div *ngFor=\"let bot of GameViewModel.Bots\">\r\n    <p>\r\n        {{bot.GamePlayerId}}\r\n    </p>\r\n    <p>\r\n        {{bot.Name}}\r\n    </p>\r\n    <p>\r\n        {{bot.Score}}\r\n    </p>\r\n</div>"

/***/ }),

/***/ "./src/app/game/game.component.ts":
/*!****************************************!*\
  !*** ./src/app/game/game.component.ts ***!
  \****************************************/
/*! exports provided: GameComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "GameComponent", function() { return GameComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _viewmodels_GameViewModel__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../viewmodels/GameViewModel */ "./src/app/viewmodels/GameViewModel.ts");
/* harmony import */ var json_typescript_mapper__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! json-typescript-mapper */ "./node_modules/json-typescript-mapper/index.js");
/* harmony import */ var json_typescript_mapper__WEBPACK_IMPORTED_MODULE_3___default = /*#__PURE__*/__webpack_require__.n(json_typescript_mapper__WEBPACK_IMPORTED_MODULE_3__);
/* harmony import */ var _services_data_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ../services/data.service */ "./src/app/services/data.service.ts");
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
                _this.GameViewModel = Object(json_typescript_mapper__WEBPACK_IMPORTED_MODULE_3__["deserialize"])(_viewmodels_GameViewModel__WEBPACK_IMPORTED_MODULE_2__["GameViewModel"], data);
            }, function (error) {
                console.log(error);
            });
        });
    };
    GameComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-game',
            template: __webpack_require__(/*! ./game.component.html */ "./src/app/game/game.component.html"),
            styles: [__webpack_require__(/*! ./game.component.css */ "./src/app/game/game.component.css")]
        }),
        __metadata("design:paramtypes", [_angular_router__WEBPACK_IMPORTED_MODULE_1__["ActivatedRoute"],
            _services_data_service__WEBPACK_IMPORTED_MODULE_4__["DataService"]])
    ], GameComponent);
    return GameComponent;
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
    function HomepageComponent(router) {
        this.router = router;
    }
    HomepageComponent.prototype.AuthUser = function () {
        this.router.navigate(['/user', this.UserName]);
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


var DataService = /** @class */ (function () {
    function DataService(http) {
        this.http = http;
    }
    DataService.prototype.SetUserName = function (userName) {
        this.UserName = userName;
    };
    DataService.prototype.GetUserName = function () {
        return this.UserName;
    };
    DataService.prototype.GetAuthorizedPlayer = function () {
        var body = { UserName: this.UserName };
        return this.http.post('http://localhost:55953/StartGame/GetAuthorizedPlayer', body);
    };
    DataService.prototype.CreateNewGame = function (playerId, amountOfBots) {
        var body = { PlayerId: playerId, AmountOfBots: amountOfBots };
        return this.http.post('http://localhost:55953/StartGame/CreateNewGame', body);
    };
    DataService.prototype.ResumeGame = function (playerId) {
        return this.http.get('http://localhost:55953/StartGame/ResumeGame?playerId=' + playerId);
    };
    DataService.prototype.GetGame = function (gameId) {
        return this.http.get('http://localhost:55953/StartGame/GetGame?gameId=' + gameId);
    };
    DataService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])({
            providedIn: 'root'
        }),
        __metadata("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_1__["HttpClient"]])
    ], DataService);
    return DataService;
}());



/***/ }),

/***/ "./src/app/startpage/startpage.component.css":
/*!***************************************************!*\
  !*** ./src/app/startpage/startpage.component.css ***!
  \***************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ""

/***/ }),

/***/ "./src/app/startpage/startpage.component.html":
/*!****************************************************!*\
  !*** ./src/app/startpage/startpage.component.html ***!
  \****************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<h2>Main page</h2>\r\n<hr />\r\n\r\n<div class=\"row\">\r\n    <div *ngIf=\"Player.ResumeGame\" class=\"col-lg-6 col-md-6 col-sm-6 col-xs-12 well\">\r\n        <h3>Game resuming</h3>\r\n        <p>You can resume your last game</p>\r\n        <div class=\"form-group\">\r\n            <a class=\"btn btn-primary\" (click)=\"ResumeGame()\">Resume game</a>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"col-lg-6 col-md-6 col-sm-6 col-xs-12 well\">\r\n        <h3>Start new game</h3>\r\n        <label class=\"control-label col-md-4\">Amount of bots:</label>\r\n        <div class=\"col-md-8\">\r\n            <input name=\"amountOfBots\" [(ngModel)]=\"AmountOfBots\" class=\"form-control\"\r\n                   type=\"number\" value=\"0\" min=\"0\" max=\"5\"\r\n                   #amountOfBots=\"ngModel\" pattern=\"[0-5]\" />\r\n            <div [hidden]=\"amountOfBots.valid\" class=\"alert alert-danger\">\r\n                Amount of bots must be more than or equals to 0 and less than or equals to 5.\r\n            </div>\r\n            <br />\r\n            <button [disabled]=\"amountOfBots.invalid\" class=\"btn btn-primary\" (click)=\"StartNewGame()\">Start new game</button>\r\n        </div>\r\n    </div>\r\n</div>"

/***/ }),

/***/ "./src/app/startpage/startpage.component.ts":
/*!**************************************************!*\
  !*** ./src/app/startpage/startpage.component.ts ***!
  \**************************************************/
/*! exports provided: StartpageComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "StartpageComponent", function() { return StartpageComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var _services_data_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../services/data.service */ "./src/app/services/data.service.ts");
/* harmony import */ var _viewmodels_AuthPlayerViewModel__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ../viewmodels/AuthPlayerViewModel */ "./src/app/viewmodels/AuthPlayerViewModel.ts");
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
    function StartpageComponent(dataService, router) {
        this.dataService = dataService;
        this.router = router;
        this.Player = new _viewmodels_AuthPlayerViewModel__WEBPACK_IMPORTED_MODULE_3__["AuthPlayerViewModel"]();
        this.AmountOfBots = 0;
    }
    StartpageComponent.prototype.ngOnInit = function () {
        this.UserName = this.dataService.GetUserName();
        this.AuthUser(this.UserName);
    };
    StartpageComponent.prototype.AuthUser = function (userName) {
        var _this = this;
        this.dataService.GetAuthorizedPlayer()
            .subscribe(function (data) {
            _this.Player.Name = data.Name;
            _this.Player.PlayerId = data.PlayerId;
            _this.Player.ResumeGame = data.ResumeGame;
        }, function (error) {
            console.log(error);
        });
    };
    StartpageComponent.prototype.StartNewGame = function () {
        var _this = this;
        this.dataService.CreateNewGame(this.Player.PlayerId, this.AmountOfBots)
            .subscribe(function (data) {
            _this.GameId = data.GameId;
            _this.router.navigate(['/user/' + _this.UserName + '/game/' + _this.GameId]);
        }, function (error) {
            console.log(error);
        });
    };
    StartpageComponent.prototype.ResumeGame = function () {
        var _this = this;
        this.dataService.ResumeGame(this.Player.PlayerId)
            .subscribe(function (data) {
            _this.GameId = data.GameId;
            _this.router.navigate(['/user/' + _this.UserName + '/game/' + _this.GameId]);
        }, function (error) {
            console.log(error);
        });
    };
    StartpageComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-startpage',
            template: __webpack_require__(/*! ./startpage.component.html */ "./src/app/startpage/startpage.component.html"),
            styles: [__webpack_require__(/*! ./startpage.component.css */ "./src/app/startpage/startpage.component.css")]
        }),
        __metadata("design:paramtypes", [_services_data_service__WEBPACK_IMPORTED_MODULE_2__["DataService"],
            _angular_router__WEBPACK_IMPORTED_MODULE_1__["Router"]])
    ], StartpageComponent);
    return StartpageComponent;
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