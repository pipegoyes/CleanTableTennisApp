import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { FinishTeamMatchCommand, OverviewClient, OverviewDto } from './web-api-client';

@Injectable({
  providedIn: 'root'
})
export class MatchService {

  constructor(private client: OverviewClient) { }


  public GetAllMatches(teamMatchIdEncoded: string): Observable<OverviewDto>{
        return this.client.getAllMatches(teamMatchIdEncoded)
  }

  public FinishTeamMatch(teamMatchIdEncoded: string): Observable<boolean> {
    var command = new FinishTeamMatchCommand({
      teamMatchIdEncoded: teamMatchIdEncoded
    });
    return this.client.finish(command);
  }
}
