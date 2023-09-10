import { Injectable } from '@angular/core';
import { TeamInformation } from './team-information/team-information.component';
import { WizardInformation } from './models/WizardInformation';
import { Players } from './models/Players';

@Injectable({
  providedIn: 'root'
})
export class WizardService {
  
  wizardInformation: WizardInformation;

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
    // this.wizardInformation = this.getDummyWizard();
  }

  getDummyWizard(): WizardInformation{
    return {
      teamInformation: {
        hostName: "Kostheim",
        guestName: "Naurod"
      },
      hostPlayers: {
        player1: "Felipe Goyes1",
        player2: "Felipe Goyes2",
        player3: "Felipe Goyes3",
        player4: "Felipe Goyes4"
      },
      guestPlayers: {
        player1: "Andres Coral1",
        player2: "Andres Coral2",
        player3: "Andres Coral3",
        player4: "Andres Coral4"
      },
      hostFirstDouble: ["Felipe Goyes1", "Felipe Goyes2"],
      hostSecondDouble: ["Felipe Goyes3", "Felipe Goyes4"],
      guestFirstDouble: ["Andres Coral1", "Andres Coral2"],
      guestSecondDouble: ["Andres Coral3", "Andres Coral4"],
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
    return this.wizardInformation.hostFirstDouble;
  }

  getHostSecondDouble(): string[]{
    return this.wizardInformation.hostSecondDouble;
  }

  getGuestFirstDouble(): string[]{
    return this.wizardInformation.guestFirstDouble;
  }

  getGuestSecondDouble(): string[]{
    return this.wizardInformation.guestSecondDouble;
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


