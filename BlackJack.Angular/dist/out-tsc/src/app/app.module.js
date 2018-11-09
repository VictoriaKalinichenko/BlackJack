var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { SharedModule } from 'app/shared/modules/shared.module';
import { AppRoutingModule } from 'app/app-routing.module';
import { APP_BASE_HREF } from '@angular/common';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppComponent } from 'app/app.component';
import { HomePageComponent } from 'app/home-page/home-page.component';
import { ErrorPageComponent } from 'app/error-page/error-page.component';
import { CreateGameComponent } from 'app/create-game/create-game.component';
import { ErrorService } from 'app/shared/services/error.service';
import { NewGameService } from 'app/shared/services/new-game.service';
import { StartService } from 'app/shared/services/start.service';
import { RequestInterceptor } from 'app/shared/interceptors/request-interceptor';
var AppModule = /** @class */ (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        NgModule({
            declarations: [
                AppComponent,
                HomePageComponent,
                ErrorPageComponent,
                CreateGameComponent
            ],
            imports: [
                BrowserModule,
                SharedModule,
                AppRoutingModule
            ],
            providers: [
                ErrorService,
                StartService,
                NewGameService,
                {
                    provide: APP_BASE_HREF,
                    useValue: '/'
                },
                {
                    provide: HTTP_INTERCEPTORS,
                    useClass: RequestInterceptor,
                    multi: true
                }
            ],
            bootstrap: [
                AppComponent
            ]
        })
    ], AppModule);
    return AppModule;
}());
export { AppModule };
//# sourceMappingURL=app.module.js.map