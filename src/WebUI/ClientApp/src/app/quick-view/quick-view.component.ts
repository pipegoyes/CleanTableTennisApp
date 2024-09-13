import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BehaviorSubject, Observable, Subject, of } from 'rxjs';
import { map, takeUntil } from 'rxjs/operators';
import { RealTimeScoreService } from '../real-time-score.service';
import { Client, FinishTeamMatchCommand, TeamMatchOverviewDto, TeamMatchResponse } from '../web-api-client';
import { AuthService } from '@auth0/auth0-angular';

@Component({
  selector: 'app-quick-view',
  templateUrl: './quick-view.component.html',
  styleUrls: ['./quick-view.component.scss']
})
export class QuickViewComponent implements OnInit, OnDestroy {

  teamMatchDto : TeamMatchResponse;
  isAuthenticated$ : Observable<boolean> = of(false);

  overviewDto$ : Observable<TeamMatchOverviewDto>;
  overviewDtoSubject : BehaviorSubject<TeamMatchOverviewDto> = new BehaviorSubject<TeamMatchOverviewDto>(null);
  private destroy$ = new Subject<void>();


  constructor(private activatedRoute : ActivatedRoute, 
    private router : Router,
    private apiClient : Client, 
    private realTimeScoreServices : RealTimeScoreService,
    public auth: AuthService) { 
      this.realTimeScoreServices.startConnection();
    }

  ngOnInit(): void {
    this.activatedRoute.params.pipe(map(p => p.teamMatchId)).subscribe(teamMatchIdEncoded => {
      this.apiClient.getTeamMatches(teamMatchIdEncoded).subscribe(t => {
        this.teamMatchDto = t[0];
      })
      this.overviewDto$ = this.apiClient.getOverviewDto(teamMatchIdEncoded)
      this.realTimeScoreServices.registerScoreListener(teamMatchIdEncoded)

      this.realTimeScoreServices.onScoreChange()
        .pipe(takeUntil(this.destroy$))
        .subscribe(dto => {
          var overviewDto = new TeamMatchOverviewDto(dto)
          console.log(overviewDto);
          this.overviewDtoSubject.next(overviewDto);
      });     

    });
  }

  public finish(): void{
    this.activatedRoute.params.pipe(map(p => p.teamMatchId)).subscribe(teamMatchIdEncoded => {
      var command = new FinishTeamMatchCommand();
      command.teamMatchIdEncoded = teamMatchIdEncoded; 
      this.apiClient.finishTeamMatch(command).subscribe(response => {
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
