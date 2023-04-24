import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthorizeGuard } from 'src/api-authorization/authorize.guard';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { HomeComponent } from './home/home.component';
import { StartWizardComponent } from './start-wizard/start-wizard.component';
import { TeamInformationComponent } from './team-information/team-information.component';
import { TeamMatchConfigurationComponent } from './team-match-configuration/team-match-configuration.component';
import { TodoComponent } from './todo/todo.component';
import { TokenComponent } from './token/token.component';

export const routes: Routes = [

  { path: 'counter', component: CounterComponent },
  { path: 'fetch-data', component: FetchDataComponent },
  { path: 'match-config', component: TeamMatchConfigurationComponent },
  { path: 'start-wizard', component: StartWizardComponent },
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'todo', component: TodoComponent, canActivate: [AuthorizeGuard] },
  { path: 'token', component: TokenComponent, canActivate: [AuthorizeGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule],
})
export class AppRoutingModule {}
