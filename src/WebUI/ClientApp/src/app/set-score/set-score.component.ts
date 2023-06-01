import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

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

  constructor() { }

  ngOnInit(): void {
  }

  onHostPointChange(event: any) {
    this.hostPointsEvent.emit(event.value);
  }

  onGuestPointChange(event: any) {
    this.guestPointsEvent.emit(event.value);
  }

}
