import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { map } from 'rxjs/operators';
import { MatchService } from '../match.service';


@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.scss']
})
export class OverviewComponent implements OnInit {

  private teamMatchIdEncoded: string;
  matches : any[] = [];


  constructor(private activatedRoute: ActivatedRoute, private matchService: MatchService) { }

  ngOnInit(): void {

    // todo create score entity to get scoreId 
    // this.matches = [{
    //   hostPlayer: "Felipe Goyes",
    //   guestPlayer: "Another Player",
    //   hostPoints: 3,
    //   guestPoints: 1,
    // }];

    this.activatedRoute.params.pipe(map(p => p.teamMatchId)).subscribe(teamMatchIdEncoded => {
      this.teamMatchIdEncoded = teamMatchIdEncoded;

      this.matchService.GetAllMatches(teamMatchIdEncoded).subscribe(o => {
        this.matches = o.overviewSingleMatchDtos;
      });
    });
  }

}
