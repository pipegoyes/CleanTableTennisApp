import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { TeamMatchClient, TeamMatchDto } from '../web-api-client';

@Component({
  selector: 'app-quick-view',
  templateUrl: './quick-view.component.html',
  styleUrls: ['./quick-view.component.scss']
})
export class QuickViewComponent implements OnInit {

  dto$ : Observable<TeamMatchDto>;

  constructor(private activatedRoute : ActivatedRoute, private teamMatchClient : TeamMatchClient) { }

  ngOnInit(): void {
    this.activatedRoute.params.pipe(map(p => p.teamMatchId)).subscribe(teamMatchIdEncoded => {
      this.dto$ = this.teamMatchClient.getSingle(teamMatchIdEncoded)
    });
  }

}
