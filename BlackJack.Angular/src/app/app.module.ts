import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';
import { APP_BASE_HREF } from '@angular/common';

import { AppComponent } from './app.component';
import { HomepageComponent } from './homepage/homepage.component';
import { StartpageComponent } from './startpage/startpage.component';
import { AuthUserComponent } from './auth-user/auth-user.component';
import { DataService } from './services/data.service';
import { GameComponent } from './game/game.component';
import { PlayerRoundStartComponent } from './player-round-start/player-round-start.component';

const appRoutes: Routes = [
    { path: '', component: HomepageComponent },
    {
        path: 'user/:UserName',
        component: AuthUserComponent,
        children: [
            { path: '', component: StartpageComponent },
            { path: 'game/:Id', component: GameComponent }
        ]
    }
];

@NgModule({
  declarations: [
      AppComponent,
      HomepageComponent,
      StartpageComponent,
      AuthUserComponent,
      GameComponent,
      PlayerRoundStartComponent
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
        DataService,
        { provide: APP_BASE_HREF, useValue: '/' }
    ],
    bootstrap: [
        AppComponent,
        HomepageComponent,
        StartpageComponent,
        AuthUserComponent,
        GameComponent
    ]
})
export class AppModule { }
