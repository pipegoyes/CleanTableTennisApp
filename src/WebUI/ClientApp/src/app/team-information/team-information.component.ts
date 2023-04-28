import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { WizardService } from '../wizard.service';
@Component({
  selector: 'app-team-information',
  templateUrl: './team-information.component.html',
  styleUrls: ['./team-information.component.scss']
})
export class TeamInformationComponent implements OnInit {

  teamInformation: TeamInformation;

  submitted: boolean = false;

  constructor(private router: Router, private wizardService: WizardService) { }

  ngOnInit(): void {
    this.teamInformation = this.wizardService.getTeamInformation();
  }

  nextPage(): void{
    if (this.teamInformation.hostName && this.teamInformation.guestName){
      this.wizardService.setTeamInformation(this.teamInformation);
      this.router.navigate(['start-wizard/host-players']);
      return;
  }

  this.submitted = true;
  }

}
export class TeamInformation{
  hostName: string = ""
  guestName: string = ""
}
