import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { DoubleScoreService } from '../double-score.service';
import { ScoreDto } from '../web-api-client';

@Component({
  selector: 'app-double-scores',
  templateUrl: './double-scores.component.html',
  styleUrls: ['./double-scores.component.scss'],
  providers: [MessageService]
})
export class DoubleScoresComponent implements OnInit {

  teamMatchId$ : Observable<string>;
  doubleMatchIdEncoded$ : Observable<string>;
  sets: ScoreDto[] = [];
  maxNumberOfSets : number;

  constructor(private router: Router, private activatedRoute : ActivatedRoute, private scoreService : DoubleScoreService, private messageService: MessageService) { }
  


  ngOnInit(): void {
    this.teamMatchId$ = this.activatedRoute.params.pipe(map(p => p.teamMatchId));
    this.doubleMatchIdEncoded$ = this.activatedRoute.params.pipe(map(p => p.doubleMatchId));
    this.initializeSets()
  }

  
  goToOverview(): void{
    this.teamMatchId$.subscribe(i => {
      this.router.navigate(['overview', i])
    }); 
  }
  
  onHostPointsChanged(value : any, index : number) : any{
    this.sets[index].hostPoints = value;
  }

  onGuestPointsChanged(value : any, index : number) : any{
    this.sets[index].guestPoints = value;
  }

  save(): void{    
    this.doubleMatchIdEncoded$.subscribe(i => {
      this.scoreService.update(i, this.sets).subscribe(s =>{
        this.messageService.add({severity:'success', summary: 'Success', detail: 'Scores updated'})
      }, error => {
        this.messageService.add({severity:'error', summary: 'Failed', detail: 'Update failed -' + error})
      })
    });
  }

  private initializeSets(){

    this.doubleMatchIdEncoded$.subscribe(id => {
      this.scoreService.get(id).subscribe(scores => {
        this.sets = scores.scores;
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
