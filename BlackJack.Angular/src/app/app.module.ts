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
import { BetInputComponent } from './bet-input/bet-input.component';
import { TakeCardComponent } from './take-card/take-card.component';
import { EndRoundComponent } from './end-round/end-round.component';
import { ErrorPageComponent } from './error-page/error-page.component';
import { BlackjackDangerChoiceComponent } from './blackjack-danger-choice/blackjack-danger-choice.component';

const appRoutes: Routes = [
    { path: '', component: HomepageComponent },
    {
        path: 'user/:UserName',
        component: AuthUserComponent,
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
      AuthUserComponent,
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
        { provide: APP_BASE_HREF, useValue: '/' }
    ],
    bootstrap: [
        AppComponent,
        HomepageComponent,
        StartpageComponent,
        AuthUserComponent,
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
