(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["common"],{

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
    StartService.prototype.initializeRound = function (gameId) {
        var options = gameId ?
            { params: new _angular_common_http__WEBPACK_IMPORTED_MODULE_0__["HttpParams"]().set('gameId', gameId.toString()) } : {};
        return this.httpClient.get('Start/Initialize', options);
    };
    StartService = __decorate([
        Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Injectable"])(),
        __metadata("design:paramtypes", [_angular_common_http__WEBPACK_IMPORTED_MODULE_0__["HttpClient"]])
    ], StartService);
    return StartService;
}());



/***/ })

}]);
//# sourceMappingURL=common.js.map