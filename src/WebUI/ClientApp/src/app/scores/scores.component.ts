import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ScoreService } from '../score.service';
import { IScoreRequest, ScoreRequest } from '../web-api-client';

@Component({
  selector: 'app-scores',
  templateUrl: './scores.component.html',
  styleUrls: ['./scores.component.scss']
})
export class ScoresComponent implements OnInit {

  points: number[] = [0,1,2,3,4,5,6,7];
  maxNumberOfSets: number = 5;
  sets: ScoreRequest[] = [];

  teamMatchIdObs : Observable<string>;
  matchIdObs : Observable<string>;

  constructor(private activatedRoute: ActivatedRoute, private router: Router, private scoreService: ScoreService) { }

  ngOnInit(): void {
    this.initializeSets();
    this.teamMatchIdObs = this.activatedRoute.params.pipe(map(p => p.teamMatchId));
    this.matchIdObs = this.activatedRoute.params.pipe(map(p => p.matchId));
  
  }

  createSet(): ScoreRequest{
    return new ScoreRequest({
      hostPoints: null,
      guestPoints: null
    })
  }

  goToOverview(): void{
    this.teamMatchIdObs.subscribe(i => {
      this.router.navigate(['overview', i])
    }); 
  }

  save(): void{    
    this.matchIdObs.subscribe(i => {
      this.scoreService.update(i, this.sets).subscribe(s =>{
        console.log("Works?"+ s)
      })
    });
  }

  onHostPointsChanged(value : any, index : number) : any{
    this.sets[index].hostPoints = value;
    console.log("value"+ value + " index "+ index)
  }

  onGuestPointsChanged(value : any, index : number) : any{
    this.sets[index].guestPoints = value;
    console.log("value"+ value + " index "+ index)
  }

  private initializeSets(){
    for (let index = 0; index < this.maxNumberOfSets; index++) {
      this.sets[index] = this.createSet();
    }
  }

}