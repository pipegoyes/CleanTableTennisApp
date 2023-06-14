import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { fileURLToPath } from 'url';
import { ScoresClient, UpdateMatchScoreCommand, ScoreDto } from './web-api-client';

@Injectable({
  providedIn: 'root'
})
export class ScoreService {

  private _matchWithScores : BehaviorSubject<MatchWithScores> = new BehaviorSubject({ matchIdEncoded: null, scores: []});
  matchWithScores$ = this._matchWithScores.asObservable(); 

  constructor(private scoreClient: ScoresClient) { }

  update(matchIdEncoded: string, scoreRequests : ScoreDto[] ) : Observable<boolean>{
    var filteredScores = scoreRequests.filter(s => s.hostPoints != null && s.guestPoints != null);
    var updateCommand = new UpdateMatchScoreCommand({
      matchIdEncoded: matchIdEncoded,
      scoreDtos: filteredScores
    });
    return this.scoreClient.update(updateCommand);
  }

  get(matchIdEncoded: string): Observable<MatchWithScores>{
    this.scoreClient.get(matchIdEncoded).subscribe(s => {
      this._matchWithScores.next({
        matchIdEncoded: matchIdEncoded,
        scores: s
      })
    })
    return this.matchWithScores$; 
  }
}
export interface MatchWithScores{
  matchIdEncoded : string,
  scores: ScoreDto[]
}