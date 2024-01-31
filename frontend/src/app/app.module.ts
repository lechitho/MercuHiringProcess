import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {TuiRootModule, TuiLinkModule} from '@taiga-ui/core';
import {TuiCardModule} from '@taiga-ui/experimental';
import {  HttpClientModule  } from '@angular/common/http';

import { NavbarComponent } from './Pages/Partials/navbar/navbar.component';
import { LoaderComponent } from './Pages/Partials/loader/loader.component';
import { CandidateListComponent } from './Pages/candidate/get/candidate-list.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    LoaderComponent,
    CandidateListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    TuiRootModule,
    TuiLinkModule,
    TuiCardModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
