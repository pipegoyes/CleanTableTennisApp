import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class WizardService {

  wizardInformation: any;

  constructor() { 
    this.wizardInformation = {
      teamInformation: null,
      hostPlayers: null,
      guestPlayers: null,
    }

  }

  setTeamInformation(teamIformation: any): void{
    this.wizardInformation.teamInformation = teamIformation;
  }

  setHostPlayers(hostPlayers: any){
    this.wizardInformation.hostPlayers = hostPlayers;
  }

  setGuestPlayers(guestPlayers: any){
    this.wizardInformation.guestPlayers = guestPlayers;
  }

  saveTeamMatch(){
    console.log(this.wizardInformation);
  }
}
