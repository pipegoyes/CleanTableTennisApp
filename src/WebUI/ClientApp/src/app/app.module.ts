import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ModalModule } from 'ngx-bootstrap/modal';
import { AppRoutingModule } from './app-routing.module';
import { TabsModule } from 'ngx-bootstrap';
import { PlayersConfigComponent } from './players-config/players-config.component';
import { StepsModule } from 'primeng/steps';
import { TeamInformationComponent } from './team-information/team-information.component';
import {CardModule } from 'primeng/card';
import { StartWizardComponent } from './start-wizard/start-wizard.component';
import {InputTextModule} from 'primeng/inputtext';
import {ButtonModule} from 'primeng/button';
import { HostPlayersComponent } from './host-players/host-players.component';
import { GuestPlayersComponent } from './guest-players/guest-players.component';
import { DoublesComponent } from './doubles/doubles.component';
import {SelectButtonModule} from 'primeng/selectbutton';
import { ResumeComponent } from './resume/resume.component';
import { environment } from 'src/environments/environment';
import { API_BASE_URL } from './web-api-client';
import { TableModule } from 'primeng/table';
import { ToastModule } from 'primeng/toast'
import { ScoresComponent } from './scores/scores.component';
import { DropdownModule } from 'primeng/dropdown';
import { SetScoreComponent } from './set-score/set-score.component';
import { DoubleScoresComponent } from './double-scores/double-scores.component';
import { OnGoingTeamMatchesComponent } from './on-going-team-matches/on-going-team-matches.component';
import { QuickViewComponent } from './quick-view/quick-view.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    PlayersConfigComponent,
    TeamInformationComponent,
    StartWizardComponent,
    HostPlayersComponent,
    GuestPlayersComponent,
    DoublesComponent,
    ResumeComponent,
    ScoresComponent,
    SetScoreComponent,
    DoubleScoresComponent,
    OnGoingTeamMatchesComponent,
    QuickViewComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    FontAwesomeModule,
    HttpClientModule,
    FormsModule,
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
    DropdownModule,
    ToastModule,
  ],
  providers: [
    { provide: API_BASE_URL, useValue: environment.apiUrl },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
