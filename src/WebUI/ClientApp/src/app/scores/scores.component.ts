import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ScoreService } from '../score.service';
import { ScoreDto } from '../web-api-client';

@Component({
  selector: 'app-scores',
  templateUrl: './scores.component.html',
  styleUrls: ['./scores.component.scss']
})
export class ScoresComponent implements OnInit {

  maxNumberOfSets: number = 5;
  sets: ScoreDto[] = [];

  teamMatchIdObs : Observable<string>;
  matchIdObs : Observable<string>;

  constructor(private activatedRoute: ActivatedRoute, private router: Router, private scoreService: ScoreService) { }

  ngOnInit(): void {
    this.teamMatchIdObs = this.activatedRoute.params.pipe(map(p => p.teamMatchId));
    this.matchIdObs = this.activatedRoute.params.pipe(map(p => p.matchId));
    this.initializeSets();
  
  }

  createSet(): ScoreDto{
    return new ScoreDto({
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
  }

  onGuestPointsChanged(value : any, index : number) : any{
    this.sets[index].guestPoints = value;
  }

  private initializeSets(){

    this.matchIdObs.subscribe(id => {
      this.scoreService.get(id).subscribe(scores => {
        this.sets = scores;
        this.fillEmptyScores();
      });
    })
  }

  private fillEmptyScores(){
    for (let index = 0; index < this.maxNumberOfSets; index++) {
      if(!this.sets[index]){
        this.sets[index] = this.createSet();
      }
    }
  }

}