import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BehaviorSubject, Observable, Subject, of } from 'rxjs';
import { map, takeUntil } from 'rxjs/operators';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { RealTimeScoreService } from '../real-time-score.service';
import { FinishTeamMatchCommand, OverviewClient, OverviewDto, OverviewSingleMatchDto, TeamMatchClient, TeamMatchDto } from '../web-api-client';

@Component({
  selector: 'app-quick-view',
  templateUrl: './quick-view.component.html',
  styleUrls: ['./quick-view.component.scss']
})
export class QuickViewComponent implements OnInit, OnDestroy {

  teamMatchDto$ : Observable<TeamMatchDto>;
  isAuthenticated$ : Observable<boolean> = of(false);

  overviewDto$ : Observable<OverviewDto>;
  overviewDtoSubject : BehaviorSubject<OverviewDto> = new BehaviorSubject<OverviewDto>(null);
  private destroy$ = new Subject<void>();


  constructor(private activatedRoute : ActivatedRoute, 
    private router : Router,
    private teamMatchClient : TeamMatchClient, 
    private overviewClient: OverviewClient,
    private authService : AuthorizeService,
    private realTimeScoreServices : RealTimeScoreService) { 
      this.realTimeScoreServices.startConnection();
    }

  ngOnInit(): void {
    this.activatedRoute.params.pipe(map(p => p.teamMatchId)).subscribe(teamMatchIdEncoded => {
      this.teamMatchDto$ = this.teamMatchClient.getSingle(teamMatchIdEncoded)
      this.overviewDto$ = this.overviewClient.getAllMatches(teamMatchIdEncoded)
      // this.isAuthenticated$ = this.authService.isAuthenticated();

      this.realTimeScoreServices.registerScoreListener(teamMatchIdEncoded)

      this.realTimeScoreServices.onScoreChange()
        .pipe(takeUntil(this.destroy$))
        .subscribe(dto => {
          var overviewDto = new OverviewDto(dto)
          console.log(overviewDto);
          this.overviewDtoSubject.next(overviewDto);
      });     

    });
  }

  public finish(): void{
    this.activatedRoute.params.pipe(map(p => p.teamMatchId)).subscribe(teamMatchIdEncoded => {
      var command = new FinishTeamMatchCommand();
      command.teamMatchIdEncoded = teamMatchIdEncoded; 
      this.overviewClient.finish(command).subscribe(response =>{
        if(response){
          this.router.navigate([''])
        }
      })
    });
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

}
