import { Component, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { AuthorizeService } from 'src/api-authorization/authorize.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  isAuthenticated$ : Observable<boolean> = of(false);

  constructor(private authService: AuthorizeService){

  }
  ngOnInit(): void {
    // this.isAuthenticated$ = this.authService.isAuthenticated();
  }
}
