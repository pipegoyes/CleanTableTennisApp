import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ScoresClient, UpdateMatchScoreCommand, ScoreRequest } from './web-api-client';

@Injectable({
  providedIn: 'root'
})
export class ScoreService {

  constructor(private scoreClient: ScoresClient) { }

  update(matchIdEncoded: string, scoreRequests : ScoreRequest[] ) : Observable<boolean>{
    var updateCommand = new UpdateMatchScoreCommand({
      matchIdEncoded: matchIdEncoded,
      scoreRequests: scoreRequests 
    })

    return this.scoreClient.update(updateCommand);
  }
}
