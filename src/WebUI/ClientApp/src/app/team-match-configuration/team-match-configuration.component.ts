import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-team-match-configuration',
  templateUrl: './team-match-configuration.component.html',
  styleUrls: ['./team-match-configuration.component.scss']
})
export class TeamMatchConfigurationComponent implements OnInit {
  teamMatchForm = new FormGroup({
    hostTeamName: new FormControl("Host"),
    guestTeamName: new FormControl("Guest"),
    hostPlayers: this.builder.array([
      this.builder.control(""),
      this.builder.control(""),
      this.builder.control(""),
      this.builder.control(""),
    ]),
    guestPlayers: this.builder.array([
      this.builder.control(""),
      this.builder.control(""),
      this.builder.control(""),
      this.builder.control(""),
    ]),
  });

  get getHostPlayers(){
    return this.teamMatchForm.get("hostPlayers") as FormArray;
  }
  
  get getGuestPlayers(){
    return this.teamMatchForm.get("guestPlayers") as FormArray;
  }

  constructor(private builder : FormBuilder) { }

  ngOnInit(): void {
  }

  onSubmit() {
    console.log(this.teamMatchForm.value);
  }

}
