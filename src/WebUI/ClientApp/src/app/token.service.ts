import { Injectable } from '@angular/core';
import { LoginClient } from './web-api-client';
import { Observable, from, of } from 'rxjs';
import { map, mapTo, mergeMap, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class TokenService {

  token :Observable<string> = of('');
  constructor(private loginClient : LoginClient) { }

  getToken() : Observable<string> {
    // this.loginClient.post().subscribe(s => {
    //   this.token = from(s.data.text());
    //   // s.data.text().then(l => this.token = of(l))
    //   s.data.text().then(l => console.log(l))
    // });
    // return this.token;

    // this.loginClient.post().subscribe(
    //   s => s.data.text().then(l => this.token = of(l));
    // )

    return this.loginClient.post().pipe(
      mergeMap(response => from(response.data.text()))
    );
  }

}
