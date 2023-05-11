import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { CounterComponent } from './counter/counter.component';
import { DoublesComponent } from './doubles/doubles.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { GuestPlayersComponent } from './guest-players/guest-players.component';
import { HomeComponent } from './home/home.component';
import { HostPlayersComponent } from './host-players/host-players.component';
import { OverviewComponent } from './overview/overview.component';
import { PlayersComponent } from './players/players.component';
import { ResumeComponent } from './resume/resume.component';
import { StartWizardComponent } from './start-wizard/start-wizard.component';
import { TeamInformationComponent } from './team-information/team-information.component';
import { TeamMatchConfigurationComponent } from './team-match-configuration/team-match-configuration.component';
import { TodoComponent } from './todo/todo.component';
import { TokenComponent } from './token/token.component';

export const routes: Routes = [

  { path: 'counter', component: CounterComponent },
  { path: 'overview/:teamMatchId', component: OverviewComponent },
  { path: 'match-config', component: TeamMatchConfigurationComponent },
  { path: 'start-wizard', component: StartWizardComponent, children: [
    {
      path: '',
      redirectTo: 'teams',
      pathMatch: 'full'
    },
    {
      path: 'teams',
      component: TeamInformationComponent
    },
    {
      path: 'host-players',
      component: HostPlayersComponent
    },
    {
      path: 'guest-players',
      component: GuestPlayersComponent
    },
    {
      path: 'doubles',
      component: DoublesComponent
    },
    {
      path: 'resume',
      component: ResumeComponent
    }
  ]
},
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'todo', component: TodoComponent, canActivate: [AuthorizeGuard] },
  { path: 'token', component: TokenComponent, canActivate: [AuthorizeGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule],
})
export class AppRoutingModule {}
