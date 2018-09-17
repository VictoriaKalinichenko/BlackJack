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
import { BetInputComponent } from './authorized-user/game/bet-input/bet-input.component';
import { TakeCardComponent } from './authorized-user/game/take-card/take-card.component';
import { EndRoundComponent } from './authorized-user/game/end-round/end-round.component';
import { ErrorPageComponent } from './error-page/error-page.component';
import { BlackjackDangerChoiceComponent } from './authorized-user/game/blackjack-danger-choice/blackjack-danger-choice.component';

import { DataService } from './services/data.service';
import { ErrorService } from './services/error.service';
import { HttpService } from './services/http.service';

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
      BetInputComponent,
      TakeCardComponent,
      EndRoundComponent,
      ErrorPageComponent,
      BlackjackDangerChoiceComponent
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
        ErrorService,
        HttpService,
        { provide: APP_BASE_HREF, useValue: '/' }
    ],
    bootstrap: [
        AppComponent,
        HomepageComponent,
        StartpageComponent,
        AuthorizedUserComponent,
        GameComponent,
        PlayerOutputComponent,
        DealerOutputComponent,
        BetInputComponent,
        TakeCardComponent,
        EndRoundComponent,
        ErrorPageComponent,
        BlackjackDangerChoiceComponent
    ]
})
export class AppModule { }
