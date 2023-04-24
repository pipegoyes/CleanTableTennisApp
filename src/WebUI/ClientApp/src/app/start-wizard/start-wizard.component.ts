import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-start-wizard',
  templateUrl: './start-wizard.component.html',
  styleUrls: ['./start-wizard.component.scss']
})
export class StartWizardComponent implements OnInit {

  items: MenuItem[];

  teamInformation : any;

  constructor() { }

  ngOnInit(): void {
    this.teamInformation = {
      hostName: ""
    };

    this.items = [{
        label: 'Team Information',
        routerLink: 'start-wizard'
    },
    {
        label: 'Host players',
        routerLink: 'host-players'
    },
  ];

  }

  nextPage() {
    if (this.teamInformation.hostName) {
        // this.ticketService.ticketInformation.personalInformation = this.personalInformation;
        // this.router.navigate(['host-players']);
        return;
    }

    // this.submitted = true;
}

}
