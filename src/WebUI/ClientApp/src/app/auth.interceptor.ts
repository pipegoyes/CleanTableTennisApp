import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { forkJoin, from, Observable, pipe } from 'rxjs';
import { AuthService } from '@auth0/auth0-angular';
import { switchMap } from 'rxjs/operators';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(public auth: AuthService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    // if(this.auth.isAuthenticated$){
    //   return from(this.auth.getAccessTokenSilently())
    //   .pipe(
    //     switchMap(token => {
    //       const modifiedRequest = request.clone({ setHeaders: { Authorization: 'Bearer ' + token } });
    //       console.log("modifiedRequest" + modifiedRequest)
    //       return next.handle(modifiedRequest);
    //     })
    //   )
    // }

    
    // pipe(
    //   forkJoin(
    //     this.auth.isAuthenticated$,
    //     this.auth.getAccessTokenSilently
    //   ),
    //   switchMap(value => {
    //     const modifiedRequest = request.clone({ setHeaders: { Authorization: 'Bearer ' + value.token } });
    //     console.log("modifiedRequest" + modifiedRequest)
    //     return next.handle(modifiedRequest);
    //   })
    // )


    // forkJoin({
    //   isAuthenticated: this.auth.isAuthenticated$,
    //   token: this.auth.getAccessTokenSilently
    // }).subscribe(value => {
    //     if(value.isAuthenticated){
    //       const modifiedRequest = request.clone({ setHeaders: { Authorization: 'Bearer ' + value.token } });
    //       console.log("modifiedRequest" + modifiedRequest)
    //       return next.handle(modifiedRequest);
    //     }
    // })

    // forkJoin({
    //   isAuthenticated: this.auth.isAuthenticated$,
    //   token: this.auth.getAccessTokenSilently
    // }).
      // return from(, )
      // .pipe(
      //   switchMap(token => {
      //     const modifiedRequest = request.clone({ setHeaders: { Authorization: 'Bearer ' + token } });
      //     console.log("modifiedRequest" + modifiedRequest)
      //     return next.handle(modifiedRequest);
      //   })
      // )
    

    return next.handle(request);
    
  }
}
