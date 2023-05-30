import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-scores',
  templateUrl: './scores.component.html',
  styleUrls: ['./scores.component.scss']
})
export class ScoresComponent implements OnInit {

  points: number[] = [0,1,2,3,4,5,6,7];
  set1: Set;
  set2: Set;
  set3: Set;
  set4: Set;
  set5: Set;

  teamMatchIdObs : Observable<string>;
  matchIdObs : Observable<string>;

  constructor(private activatedRoute: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
    this.set1 = this.createSet();
    this.set2 = this.createSet();
    this.set3 = this.createSet();
    this.set4 = this.createSet();
    this.set5 = this.createSet();


    this.teamMatchIdObs = this.activatedRoute.params.pipe(map(p => p.teamMatchId));
    this.matchIdObs = this.activatedRoute.params.pipe(map(p => p.matchId));
  
    this.matchIdObs.subscribe(i => {
      console.log("my matchId:"+ i);
    });
  }

  createSet(): Set{
    return {
      hostPoints: null,
      guestPoints: null
    }
  }

  goToOverview(): void{
    this.teamMatchIdObs.subscribe(i => {
      this.router.navigate(['overview', i])
    });
  }

}

export interface Set{
  hostPoints: number,
  guestPoints: number
}