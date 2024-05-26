import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { AuthorizeService } from 'src/api-authorization/authorize.service';
import { TokenService } from '../token.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  isAuthenticated$ : Observable<boolean> = of(false);
  token : string = null;

  constructor(private tokenService: TokenService){

  }
  ngOnInit(): void {
    // this.isAuthenticated$ = this.authService.isAuthenticated();
    this.tokenService.getToken().subscribe(t => this.token = t);
  }
}
