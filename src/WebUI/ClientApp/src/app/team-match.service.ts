import { Injectable } from '@angular/core';
import { Players } from './models/Players';
import { WizardInformation } from './models/WizardInformation';
import { CreateTeamMatchCommand, DoublePosition, ITeamMatchClient, PlayerRequest, TeamMatchClient, TeamRequest } from './web-api-client';

@Injectable({
  providedIn: 'root'
})
export class TeamMatchService {

  constructor(private teamMatchClient: TeamMatchClient) { }


  public save(wizardInformation: WizardInformation){
    var teamMatchCommand = this.ToTeamMatchCommand(wizardInformation);
    this.teamMatchClient.create(teamMatchCommand).subscribe((s) => {
      console.log("subscribed: "+ s);
    });
  }

  private ToTeamMatchCommand(wizardInformation: WizardInformation): CreateTeamMatchCommand{

    var teamMatchCommand = new CreateTeamMatchCommand();
    teamMatchCommand.hostTeam = this.CreateTeamRequest(
      wizardInformation.teamInformation.hostName, 
      wizardInformation.hostPlayers,
      wizardInformation.hostFirstDouble,
      wizardInformation.hostSecondDouble);

    teamMatchCommand.guestTeam = this.CreateTeamRequest(
      wizardInformation.teamInformation.guestName, 
      wizardInformation.guestPlayers,
      wizardInformation.guestFirstDouble,
      wizardInformation.guestSecondDouble);

    return teamMatchCommand;
  }

  private CreateTeamRequest(name: string, players: Players, firstDoubleNames: string[], secondDoubleNames: string[]){
    return new TeamRequest({name: name, players: [
      new PlayerRequest({fullName: players.player1, doublePosition: this.GetDoublePosition(players.player1, firstDoubleNames, secondDoubleNames)}),
      new PlayerRequest({fullName: players.player2, doublePosition: this.GetDoublePosition(players.player2, firstDoubleNames, secondDoubleNames)}),
      new PlayerRequest({fullName: players.player3, doublePosition: this.GetDoublePosition(players.player3, firstDoubleNames, secondDoubleNames)}),
      new PlayerRequest({fullName: players.player4, doublePosition: this.GetDoublePosition(players.player4, firstDoubleNames, secondDoubleNames)}),
    ]})
  }

  private GetDoublePosition(playerName: string, firstDoubleNames: string[], secondDoubleNames: string[]): DoublePosition{
    if(firstDoubleNames.includes(playerName)){
      return DoublePosition.FirstDouble
    }

    if(secondDoubleNames.includes(playerName)){
      return DoublePosition.SecondDouble;
    }

    return DoublePosition.None;
  }
}