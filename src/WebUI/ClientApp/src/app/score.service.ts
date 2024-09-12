import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { UpdateMatchScoreCommand, ScoreDto, Client } from './web-api-client';

@Injectable({
  providedIn: 'root'
})
export class ScoreService {

  constructor(private apiClient: Client) { }

  update(matchIdEncoded: string, scoreRequests : ScoreDto[], teamMatchIdEncoded: string) : Observable<boolean>{
    var filteredScores = scoreRequests.filter(s => s.hostPoints != null && s.guestPoints != null);
    var updateCommand = new UpdateMatchScoreCommand({
      matchIdEncoded: matchIdEncoded,
      teamMatchIdEncoded: teamMatchIdEncoded,
      scoreDtos: filteredScores.filter(s => s.guestPoints != 0 || s.hostPoints != 0)
    });
    return this.apiClient.updateScore(updateCommand);
  }

  get(matchIdEncoded: string): Observable<ScoreDto[]>{
    return this.apiClient.getScore(matchIdEncoded);
  }

  calculateRivalScore(points: number): number{
    if(points != 11 && points < 11){
      return 11;
    }
    return null;
  }
}
