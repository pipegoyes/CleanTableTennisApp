import { Injectable } from '@angular/core';
import { Doubles } from './doubles/doubles.component';
import { TeamInformation } from './team-information/team-information.component';

@Injectable({
  providedIn: 'root'
})
export class WizardService {
  
  wizardInformation: any;

  constructor() { 
    this.wizardInformation = {
      teamInformation: new TeamInformation(),
      hostPlayers: new Players(),
      guestPlayers: new Players(),
      hostDoubles: new Doubles(),
      guestDoubles: new Doubles()
    }

  }

  setTeamInformation(teamIformation: TeamInformation): void{
    this.wizardInformation.teamInformation = teamIformation;
  }

  setHostPlayers(hostPlayers: Players){
    this.wizardInformation.hostPlayers = hostPlayers;
  }

  setGuestPlayers(guestPlayers: Players){
    this.wizardInformation.guestPlayers = guestPlayers;
  }

  setDoubles(hostDoubles: Doubles, guestDoubles: Doubles) {
    this.wizardInformation.hostDoubles = hostDoubles;
    this.wizardInformation.guestDoubles = guestDoubles;
  }

  getHostDoubles(): Doubles{
    return this.wizardInformation.hostDoubles;
  }

  getTeamInformation(): TeamInformation {
    return this.wizardInformation.teamInformation;
  }

  getHostPlayers(): Players{
    return this.wizardInformation.hostPlayers;
  }

  getGuestPlayers(): Players{
    return this.wizardInformation.guestPlayers;
  }

  saveTeamMatch(){
    console.log(this.wizardInformation);
  }
}

export class Players{
  player1: string = ""
  player2: string = ""
  player3: string = ""
  player4: string = ""
}