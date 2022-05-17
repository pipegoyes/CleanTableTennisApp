import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-players-config',
  templateUrl: './players-config.component.html',
  styleUrls: ['./players-config.component.scss']
})
export class PlayersConfigComponent implements OnInit {

  // players: number[] = [1,2,3,4];
  players: number[] = [1,2,3,4];

  constructor() { }

  ngOnInit(): void {
  }

}
