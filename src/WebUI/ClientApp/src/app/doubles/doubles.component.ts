import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Players, WizardService } from '../wizard.service';

@Component({
  selector: 'app-doubles',
  templateUrl: './doubles.component.html',
  styleUrls: ['./doubles.component.scss']
})
export class DoublesComponent implements OnInit {

  selectHostPlayers: SelectPlayer[];
  selectGuestPlayers: SelectPlayer[];

  selectedFirstDoubleHost: string[]; 
  selectedSecondDoubleHost: string[]; 
  
  selectedFirstDoubleGuest: string[]; 
  selectedSecondDoubleGuest: string[]; 

  submitted: boolean = false;

  constructor(private router: Router, private wizardService: WizardService) { }

  ngOnInit(): void {
    var hostPlayers = this.wizardService.getHostPlayers();
    var guestPlayers = this.wizardService.getGuestPlayers();
    this.selectHostPlayers = this.toSelectPlayers(hostPlayers);
    this.selectGuestPlayers = this.toSelectPlayers(guestPlayers);
  }

  nextPage(): void{
    if (this.validateHostDoubles() && this.validateGuestDoubles() ){
      this.wizardService.setHostDoubles(this.selectedFirstDoubleHost, this.selectedSecondDoubleHost);
      this.wizardService.setGuestDoubles(this.selectedFirstDoubleGuest, this.selectedSecondDoubleGuest);
      this.wizardService.saveTeamMatch();
      this.router.navigate(['start-wizard/resume']);
      return;
    }

    this.submitted = true;
  }

  prevPage(): void{
    this.router.navigate(['start-wizard/guest-players'])
  }

  private toSelectPlayers(players: Players): SelectPlayer[]{
    return [{
      label: players.player1,
      value: players.player1
    },{
      label: players.player2,
      value: players.player2
    },{
      label: players.player3,
      value: players.player3
    },{
      label: players.player4,
      value: players.player4
    }]
  }

  private validateGuestDoubles() {
    return true;
  }

  private validateHostDoubles(): boolean{
    return this.selectedFirstDoubleHost.length == 2 && this.selectedSecondDoubleHost.length == 2;
  }
}

export interface SelectPlayer{
  label: string
  value: string 
}