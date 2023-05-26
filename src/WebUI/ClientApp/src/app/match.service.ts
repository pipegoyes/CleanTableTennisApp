import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {MatchClient, OverviewDto } from './web-api-client';

@Injectable({
  providedIn: 'root'
})
export class MatchService {

  constructor(private client: MatchClient) { }


  public GetAllMatches(teamMatchIdEncoded: string): Observable<OverviewDto>{
        return this.client.getAllMatches(teamMatchIdEncoded)
  }
}
