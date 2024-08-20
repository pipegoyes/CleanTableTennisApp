import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ScoreService } from '../score.service';

@Component({
  selector: 'app-set-score',
  templateUrl: './set-score.component.html',
  styleUrls: ['./set-score.component.scss']
})
export class SetScoreComponent implements OnInit {

  points: number[] = Array.from(Array(31).keys());

  @Input() setNumber: string;
  @Input() hostPoints: number;
  @Input() guestPoints: number;

  @Output() hostPointsEvent = new EventEmitter<number>();
  @Output() guestPointsEvent = new EventEmitter<number>();

  constructor(private scoreService: ScoreService) { }

  ngOnInit(): void {
  }

  onHostPointChange(event: any) {
    var rivalScore = this.scoreService.calculateRivalScore(event.value);
    if(rivalScore){
      this.guestPoints = rivalScore;
      this.guestPointsEvent.emit(this.guestPoints);
    }

    this.hostPointsEvent.emit(event.value);
    
  }

  onGuestPointChange(event: any) {
    var rivalScore = this.scoreService.calculateRivalScore(event.value);
    if(rivalScore){
      this.hostPoints = rivalScore;
      this.hostPointsEvent.emit(this.hostPoints);
    }
    this.guestPointsEvent.emit(event.value);
  }

}
