var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { APP_BASE_HREF } from '@angular/common';
import { AppComponent } from './app.component';
import { HomepageComponent } from './homepage/homepage.component';
import { StartpageComponent } from './startpage/startpage.component';
import { DataService } from './services/data.service';
var appRoutes = [
    { path: 'startpage', component: StartpageComponent },
    { path: 'homepage', component: HomepageComponent }
];
var AppModule = /** @class */ (function () {
    function AppModule() {
    }
    AppModule = __decorate([
        NgModule({
            declarations: [
                AppComponent,
                HomepageComponent,
                StartpageComponent
            ],
            imports: [
                BrowserModule,
                FormsModule,
                HttpClientModule,
                RouterModule.forRoot(appRoutes, { enableTracing: true })
            ],
            providers: [
                DataService,
                { provide: APP_BASE_HREF, useValue: '/' }
            ],
            bootstrap: [
                AppComponent,
                HomepageComponent,
                StartpageComponent
            ]
        })
    ], AppModule);
    return AppModule;
}());
export { AppModule };
//# sourceMappingURL=app.module.js.map