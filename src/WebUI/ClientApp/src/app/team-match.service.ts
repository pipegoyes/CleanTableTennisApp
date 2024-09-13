import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Players } from './models/Players';
import { WizardInformation } from './models/WizardInformation';
import { Client, CreateTeamMatchRequest, DoublePosition, CreatePlayerRequest, TeamMatchResponse, CreateTeamPlayersRequest } from './web-api-client';

@Injectable({
  providedIn: 'root'
})
export class TeamMatchService {

  constructor(private teamMatchClient: Client) { }


  get() : Observable<TeamMatchResponse[]>{
    return this.teamMatchClient.getTeamMatches(null);
  }

  save(wizardInformation: WizardInformation): Observable<string> {
    var teamMatchCommand = this.ToTeamMatchCommand(wizardInformation);
    return this.teamMatchClient.createTeamMatch(teamMatchCommand);
  }

  private ToTeamMatchCommand(wizardInformation: WizardInformation): CreateTeamMatchRequest{

    var teamMatchCommand = new CreateTeamMatchRequest();
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
    return new CreateTeamPlayersRequest ({name: name, players: [
      new CreatePlayerRequest({fullName: players.player1, doublePosition: this.GetDoublePosition(players.player1, firstDoubleNames, secondDoubleNames)}),
      new CreatePlayerRequest({fullName: players.player2, doublePosition: this.GetDoublePosition(players.player2, firstDoubleNames, secondDoubleNames)}),
      new CreatePlayerRequest({fullName: players.player3, doublePosition: this.GetDoublePosition(players.player3, firstDoubleNames, secondDoubleNames)}),
      new CreatePlayerRequest({fullName: players.player4, doublePosition: this.GetDoublePosition(players.player4, firstDoubleNames, secondDoubleNames)}),
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