import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ScoresClient, UpdateMatchScoreCommand, ScoreRequest } from './web-api-client';

@Injectable({
  providedIn: 'root'
})
export class ScoreService {

  constructor(private scoreClient: ScoresClient) { }

  update(matchIdEncoded: string, scoreRequests : ScoreRequest[] ) : Observable<boolean>{
    var updateCommand = new UpdateMatchScoreCommand();
    updateCommand.matchIdEncoded = matchIdEncoded;
    updateCommand.scoreRequests = scoreRequests.filter(s => s.hostPoints != null && s.guestPoints != null);

    return this.scoreClient.update(updateCommand);
  }
}
