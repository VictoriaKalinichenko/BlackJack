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

@NgModule({
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
export class AppModule { }
