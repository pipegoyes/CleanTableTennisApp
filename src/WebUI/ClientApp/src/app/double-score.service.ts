import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { MatchWithScores } from './models/MatchWithScores';
import { Client, ScoreDto, UpdateDoubleMatchScoreCommand } from './web-api-client';

@Injectable({
  providedIn: 'root'
})
export class DoubleScoreService {
  private _doubleMatchWithScores : BehaviorSubject<MatchWithScores> = new BehaviorSubject({ matchIdEncoded: null, scores: []});
  matchWithScores$ = this._doubleMatchWithScores.asObservable(); 

  constructor(private apiClient: Client) { }

  update(matchIdEncoded: string, scoreRequests : ScoreDto[] ) : Observable<boolean>{
    var filteredScores = scoreRequests.filter(s => s.hostPoints != null && s.guestPoints != null);
    var updateCommand = new UpdateDoubleMatchScoreCommand({
      doubleMatchIdEncoded: matchIdEncoded,
      scoreDtos: filteredScores.filter(s => s.guestPoints != 0 || s.hostPoints != 0)
    });
    return this.apiClient.updateDoubleScore(updateCommand);
  }

  get(matchIdEncoded: string): Observable<MatchWithScores>{
    this.apiClient.getDoubleScore(matchIdEncoded).subscribe(s => {
      this._doubleMatchWithScores.next({
        matchIdEncoded: matchIdEncoded,
        scores: s
      })
    })
    return this.matchWithScores$; 
  }
}