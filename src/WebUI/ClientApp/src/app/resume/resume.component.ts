import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { WizardService } from '../wizard.service';
import { Players } from "../models/Players";

@Component({
  selector: 'app-resume',
  templateUrl: './resume.component.html',
  styleUrls: ['./resume.component.scss']
})
export class ResumeComponent implements OnInit {

  teamInformation: any;
  
  hostPlayers: Players;
  guestPlayers: Players;

  guestFirstDouble: string[];
  guestSecondDouble: string[];

  hostFirstDouble: string[];
  hostSecondDouble: string[];

  constructor(private router: Router, private wizardService: WizardService) { }

  ngOnInit(): void {
    this.teamInformation = this.wizardService.getTeamInformation();
    
    this.hostPlayers = this.wizardService.getHostPlayers();
    this.guestPlayers = this.wizardService.getGuestPlayers();

    this.hostFirstDouble = this.wizardService.getHostFirstDouble();
    this.hostSecondDouble = this.wizardService.getHostSecondDouble();

    this.guestFirstDouble = this.wizardService.getGuestFirstDouble();
    this.guestSecondDouble = this.wizardService.getGuestSecondDouble();
  }

  send(): void {
    this.wizardService.saveTeamMatch();
  }

  prevPage(): void {
    this.router.navigate(['start-wizard/doubles']);
  }

}
