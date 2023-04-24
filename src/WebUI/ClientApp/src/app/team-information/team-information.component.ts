import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-team-information',
  templateUrl: './team-information.component.html',
  styleUrls: ['./team-information.component.scss']
})
export class TeamInformationComponent implements OnInit {

  teamInformation: any;

  constructor() { }

  ngOnInit(): void {
    this.teamInformation = {
      hostName: ""
    };
  }

}
