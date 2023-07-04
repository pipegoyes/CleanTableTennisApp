import { Inject, Injectable } from '@angular/core';
import { API_BASE_URL, OverviewDto, ScoreDto } from './web-api-client';
import * as signalR from "@microsoft/signalr"
import { BehaviorSubject, Observable, of, Subject } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class RealTimeScoreService {

  baseUrl: string;
  overviewDtoSubject : Subject<OverviewDto> = new Subject<OverviewDto>() ;

  private hubConnection: signalR.HubConnection
    public startConnection = () => {
      this.hubConnection = new signalR.HubConnectionBuilder()
                              .withUrl(this.baseUrl + '/real-time-scores')
                              .build();
      this.hubConnection
        .start()
        .then(() => console.log('Connection started'))
        .catch(err => console.log('Error while starting connection: ' + err))
    }

  constructor(@Inject(API_BASE_URL) baseUrl?: string) { 
    this.baseUrl = baseUrl;
  }

  public registerScoreListener(matchIdEncoded: string): void {
    this.hubConnection.on('scores-matchId:' + matchIdEncoded, (data) => {
      this.overviewDtoSubject.next(data)
    });
  }

  public onScoreChange() : Observable<OverviewDto>{
    return this.overviewDtoSubject.asObservable();
  }
}
