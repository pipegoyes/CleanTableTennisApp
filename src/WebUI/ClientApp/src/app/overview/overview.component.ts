import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { map } from 'rxjs/operators';


@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.scss']
})
export class OverviewComponent implements OnInit {

  private teamMatchIdEncoded: string;
  matches : any[];


  constructor(private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void {
    this.matches = [{
      hostPlayer: "Felipe Goyes",
      guestPlayer: "Another Player",
      hostPoints: 3,
      guestPoints: 1,
      type: "single",
      scoreId: "scoreId"
    }]
    this.activatedRoute.params.pipe(map(p => p.teamMatchId)).subscribe(teamMatchId => {
      this.teamMatchIdEncoded = teamMatchId;
    });
  }

}
