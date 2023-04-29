import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import { Players, WizardService } from '../wizard.service';

@Component({
  selector: 'app-guest-players',
  templateUrl: './guest-players.component.html',
  styleUrls: ['./guest-players.component.scss']
})
export class GuestPlayersComponent implements OnInit {

  guestPlayers: Players;

  submitted: boolean = false;
  
  constructor(private router: Router, private wizardService: WizardService) { }

  ngOnInit(): void {
    this.guestPlayers = this.wizardService.getGuestPlayers();
  }

  nextPage(): void{
    if (this.guestPlayers.player1 && this.guestPlayers.player2 && this.guestPlayers.player3 && this.guestPlayers.player4){
        this.wizardService.setGuestPlayers(this.guestPlayers);
        this.wizardService.saveTeamMatch();
        this.router.navigate(['start-wizard/doubles']);
        return;
    }
    this.submitted = true;
  }

  prevPage(): void{
    this.router.navigate(['start-wizard/host-players'])
  }

}
