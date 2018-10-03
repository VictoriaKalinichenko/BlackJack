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
import { UserNameService } from '../../shared/services/user-name-service/user-name.service';
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
        Component({
            selector: 'app-authorized-user',
            templateUrl: './authorized-user.component.html',
            styleUrls: ['./authorized-user.component.css']
        }),
        __metadata("design:paramtypes", [UserNameService,
            ActivatedRoute])
    ], AuthorizedUserComponent);
    return AuthorizedUserComponent;
}());
export { AuthorizedUserComponent };
//# sourceMappingURL=authorized-user.component.js.map