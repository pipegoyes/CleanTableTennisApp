import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { map } from 'rxjs/operators';
import { MatchService } from '../match.service';
import { OverviewDoubleMatchDto, OverviewSingleMatchDto } from '../web-api-client';


@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.scss']
})
export class OverviewComponent implements OnInit {

  singleMatches : OverviewSingleMatchDto[] = [];
  doubleMatches : OverviewDoubleMatchDto[] = [];


  constructor(private activatedRoute: ActivatedRoute, private matchService: MatchService) { }

  ngOnInit(): void {
    this.activatedRoute.params.pipe(map(p => p.teamMatchId)).subscribe(teamMatchIdEncoded => {
      this.matchService.GetAllMatches(teamMatchIdEncoded).subscribe(o => {
        this.singleMatches = o.overviewSingleMatchDtos;
        this.doubleMatches = o.overviewDoubleMatchDtos;
      });
    });
  }

}
