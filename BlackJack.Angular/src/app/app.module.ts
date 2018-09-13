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
import { PlayerOutputComponent } from './player-output/player-output.component';
import { DealerOutputComponent } from './dealer-output/dealer-output.component';

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
      PlayerOutputComponent,
      DealerOutputComponent
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
        GameComponent,
        PlayerOutputComponent
    ]
})
export class AppModule { }
