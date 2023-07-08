import { compileClassMetadata } from '@angular/compiler';
import { Component, NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { DoubleScoresComponent } from './double-scores/double-scores.component';
import { DoublesComponent } from './doubles/doubles.component';
import { GuestPlayersComponent } from './guest-players/guest-players.component';
import { HomeComponent } from './home/home.component';
import { HostPlayersComponent } from './host-players/host-players.component';
import { OverviewComponent } from './overview/overview.component';
import { QuickViewComponent } from './quick-view/quick-view.component';
import { ResumeComponent } from './resume/resume.component';
import { ScoresComponent } from './scores/scores.component';
import { StartWizardComponent } from './start-wizard/start-wizard.component';
import { TeamInformationComponent } from './team-information/team-information.component';

export const routes: Routes = [

  { path: 'overview/:teamMatchId', component: OverviewComponent },
  { path: 'overview/:teamMatchId/single-scores/:matchId', component: ScoresComponent},
  { path: 'overview/:teamMatchId/double-scores/:doubleMatchId', component: DoubleScoresComponent},
  { path: 'quick-view/:teamMatchId', component: QuickViewComponent},
  { path: 'quick-view/:teamMatchId/single-scores/:matchId', component: ScoresComponent},
  { path: 'quick-view/:teamMatchId/double-scores/:doubleMatchId', component: DoubleScoresComponent},
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
  // { path: 'todo', component: TodoComponent, canActivate: [AuthorizeGuard] },
  // { path: 'token', component: TokenComponent, canActivate: [AuthorizeGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule {}
