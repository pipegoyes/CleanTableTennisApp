import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { OverviewClient, OverviewDto, OverviewSingleMatchDto, TeamMatchClient, TeamMatchDto } from '../web-api-client';

@Component({
  selector: 'app-quick-view',
  templateUrl: './quick-view.component.html',
  styleUrls: ['./quick-view.component.scss']
})
export class QuickViewComponent implements OnInit {

  teamMatchDto$ : Observable<TeamMatchDto>;
  overviewDto$ : Observable<OverviewDto>;

  constructor(private activatedRoute : ActivatedRoute, private teamMatchClient : TeamMatchClient, private overviewClient: OverviewClient) { }

  ngOnInit(): void {

    this.activatedRoute.params.pipe(map(p => p.teamMatchId)).subscribe(teamMatchIdEncoded => {
      this.teamMatchDto$ = this.teamMatchClient.getSingle(teamMatchIdEncoded)
      this.overviewDto$ = this.overviewClient.getAllMatches(teamMatchIdEncoded)
    });
  }

}
