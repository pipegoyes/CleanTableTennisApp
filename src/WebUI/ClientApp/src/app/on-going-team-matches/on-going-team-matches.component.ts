import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { TeamMatchService } from '../team-match.service';
import { TeamMatchDto } from '../web-api-client';

@Component({
  selector: 'app-on-going-team-matches',
  templateUrl: './on-going-team-matches.component.html',
  styleUrls: ['./on-going-team-matches.component.scss']
})
export class OnGoingTeamMatchesComponent implements OnInit {

  teamMatches$: Observable<TeamMatchDto[]>

  constructor(private teamMatchService: TeamMatchService) { }

  ngOnInit(): void {
    this.teamMatches$ = this.teamMatchService.get();
  }

}
