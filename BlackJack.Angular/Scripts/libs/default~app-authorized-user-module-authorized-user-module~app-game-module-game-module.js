(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["default~app-authorized-user-module-authorized-user-module~app-game-module-game-module"],{

/***/ "./src/app/authorized-user-module/authorized-user-routing.module.ts":
/*!**************************************************************************!*\
  !*** ./src/app/authorized-user-module/authorized-user-routing.module.ts ***!
  \**************************************************************************/
/*! exports provided: AuthorizedUserRoutingModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AuthorizedUserRoutingModule", function() { return AuthorizedUserRoutingModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var app_authorized_user_module_authorized_user_authorized_user_component__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! app/authorized-user-module/authorized-user/authorized-user.component */ "./src/app/authorized-user-module/authorized-user/authorized-user.component.ts");
/* harmony import */ var app_authorized_user_module_start_page_start_page_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! app/authorized-user-module/start-page/start-page.component */ "./src/app/authorized-user-module/start-page/start-page.component.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};




var routes = [
    {
        path: '',
        component: app_authorized_user_module_authorized_user_authorized_user_component__WEBPACK_IMPORTED_MODULE_2__["AuthorizedUserComponent"],
        children: [
            {
                path: '',
                component: app_authorized_user_module_start_page_start_page_component__WEBPACK_IMPORTED_MODULE_3__["StartPageComponent"]
            },
            {
                path: 'game/:Id',
                loadChildren: 'app/game-module/game.module#GameModule'
            }
        ]
    }
];
var AuthorizedUserRoutingModule = /** @class */ (function () {
    function AuthorizedUserRoutingModule() {
    }
    AuthorizedUserRoutingModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"])({
            imports: [
                _angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"].forChild(routes)
            ],
            exports: [
                _angular_router__WEBPACK_IMPORTED_MODULE_1__["RouterModule"]
            ]
        })
    ], AuthorizedUserRoutingModule);
    return AuthorizedUserRoutingModule;
}());



/***/ }),

/***/ "./src/app/authorized-user-module/authorized-user.module.ts":
/*!******************************************************************!*\
  !*** ./src/app/authorized-user-module/authorized-user.module.ts ***!
  \******************************************************************/
/*! exports provided: AuthorizedUserModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AuthorizedUserModule", function() { return AuthorizedUserModule; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var app_shared_modules_shared_module__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! app/shared/modules/shared.module */ "./src/app/shared/modules/shared.module.ts");
/* harmony import */ var app_authorized_user_module_authorized_user_routing_module__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! app/authorized-user-module/authorized-user-routing.module */ "./src/app/authorized-user-module/authorized-user-routing.module.ts");
/* harmony import */ var app_authorized_user_module_authorized_user_authorized_user_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! app/authorized-user-module/authorized-user/authorized-user.component */ "./src/app/authorized-user-module/authorized-user/authorized-user.component.ts");
/* harmony import */ var app_authorized_user_module_start_page_start_page_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! app/authorized-user-module/start-page/start-page.component */ "./src/app/authorized-user-module/start-page/start-page.component.ts");
/* harmony import */ var app_shared_services_user_name_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! app/shared/services/user-name.service */ "./src/app/shared/services/user-name.service.ts");
/* harmony import */ var app_shared_services_start_service__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! app/shared/services/start.service */ "./src/app/shared/services/start.service.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};







var AuthorizedUserModule = /** @class */ (function () {
    function AuthorizedUserModule() {
    }
    AuthorizedUserModule = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["NgModule"])({
            declarations: [
                app_authorized_user_module_authorized_user_authorized_user_component__WEBPACK_IMPORTED_MODULE_3__["AuthorizedUserComponent"],
                app_authorized_user_module_start_page_start_page_component__WEBPACK_IMPORTED_MODULE_4__["StartPageComponent"]
            ],
            imports: [
                app_authorized_user_module_authorized_user_routing_module__WEBPACK_IMPORTED_MODULE_2__["AuthorizedUserRoutingModule"],
                app_shared_modules_shared_module__WEBPACK_IMPORTED_MODULE_1__["SharedModule"]
            ],
            providers: [
                app_shared_services_user_name_service__WEBPACK_IMPORTED_MODULE_5__["UserNameService"],
                app_shared_services_start_service__WEBPACK_IMPORTED_MODULE_6__["StartService"]
            ]
        })
    ], AuthorizedUserModule);
    return AuthorizedUserModule;
}());



/***/ }),

/***/ "./src/app/authorized-user-module/authorized-user/authorized-user.component.html":
/*!***************************************************************************************!*\
  !*** ./src/app/authorized-user-module/authorized-user/authorized-user.component.html ***!
  \***************************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<nav class=\"navbar navbar-default\">\r\n    <div class=\"container\">\r\n        <div class=\"navbar-header\">\r\n            <button type=\"button\" class=\"navbar-toggle\" data-toggle=\"collapse\" data-target=\"#myNavbar\">\r\n                <span class=\"icon-bar\"></span>\r\n                <span class=\"icon-bar\"></span>\r\n                <span class=\"icon-bar\"></span>\r\n            </button>\r\n            <a class=\"navbar-brand\" [routerLink]=\"['/user', userName]\">\r\n                Home\r\n            </a>\r\n        </div>\r\n\r\n        <div class=\"collapse navbar-collapse\" id=\"myNavbar\">\r\n            <ul class=\"nav navbar-nav\">\r\n                <li class=\"navbar-text\">\r\n                    <b>Log in as: {{userName}}</b>\r\n                </li>\r\n                <li class=\"nav-item\">\r\n                    <a routerLink=\"/\">\r\n                        Log out\r\n                    </a>\r\n                </li>\r\n            </ul>\r\n        </div>\r\n    </div>\r\n</nav>\r\n<router-outlet></router-outlet>  "

/***/ }),

/***/ "./src/app/authorized-user-module/authorized-user/authorized-user.component.ts":
/*!*************************************************************************************!*\
  !*** ./src/app/authorized-user-module/authorized-user/authorized-user.component.ts ***!
  \*************************************************************************************/
/*! exports provided: AuthorizedUserComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AuthorizedUserComponent", function() { return AuthorizedUserComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var app_shared_services_user_name_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! app/shared/services/user-name.service */ "./src/app/shared/services/user-name.service.ts");
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
    function AuthorizedUserComponent(userNameService, route) {
        this.userNameService = userNameService;
        this.route = route;
    }
    AuthorizedUserComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.route.params.subscribe(function (params) {
            _this.userName = params['userName'];
        });
        this.userNameService.setUserName(this.userName);
    };
    AuthorizedUserComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-authorized-user',
            template: __webpack_require__(/*! ./authorized-user.component.html */ "./src/app/authorized-user-module/authorized-user/authorized-user.component.html")
        }),
        __metadata("design:paramtypes", [app_shared_services_user_name_service__WEBPACK_IMPORTED_MODULE_2__["UserNameService"],
            _angular_router__WEBPACK_IMPORTED_MODULE_1__["ActivatedRoute"]])
    ], AuthorizedUserComponent);
    return AuthorizedUserComponent;
}());



/***/ }),

/***/ "./src/app/authorized-user-module/start-page/start-page.component.html":
/*!*****************************************************************************!*\
  !*** ./src/app/authorized-user-module/start-page/start-page.component.html ***!
  \*****************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<h2>Main page</h2>\r\n<hr />\r\n\r\n<div class=\"row row-flex\">\r\n    <div *ngIf=\"player.resumeGame\" class=\"col-lg-6 col-md-6 col-sm-6 col-xs-12 well\">\r\n        <h3>Game resuming</h3>\r\n        <p>You can resume your last game</p>\r\n        <div class=\"form-group\">\r\n            <a class=\"btn btn-primary\" (click)=\"resumeGame()\">Resume game</a>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"col-lg-6 col-md-6 col-sm-6 col-xs-12 well\">\r\n        <h3>Start new game</h3>\r\n        <label class=\"control-label col-md-4\">Amount of bots:</label>\r\n        <div class=\"col-md-8\">\r\n            <input name=\"amount\" [(ngModel)]=\"amountOfBots\" class=\"form-control\"\r\n                   type=\"number\" value=\"0\" min=\"0\" max=\"5\"\r\n                   #amount=\"ngModel\" pattern=\"[0-5]\" />\r\n            <div [hidden]=\"amount.valid\" class=\"alert alert-danger\">\r\n                Amount of bots must be more than or equals to 0 and less than or equals to 5.\r\n            </div>\r\n            <br />\r\n            <button [disabled]=\"amount.invalid\" class=\"btn btn-primary\" (click)=\"startNewGame()\">Start new game</button>\r\n        </div>\r\n    </div>\r\n</div>"

/***/ }),

/***/ "./src/app/authorized-user-module/start-page/start-page.component.ts":
/*!***************************************************************************!*\
  !*** ./src/app/authorized-user-module/start-page/start-page.component.ts ***!
  \***************************************************************************/
/*! exports provided: StartPageComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "StartPageComponent", function() { return StartPageComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm5/router.js");
/* harmony import */ var app_shared_services_user_name_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! app/shared/services/user-name.service */ "./src/app/shared/services/user-name.service.ts");
/* harmony import */ var app_shared_services_start_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! app/shared/services/start.service */ "./src/app/shared/services/start.service.ts");
/* harmony import */ var app_shared_view_models_authorize_player_start_view__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! app/shared/view-models/authorize-player-start-view */ "./src/app/shared/view-models/authorize-player-start-view.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};





var StartPageComponent = /** @class */ (function () {
    function StartPageComponent(userNameService, startService, router) {
        this.userNameService = userNameService;
        this.startService = startService;
        this.router = router;
        this.player = new app_shared_view_models_authorize_player_start_view__WEBPACK_IMPORTED_MODULE_4__["AuthorizePlayerStartView"]();
        this.amountOfBots = 0;
    }
    StartPageComponent.prototype.ngOnInit = function () {
        this.userName = this.userNameService.getUserName();
        this.authUser(this.userName);
    };
    StartPageComponent.prototype.authUser = function (userName) {
        var _this = this;
        this.startService.getAuthorizedPlayer(this.userName)
            .subscribe(function (data) {
            _this.player.name = data["Name"];
            _this.player.playerId = data["PlayerId"];
            _this.player.resumeGame = data["ResumeGame"];
        });
    };
    StartPageComponent.prototype.startNewGame = function () {
        var _this = this;
        this.startService.createGame(this.player.playerId, this.amountOfBots)
            .subscribe(function (data) {
            _this.gameId = data["GameId"];
            _this.router.navigate(['/user/' + _this.userName + '/game/' + _this.gameId]);
        });
    };
    StartPageComponent.prototype.resumeGame = function () {
        var _this = this;
        this.startService.resumeGame(this.player.playerId)
            .subscribe(function (data) {
            _this.gameId = data["GameId"];
            _this.router.navigate(['/user/' + _this.userName + '/game/' + _this.gameId]);
        });
    };
    StartPageComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-start-page',
            template: __webpack_require__(/*! ./start-page.component.html */ "./src/app/authorized-user-module/start-page/start-page.component.html")
        }),
        __metadata("design:paramtypes", [app_shared_services_user_name_service__WEBPACK_IMPORTED_MODULE_2__["UserNameService"],
            app_shared_services_start_service__WEBPACK_IMPORTED_MODULE_3__["StartService"],
            _angular_router__WEBPACK_IMPORTED_MODULE_1__["Router"]])
    ], StartPageComponent);
    return StartPageComponent;
}());



/***/ }),

/***/ "./src/app/shared/services/start.service.ts":
/*!**************************************************!*\
  !*** ./src/app/shared/services/start.service.ts ***!
  \**************************************************/
/*! exports provided: StartService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "StartService", function() { return StartService; });
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/common/http */ "./node_modules/@angular/common/fesm5/http.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var app_authorized_user_module_authorized_user_module__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! app/authorized-user-module/authorized-user.module */ "./src/app/authorized-user-module/authorized-user.module.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (undefined && undefined.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};



var StartService = /** @class */ (function () {
    function StartService(httpClient) {
        this.httpClient = httpClient;
    }
    StartService.prototype.getAuthorizedPlayer = function (userName) {
        var options = userName ?
            { params: new _angular_common_http__WEBPACK_IMPORTED_MODULE_0__["HttpParams"]().set('userName', userName.toString()) } : {};
        return this.httpClient.get('Start/AuthorizePlayer', options);
    };
    StartService.prototype.createGame = function (playerId, amountOfBots) {
        var body = { PlayerId: playerId, AmountOfBots: amountOfBots };
        return this.httpClient.post('Start/CreateGame', body);
    };
    StartService.prototype.resumeGame = function (playerId) {
        var options = playerId ?
            { params: new _angular_common_http__WEBPACK_IMPORTED_MODULE_0__["HttpParams"]().set('playerId', playerId.toString()) } : {};
        return this.httpClient.get('Start/ResumeGame', options);
    };
    StartService.prototype.getGame = function (gameId) {
        var options = gameId ?
            { params: new _angular_common_http__WEBPACK_IMPORTED_MODULE_0__["HttpParams"]().set('gameId', gameId.toString()) } : {};
        return this.httpClient.get('Start/Initialize', options);
    };
    StartService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Injectable"])({
            providedIn: app_authorized_user_module_authorized_user_module__WEBPACK_IMPORTED_MODULE_2__["AuthorizedUserModule"]
        }),
        __metadata("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_0__["HttpClient"]])
    ], StartService);
    return StartService;
}());



/***/ }),

/***/ "./src/app/shared/services/user-name.service.ts":
/*!******************************************************!*\
  !*** ./src/app/shared/services/user-name.service.ts ***!
  \******************************************************/
/*! exports provided: UserNameService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "UserNameService", function() { return UserNameService; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm5/core.js");
/* harmony import */ var app_authorized_user_module_authorized_user_module__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! app/authorized-user-module/authorized-user.module */ "./src/app/authorized-user-module/authorized-user.module.ts");
var __decorate = (undefined && undefined.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};


var UserNameService = /** @class */ (function () {
    function UserNameService() {
    }
    UserNameService.prototype.setUserName = function (userName) {
        this.userName = userName;
    };
    UserNameService.prototype.getUserName = function () {
        return this.userName;
    };
    UserNameService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Injectable"])({
            providedIn: app_authorized_user_module_authorized_user_module__WEBPACK_IMPORTED_MODULE_1__["AuthorizedUserModule"]
        })
    ], UserNameService);
    return UserNameService;
}());



/***/ }),

/***/ "./src/app/shared/view-models/authorize-player-start-view.ts":
/*!*******************************************************************!*\
  !*** ./src/app/shared/view-models/authorize-player-start-view.ts ***!
  \*******************************************************************/
/*! exports provided: AuthorizePlayerStartView */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AuthorizePlayerStartView", function() { return AuthorizePlayerStartView; });
var AuthorizePlayerStartView = /** @class */ (function () {
    function AuthorizePlayerStartView() {
    }
    return AuthorizePlayerStartView;
}());



/***/ })

}]);
//# sourceMappingURL=default~app-authorized-user-module-authorized-user-module~app-game-module-game-module.js.map