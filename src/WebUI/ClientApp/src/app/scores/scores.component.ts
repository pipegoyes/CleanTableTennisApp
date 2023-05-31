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
  set1: ScoreRequest;
  set2: ScoreRequest;
  set3: ScoreRequest;
  set4: ScoreRequest;
  set5: ScoreRequest;

  teamMatchIdObs : Observable<string>;
  matchIdObs : Observable<string>;

  constructor(private activatedRoute: ActivatedRoute, private router: Router, private scoreService: ScoreService) { }

  ngOnInit(): void {
    this.set1 = this.createSet();
    this.set2 = this.createSet();
    this.set3 = this.createSet();
    this.set4 = this.createSet();
    this.set5 = this.createSet();


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
      this.scoreService.update(i, [this.set1, this.set2, this.set3, this.set4,this.set5]).subscribe(s =>{
        console.log("Works?"+ s)
      })
    });
    
  }

}