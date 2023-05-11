import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { TodoComponent } from './todo/todo.component';
import { ApiAuthorizationModule } from 'src/api-authorization/api-authorization.module';
import { AuthorizeInterceptor } from 'src/api-authorization/authorize.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ModalModule } from 'ngx-bootstrap/modal';
import { AppRoutingModule } from './app-routing.module';
import { TokenComponent } from './token/token.component';
import { TeamMatchConfigurationComponent } from './team-match-configuration/team-match-configuration.component';
import { TabsModule } from 'ngx-bootstrap';
import { PlayersConfigComponent } from './players-config/players-config.component';
import { StepsModule } from 'primeng/steps';
import { TeamInformationComponent } from './team-information/team-information.component';
import {CardModule } from 'primeng/card';
import { StartWizardComponent } from './start-wizard/start-wizard.component';
import {InputTextModule} from 'primeng/inputtext';
import {ButtonModule} from 'primeng/button';
import { PlayersComponent } from './players/players.component';
import { HostPlayersComponent } from './host-players/host-players.component';
import { GuestPlayersComponent } from './guest-players/guest-players.component';
import { DoublesComponent } from './doubles/doubles.component';
import {SelectButtonModule} from 'primeng/selectbutton';
import { ResumeComponent } from './resume/resume.component';
import { environment } from 'src/environments/environment';
import { API_BASE_URL } from './web-api-client';
import { OverviewComponent } from './overview/overview.component';
import {TableModule} from 'primeng/table';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    TodoComponent,
    TokenComponent,
    TeamMatchConfigurationComponent,
    PlayersConfigComponent,
    TeamInformationComponent,
    StartWizardComponent,
    PlayersComponent,
    HostPlayersComponent,
    GuestPlayersComponent,
    DoublesComponent,
    ResumeComponent,
    OverviewComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    FontAwesomeModule,
    HttpClientModule,
    FormsModule,
    ApiAuthorizationModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ModalModule.forRoot(),
    TabsModule.forRoot(),
    ReactiveFormsModule,
    StepsModule,
    CardModule,
    InputTextModule,
    ButtonModule,
    SelectButtonModule,
    TableModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthorizeInterceptor, multi: true },
    { provide: API_BASE_URL, useValue: environment.apiUrl }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
