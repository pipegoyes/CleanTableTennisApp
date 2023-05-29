import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-scores',
  templateUrl: './scores.component.html',
  styleUrls: ['./scores.component.scss']
})
export class ScoresComponent implements OnInit {

  teamMatchIdObs : Observable<string>;
  matchIdObs : Observable<string>;

  constructor(private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.teamMatchIdObs = this.activatedRoute.params.pipe(map(p => p.teamMatchId));
    this.matchIdObs = this.activatedRoute.params.pipe(map(p => p.matchId));

    this.teamMatchIdObs.subscribe(i => {
      console.log("my TeamMatch:" + i);
    });

    this.matchIdObs.subscribe(i => {
      console.log("my matchId:"+ i);
    });
  }

}
