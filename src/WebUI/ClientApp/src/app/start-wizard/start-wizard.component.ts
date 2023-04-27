import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-start-wizard',
  templateUrl: './start-wizard.component.html',
  styleUrls: ['./start-wizard.component.scss']
})
export class StartWizardComponent implements OnInit {

  items: MenuItem[];

  constructor() { }

  ngOnInit(): void {
    this.items = [{
        label: 'Teams',
        routerLink: 'start-wizard'
    },
    {
        label: 'Host Players',
        routerLink: 'host-players'
    },
    {
        label: 'Guest Players',
        routerLink: 'players'
    },
  ];

  }
}
