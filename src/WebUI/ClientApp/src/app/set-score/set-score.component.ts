import { Component, Input, OnInit } from '@angular/core';

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

  constructor() { }

  ngOnInit(): void {
  }

}
