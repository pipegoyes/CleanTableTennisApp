import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { TeamMatchService } from '../team-match.service';
import { TeamMatchResponse } from '../web-api-client';
import { AuthService } from '@auth0/auth0-angular';

@Component({
  selector: 'app-on-going-team-matches',
  templateUrl: './on-going-team-matches.component.html',
  styleUrls: ['./on-going-team-matches.component.scss']
})
export class OnGoingTeamMatchesComponent implements OnInit {

  teamMatches$: Observable<TeamMatchResponse[]>

  constructor(private teamMatchService: TeamMatchService, private auth : AuthService) { }

  ngOnInit(): void {
    this.teamMatches$ = this.teamMatchService.get();
  }

}
