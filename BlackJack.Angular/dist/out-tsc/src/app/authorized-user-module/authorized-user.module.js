var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { NgModule } from '@angular/core';
import { SharedModule } from 'app/shared/modules/shared.module';
import { AuthorizedUserRoutingModule } from 'app/authorized-user-module/authorized-user-routing.module';
import { AuthorizedUserComponent } from 'app/authorized-user-module/authorized-user/authorized-user.component';
import { StartPageComponent } from 'app/authorized-user-module/start-page/start-page.component';
import { UserNameService } from 'app/shared/services/user-name.service';
import { StartService } from 'app/shared/services/start.service';
var AuthorizedUserModule = /** @class */ (function () {
    function AuthorizedUserModule() {
    }
    AuthorizedUserModule = __decorate([
        NgModule({
            declarations: [
                AuthorizedUserComponent,
                StartPageComponent
            ],
            imports: [
                AuthorizedUserRoutingModule,
                SharedModule
            ],
            providers: [
                UserNameService,
                StartService
            ]
        })
    ], AuthorizedUserModule);
    return AuthorizedUserModule;
}());
export { AuthorizedUserModule };
//# sourceMappingURL=authorized-user.module.js.map