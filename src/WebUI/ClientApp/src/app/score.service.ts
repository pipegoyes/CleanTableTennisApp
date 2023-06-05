import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ScoresClient, UpdateMatchScoreCommand, ScoreDto } from './web-api-client';

@Injectable({
  providedIn: 'root'
})
export class ScoreService {

  constructor(private scoreClient: ScoresClient) { }

  update(matchIdEncoded: string, scoreRequests : ScoreDto[] ) : Observable<boolean>{
    var updateCommand = new UpdateMatchScoreCommand();
    updateCommand.matchIdEncoded = matchIdEncoded;
    updateCommand.scoreDtos = scoreRequests.filter(s => s.hostPoints != null && s.guestPoints != null);

    return this.scoreClient.update(updateCommand);
  }

  get(matchIdEncoded: string): Observable<ScoreDto[]>{
    return this.scoreClient.get(matchIdEncoded);
  }
}
