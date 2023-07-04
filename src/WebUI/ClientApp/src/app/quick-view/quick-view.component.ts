import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { map, takeUntil } from 'rxjs/operators';
import { RealTimeScoreService } from '../real-time-score.service';
import { OverviewClient, OverviewDto, OverviewSingleMatchDto, TeamMatchClient, TeamMatchDto } from '../web-api-client';

@Component({
  selector: 'app-quick-view',
  templateUrl: './quick-view.component.html',
  styleUrls: ['./quick-view.component.scss']
})
export class QuickViewComponent implements OnInit, OnDestroy {

  teamMatchDto$ : Observable<TeamMatchDto>;

  overviewDto$ : Observable<OverviewDto>;
  overviewDtoSubject : BehaviorSubject<OverviewDto> = new BehaviorSubject<OverviewDto>(null);
  private destroy$ = new Subject<void>();


  constructor(private activatedRoute : ActivatedRoute, 
    private teamMatchClient : TeamMatchClient, 
    private overviewClient: OverviewClient,
    private realTimeScoreServices : RealTimeScoreService) { 
      this.realTimeScoreServices.startConnection();
    }

  ngOnInit(): void {
    this.activatedRoute.params.pipe(map(p => p.teamMatchId)).subscribe(teamMatchIdEncoded => {
      this.teamMatchDto$ = this.teamMatchClient.getSingle(teamMatchIdEncoded)
      this.overviewDto$ = this.overviewClient.getAllMatches(teamMatchIdEncoded)

      this.realTimeScoreServices.registerScoreListener(teamMatchIdEncoded)

      this.realTimeScoreServices.onScoreChange()
        .pipe(takeUntil(this.destroy$))
        .subscribe(dto => {
          console.log(dto);
          this.overviewDtoSubject.next(dto);
      });     

    });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

}
