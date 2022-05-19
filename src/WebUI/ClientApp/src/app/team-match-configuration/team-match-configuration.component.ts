import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-team-match-configuration',
  templateUrl: './team-match-configuration.component.html',
  styleUrls: ['./team-match-configuration.component.scss']
})
export class TeamMatchConfigurationComponent implements OnInit {

  hostTeamName = new FormControl("Host");
  guestTeamName = new FormControl("Guest");
  player1 = new FormControl("");
  teamMatchConfiguration = new FormGroup({
    hostTeamName : this.hostTeamName,
    guestTeamName : this.guestTeamName,
    hostTeamPlayersForm : new FormGroup({
      player1: this.player1,
      // player2: new FormControl(""),
      // player3: new FormControl(""),
      // player4: new FormControl(""),
    })
  });

  
  
  constructor() { }

  ngOnInit(): void {
  }

  onSubmit() {
    console.log(this.teamMatchConfiguration.value);
  }

}
