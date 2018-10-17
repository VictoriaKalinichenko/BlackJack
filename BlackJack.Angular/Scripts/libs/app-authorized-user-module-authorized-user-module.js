(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["app-authorized-user-module-authorized-user-module"],{

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

module.exports = "<nav class=\"navbar navbar-default\">\r\n    <div class=\"container\">\r\n        <div class=\"navbar-header\">\r\n            <button type=\"button\" class=\"navbar-toggle\" data-toggle=\"collapse\" data-target=\"#myNavbar\">\r\n                <span class=\"icon-bar\"></span>\r\n                <span class=\"icon-bar\"></span>\r\n                <span class=\"icon-bar\"></span>\r\n            </button>\r\n            <a class=\"navbar-brand\" [routerLink]=\"['/user', UserName]\">\r\n                Home\r\n            </a>\r\n        </div>\r\n\r\n        <div class=\"collapse navbar-collapse\" id=\"myNavbar\">\r\n            <ul class=\"nav navbar-nav\">\r\n                <li class=\"navbar-text\">\r\n                    <b>Log in as: {{UserName}}</b>\r\n                </li>\r\n                <li class=\"nav-item\">\r\n                    <a routerLink=\"/\">\r\n                        Log out\r\n                    </a>\r\n                </li>\r\n            </ul>\r\n        </div>\r\n    </div>\r\n</nav>\r\n<router-outlet></router-outlet>  "

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
    function AuthorizedUserComponent(_userNameService, _route) {
        this._userNameService = _userNameService;
        this._route = _route;
    }
    AuthorizedUserComponent.prototype.ngOnInit = function () {
        var _this = this;
        this._route.params.subscribe(function (params) {
            _this.UserName = params['UserName'];
        });
        this._userNameService.SetUserName(this.UserName);
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

/***/ "./src/app/authorized-user-module/start-page/start-page.component.css":
/*!****************************************************************************!*\
  !*** ./src/app/authorized-user-module/start-page/start-page.component.css ***!
  \****************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = ".row-flex {\r\n    display: flex;\r\n    flex-flow: row wrap;\r\n}\r\n"

/***/ }),

/***/ "./src/app/authorized-user-module/start-page/start-page.component.html":
/*!*****************************************************************************!*\
  !*** ./src/app/authorized-user-module/start-page/start-page.component.html ***!
  \*****************************************************************************/
/*! no static exports found */
/***/ (function(module, exports) {

module.exports = "<h2>Main page</h2>\r\n<hr />\r\n\r\n<div class=\"row row-flex\">\r\n    <div *ngIf=\"Player.ResumeGame\" class=\"col-lg-6 col-md-6 col-sm-6 col-xs-12 well\">\r\n        <h3>Game resuming</h3>\r\n        <p>You can resume your last game</p>\r\n        <div class=\"form-group\">\r\n            <a class=\"btn btn-primary\" (click)=\"ResumeGame()\">Resume game</a>\r\n        </div>\r\n    </div>\r\n\r\n    <div class=\"col-lg-6 col-md-6 col-sm-6 col-xs-12 well\">\r\n        <h3>Start new game</h3>\r\n        <label class=\"control-label col-md-4\">Amount of bots:</label>\r\n        <div class=\"col-md-8\">\r\n            <input name=\"amountOfBots\" [(ngModel)]=\"AmountOfBots\" class=\"form-control\"\r\n                   type=\"number\" value=\"0\" min=\"0\" max=\"5\"\r\n                   #amountOfBots=\"ngModel\" pattern=\"[0-5]\" />\r\n            <div [hidden]=\"amountOfBots.valid\" class=\"alert alert-danger\">\r\n                Amount of bots must be more than or equals to 0 and less than or equals to 5.\r\n            </div>\r\n            <br />\r\n            <button [disabled]=\"amountOfBots.invalid\" class=\"btn btn-primary\" (click)=\"StartNewGame()\">Start new game</button>\r\n        </div>\r\n    </div>\r\n</div>"

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
/* harmony import */ var app_shared_services_http_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! app/shared/services/http.service */ "./src/app/shared/services/http.service.ts");
/* harmony import */ var app_shared_view_models_authorize_player_view_model__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! app/shared/view-models/authorize-player-view-model */ "./src/app/shared/view-models/authorize-player-view-model.ts");
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
    function StartPageComponent(_userNameService, _httpService, _router) {
        this._userNameService = _userNameService;
        this._httpService = _httpService;
        this._router = _router;
        this.Player = new app_shared_view_models_authorize_player_view_model__WEBPACK_IMPORTED_MODULE_4__["AuthorizePlayerViewModel"]();
        this.AmountOfBots = 0;
    }
    StartPageComponent.prototype.ngOnInit = function () {
        this.UserName = this._userNameService.GetUserName();
        this.AuthUser(this.UserName);
    };
    StartPageComponent.prototype.AuthUser = function (userName) {
        var _this = this;
        this._httpService.GetAuthorizedPlayer(this.UserName)
            .subscribe(function (data) {
            _this.Player.Name = data.Name;
            _this.Player.PlayerId = data.PlayerId;
            _this.Player.ResumeGame = data.ResumeGame;
        });
    };
    StartPageComponent.prototype.StartNewGame = function () {
        var _this = this;
        this._httpService.CreateGame(this.Player.PlayerId, this.AmountOfBots)
            .subscribe(function (data) {
            _this.GameId = data["GameId"];
            _this._router.navigate(['/user/' + _this.UserName + '/game/' + _this.GameId]);
        });
    };
    StartPageComponent.prototype.ResumeGame = function () {
        var _this = this;
        this._httpService.ResumeGame(this.Player.PlayerId)
            .subscribe(function (data) {
            _this.GameId = data["GameId"];
            _this._router.navigate(['/user/' + _this.UserName + '/game/' + _this.GameId]);
        });
    };
    StartPageComponent = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_0__["Component"])({
            selector: 'app-start-page',
            template: __webpack_require__(/*! ./start-page.component.html */ "./src/app/authorized-user-module/start-page/start-page.component.html"),
            styles: [__webpack_require__(/*! ./start-page.component.css */ "./src/app/authorized-user-module/start-page/start-page.component.css")]
        }),
        __metadata("design:paramtypes", [app_shared_services_user_name_service__WEBPACK_IMPORTED_MODULE_2__["UserNameService"],
            app_shared_services_http_service__WEBPACK_IMPORTED_MODULE_3__["HttpService"],
            _angular_router__WEBPACK_IMPORTED_MODULE_1__["Router"]])
    ], StartPageComponent);
    return StartPageComponent;
}());



/***/ }),

/***/ "./src/app/shared/view-models/authorize-player-view-model.ts":
/*!*******************************************************************!*\
  !*** ./src/app/shared/view-models/authorize-player-view-model.ts ***!
  \*******************************************************************/
/*! exports provided: AuthorizePlayerViewModel */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AuthorizePlayerViewModel", function() { return AuthorizePlayerViewModel; });
var AuthorizePlayerViewModel = /** @class */ (function () {
    function AuthorizePlayerViewModel() {
    }
    return AuthorizePlayerViewModel;
}());



/***/ })

}]);
//# sourceMappingURL=app-authorized-user-module-authorized-user-module.js.map