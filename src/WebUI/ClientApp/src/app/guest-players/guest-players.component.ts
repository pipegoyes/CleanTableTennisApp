import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';

@Component({
  selector: 'app-guest-players',
  templateUrl: './guest-players.component.html',
  styleUrls: ['./guest-players.component.scss']
})
export class GuestPlayersComponent implements OnInit {

  guestPlayers: any;

  submitted: boolean = false;
  
  constructor(private router: Router) { }

  ngOnInit(): void {
    this.guestPlayers = {
      player1: "",
      player2: "",
      player3: "",
      player4: ""
    }
  }

  nextPage(): void{
    if (this.guestPlayers.player1 && this.guestPlayers.player2 && this.guestPlayers.player3 && this.guestPlayers.player4){
        this.router.navigate(['start-wizard/guest-players']);
        return;
    }
    this.submitted = true;
  }

}
