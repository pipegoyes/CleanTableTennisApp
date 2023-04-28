import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { WizardService } from '../wizard.service';

@Component({
  selector: 'app-host-players',
  templateUrl: './host-players.component.html',
  styleUrls: ['./host-players.component.scss']
})
export class HostPlayersComponent implements OnInit {

  hostPlayers: any;

  submitted: boolean = false;
  
  constructor(private router: Router, private wizardService: WizardService) { }

  ngOnInit(): void {
    this.hostPlayers = {
      player1: "",
      player2: "",
      player3: "",
      player4: ""
    }
  }

  nextPage(): void{
    if (this.hostPlayers.player1 && this.hostPlayers.player2 && this.hostPlayers.player3 && this.hostPlayers.player4){
        this.wizardService.setHostPlayers(this.hostPlayers);
        this.router.navigate(['start-wizard/guest-players']);
        return;
    }
    this.submitted = true;
  }

  prevPage(): void{
    this.router.navigate(['start-wizard/teams'])
  }

}