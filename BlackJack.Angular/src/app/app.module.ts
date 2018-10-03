import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { SharedModule } from './shared/modules/shared.module';
import { AppRoutingModule } from './app-routing.module';
import { APP_BASE_HREF } from '@angular/common';

import { AppComponent } from './app.component';
import { HomepageComponent } from './homepage/homepage.component';
import { ErrorPageComponent } from './error-page/error-page.component';

import { UserNameService } from './shared/services/user-name-service/user-name.service';
import { ErrorService } from './shared/services/error-service/error.service';
import { HttpService } from './shared/services/http-service/http.service';

@NgModule({
    declarations: [
        AppComponent,
        HomepageComponent,
        ErrorPageComponent
    ],
    imports: [
        BrowserModule,
        SharedModule,
        AppRoutingModule
    ],
    providers: [
        UserNameService,
        ErrorService,
        HttpService,
        { provide: APP_BASE_HREF, useValue: '/' }
    ],
    bootstrap: [
        AppComponent
    ]
})
export class AppModule { }
