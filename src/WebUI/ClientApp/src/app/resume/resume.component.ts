import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Players, WizardService } from '../wizard.service';

@Component({
  selector: 'app-resume',
  templateUrl: './resume.component.html',
  styleUrls: ['./resume.component.scss']
})
export class ResumeComponent implements OnInit {

  teamInformation: any;
  hostPlayers: Players;
  hostFirstDouble: string[];
  hostSecondDouble: string[];
  constructor(private router: Router, private wizardService: WizardService) { }

  ngOnInit(): void {
    this.teamInformation = this.wizardService.getTeamInformation();
    this.hostPlayers = this.wizardService.getHostPlayers();
    this.hostFirstDouble = this.wizardService.getHostFirstDouble();
    this.hostSecondDouble = this.wizardService.getHostSecondDouble();
  }

  send(): void {
    this.wizardService.saveTeamMatch();
  }

  prevPage(): void {
    this.router.navigate(['start-wizard/doubles']);
  }

}
