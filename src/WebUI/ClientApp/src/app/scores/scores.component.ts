import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { ScoreService } from '../score.service';
import { ScoreDto } from '../web-api-client';

@Component({
  selector: 'app-scores',
  templateUrl: './scores.component.html',
  styleUrls: ['./scores.component.scss'],
  providers: [MessageService]
})
export class ScoresComponent implements OnInit {

  maxNumberOfSets: number = 5;
  sets: ScoreDto[] = [];

  teamMatchId$ : Observable<string>;
  matchId$ : Observable<string>;

  constructor(private activatedRoute: ActivatedRoute, private router: Router, private scoreService: ScoreService, private messageService : MessageService) { }

  ngOnInit(): void {
    // Bug-fixed you need to initalize all scores first, because this array render the dropdowns first
    this.sets.push(new ScoreDto())
    this.sets.push(new ScoreDto())
    this.sets.push(new ScoreDto())
    this.sets.push(new ScoreDto())
    this.sets.push(new ScoreDto())

    this.teamMatchId$ = this.activatedRoute.params.pipe(map(p => p.teamMatchId));
    this.matchId$ = this.activatedRoute.params.pipe(map(p => p.matchId));
    this.initializeSets();
  
  }

  goToQuickView(): void{
    this.teamMatchId$.subscribe(i => {
      this.router.navigate(['quick-view', i])
    }); 
  }

  save(): void{    
    this.teamMatchId$.subscribe(teamMatchIdEncoded => {
      this.matchId$.subscribe(i => {
        this.scoreService.update(i, this.sets, teamMatchIdEncoded).subscribe(s =>{
          this.messageService.add({severity:'success', summary: 'Success', detail: 'Scores updated'})
        }, error => {
          this.messageService.add({severity:'error', summary: 'Failed', detail: 'Update failed -' + error})
        })
      });
    })
  }

  onHostPointsChanged(value : any, index : number) : any{
    this.sets[index].hostPoints = value;
  }

  onGuestPointsChanged(value : any, index : number) : any{
    this.sets[index].guestPoints = value;
  }

  private initializeSets(){

    this.matchId$.subscribe(id => {
      this.scoreService.get(id).subscribe(scores => {
        
        scores.forEach( (element, i) => {
          if(this.sets[i]){
            this.sets[i].guestPoints = element.guestPoints;
            this.sets[i].hostPoints = element.hostPoints;
            this.sets[i].scoreIdEncoded = element.scoreIdEncoded;
          }
        });
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

  private createSet(): ScoreDto{
    return new ScoreDto({
      hostPoints: null,
      guestPoints: null
    })
  }


}