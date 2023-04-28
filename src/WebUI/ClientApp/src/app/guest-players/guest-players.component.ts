import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import { WizardService } from '../wizard.service';

@Component({
  selector: 'app-guest-players',
  templateUrl: './guest-players.component.html',
  styleUrls: ['./guest-players.component.scss']
})
export class GuestPlayersComponent implements OnInit {

  guestPlayers: any;

  submitted: boolean = false;
  
  constructor(private router: Router, private wizardService: WizardService) { }

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
        this.wizardService.setGuestPlayers(this.guestPlayers);
        this.wizardService.saveTeamMatch();
        this.router.navigate(['start-wizard/guest-players']);
        return;
    }
    this.submitted = true;
  }

  prevPage(): void{
    this.router.navigate(['start-wizard/host-players'])
  }

}
