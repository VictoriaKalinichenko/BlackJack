import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule, Routes } from '@angular/router';

import { AppComponent } from './app.component';
import { HomepageComponent } from './homepage/homepage.component';
import { StartpageComponent } from './startpage/startpage.component';
import { DataService } from './services/data.service';

const appRoutes: Routes = [
    { path: 'startpage', component: StartpageComponent },
    { path: 'homepage', component: HomepageComponent }
];

@NgModule({
  declarations: [
      AppComponent,
      HomepageComponent,
      StartpageComponent
  ],
  imports: [
      BrowserModule,
      FormsModule,
      HttpClientModule,
      RouterModule.forRoot(
          appRoutes,
          { enableTracing: true }
      )
  ],
    providers: [
        DataService
    ],
    bootstrap: [
        AppComponent,
        HomepageComponent,
        StartpageComponent
    ]
})
export class AppModule { }
