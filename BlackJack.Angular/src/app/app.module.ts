import { APP_BASE_HREF } from '@angular/common';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from 'app/app-routing.module';
import { AppComponent } from 'app/app.component';
import { CreateGameComponent } from 'app/create-game/create-game.component';
import { ErrorPageComponent } from 'app/error-page/error-page.component';
import { HomePageComponent } from 'app/home-page/home-page.component';
import { RequestInterceptor } from 'app/shared/interceptors/request-interceptor';
import { SharedModule } from 'app/shared/modules/shared.module';
import { StartService } from 'app/shared/services/start.service';

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
        StartService,
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
