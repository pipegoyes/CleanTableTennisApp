import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { map } from 'rxjs/operators';
import { MatchService } from '../match.service';
import { OverviewDoubleMatchDto, OverviewSingleMatchDto } from '../web-api-client';


@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.scss']
})
export class OverviewComponent implements OnInit {

  // todo change to observables
  singleMatches : OverviewSingleMatchDto[] = [];
  doubleMatches : OverviewDoubleMatchDto[] = [];


  constructor(private activatedRoute: ActivatedRoute, private matchService: MatchService, private router: Router) { }

  ngOnInit(): void {
    this.activatedRoute.params.pipe(map(p => p.teamMatchId)).subscribe(teamMatchIdEncoded => {
      this.matchService.GetAllMatches(teamMatchIdEncoded).subscribe(o => {
        this.singleMatches = o.overviewSingleMatchDtos.sort((a,b) => a.playingOrder - b.playingOrder);
        this.doubleMatches = o.overviewDoubleMatchDtos.sort((a,b) => a.playingOrder - b.playingOrder);
      });
    });
  }

  finish(): void{
    this.activatedRoute.params.pipe(map(p => p.teamMatchId)).subscribe(teamMatchIdEncoded => {
      this.matchService.FinishTeamMatch(teamMatchIdEncoded).subscribe(ok => {
        if(ok){
          this.router.navigate(['/']) 
        }
      });
    });
  }
}
