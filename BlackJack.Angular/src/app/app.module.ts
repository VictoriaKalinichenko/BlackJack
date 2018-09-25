import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { APP_BASE_HREF } from '@angular/common';

import { AppComponent } from './app.component';
import { HomepageComponent } from './homepage/homepage.component';
import { StartpageComponent } from './authorized-user/startpage/startpage.component';
import { AuthorizedUserComponent } from './authorized-user/authorized-user.component';
import { GameComponent } from './authorized-user/game/game.component';
import { PlayerOutputComponent } from './authorized-user/game/player-output/player-output.component';
import { DealerOutputComponent } from './authorized-user/game/dealer-output/dealer-output.component';
import { ErrorPageComponent } from './error-page/error-page.component';

import { UserNameService } from './shared/services/user-name.service';
import { ErrorService } from './shared/services/error.service';
import { HttpService } from './shared/services/http.service';

const appRoutes: Routes = [
    { path: '', component: HomepageComponent },
    {
        path: 'user/:UserName',
        component: AuthorizedUserComponent,
        children: [
            { path: '', component: StartpageComponent },
            { path: 'game/:Id', component: GameComponent }
        ]
    },
    { path: 'error', component: ErrorPageComponent }
];

@NgModule({
  declarations: [
      AppComponent,
      HomepageComponent,
      StartpageComponent,
      AuthorizedUserComponent,
      GameComponent,
      PlayerOutputComponent,
      DealerOutputComponent,
      ErrorPageComponent
  ],
  imports: [
      BrowserModule,
      FormsModule,
      HttpClientModule,
      RouterModule.forRoot(
          appRoutes,
          { useHash: true }
      )
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
