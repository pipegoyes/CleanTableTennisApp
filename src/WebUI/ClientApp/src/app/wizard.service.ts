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
    this.wizardInformation = this.getDummyWizard();
  }

  getDummyWizard(): WizardInformation{
    return {
      teamInformation: {
        hostName: "Kostheim",
        guestName: "Naurod"
      },
      hostPlayers: {
        player1: "Felipe Goyes",
        player2: "Torsten Oelhof",
        player3: "Simon Henz",
        player4: "Manfred Moses"
      },
      guestPlayers: {
        player1: "Another Goyes",
        player2: "Another Oelhof",
        player3: "Another Henz",
        player4: "Another Moses"
      },
      hostFirstDouble: ["Felipe Goyes", "Torsten Oelhof"],
      hostSecondDouble: ["Simon Henz", "Manfred Moses"],
      guestFirstDouble: ["Another Goyes", "Another Oelhof"],
      guestSecondDouble: ["Another Henz", "Another Moses"],
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


