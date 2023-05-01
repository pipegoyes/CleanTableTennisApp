import { Injectable } from '@angular/core';
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
      hostFirstDouble: null,
      hostSecondDouble: null,
      guestFirstDouble: null,
      guestSecondDouble: null
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

  setHostDoubles(hostFirstDouble: string[], hostSecondDouble: string[]) {
    this.wizardInformation.hostFirstDouble = hostFirstDouble;
    this.wizardInformation.hostSecondDouble = hostSecondDouble;
  }

  setGuestDoubles(guestFirstDouble: string[], guestSecondDouble: string[]) {
    this.wizardInformation.guestFirstDouble = guestFirstDouble;
    this.wizardInformation.guestSecondDouble = guestSecondDouble;
  }

  getHostFirstDouble(): string[]{
    return this.wizardInformation.hostu;
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