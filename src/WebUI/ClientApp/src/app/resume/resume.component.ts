import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { WizardService } from '../wizard.service';

@Component({
  selector: 'app-resume',
  templateUrl: './resume.component.html',
  styleUrls: ['./resume.component.scss']
})
export class ResumeComponent implements OnInit {

  constructor(private router: Router, private wizardService: WizardService) { }

  ngOnInit(): void {
  }

  send(): void {
    this.wizardService.saveTeamMatch();
  }

  prevPage(): void {
    this.router.navigate(['start-wizard/doubles']);
  }

}
