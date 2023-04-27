import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
@Component({
  selector: 'app-team-information',
  templateUrl: './team-information.component.html',
  styleUrls: ['./team-information.component.scss']
})
export class TeamInformationComponent implements OnInit {

  teamInformation: any;

  submitted: boolean = false;

  constructor(private router: Router) { }

  ngOnInit(): void {
    this.teamInformation = {
      hostName: "",
      guestName: ""
    };
  }

  nextPage(): void{
    if (this.teamInformation.hostName && this.teamInformation.guestName){
      this.router.navigate(['start-wizard/host-players']);
      return;
  }

  this.submitted = true;
  }

}
