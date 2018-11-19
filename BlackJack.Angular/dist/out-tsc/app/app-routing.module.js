var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CreateGameComponent } from 'app/create-game/create-game.component';
import { ErrorPageComponent } from 'app/error-page/error-page.component';
import { HomePageComponent } from 'app/home-page/home-page.component';
var appRoutes = [
    {
        path: '',
        component: HomePageComponent
    },
    {
        path: 'create/:userName',
        component: CreateGameComponent
    },
    {
        path: 'game/:userName/:gameId/:isNewGame',
        loadChildren: 'app/game-module/game.module#GameModule'
    },
    {
        path: 'error/:message',
        component: ErrorPageComponent
    }
];
var AppRoutingModule = /** @class */ (function () {
    function AppRoutingModule() {
    }
    AppRoutingModule = __decorate([
        NgModule({
            imports: [
                RouterModule.forRoot(appRoutes, { useHash: true })
            ],
            exports: [
                RouterModule
            ]
        })
    ], AppRoutingModule);
    return AppRoutingModule;
}());
export { AppRoutingModule };
//# sourceMappingURL=app-routing.module.js.map