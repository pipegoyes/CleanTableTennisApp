import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { MatchWithScores } from './models/MatchWithScores';
import { ScoresClient, UpdateMatchScoreCommand, ScoreDto } from './web-api-client';

@Injectable({
  providedIn: 'root'
})
export class ScoreService {

  constructor(private scoreClient: ScoresClient) { }

  update(matchIdEncoded: string, scoreRequests : ScoreDto[], teamMatchIdEncoded: string) : Observable<boolean>{
    var filteredScores = scoreRequests.filter(s => s.hostPoints != null && s.guestPoints != null);
    var updateCommand = new UpdateMatchScoreCommand({
      matchIdEncoded: matchIdEncoded,
      teamMatchIdEncoded: teamMatchIdEncoded,
      scoreDtos: filteredScores.filter(s => s.guestPoints != 0 || s.hostPoints != 0)
    });
    return this.scoreClient.update(updateCommand);
  }

  get(matchIdEncoded: string): Observable<ScoreDto[]>{
    return this.scoreClient.get(matchIdEncoded);
  }
}
