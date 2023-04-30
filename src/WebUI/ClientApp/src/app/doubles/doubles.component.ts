import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Players, WizardService } from '../wizard.service';

@Component({
  selector: 'app-doubles',
  templateUrl: './doubles.component.html',
  styleUrls: ['./doubles.component.scss']
})
export class DoublesComponent implements OnInit {

  selectHostPlayers: SelectPlayer[];
  selectGuestPlayers: SelectPlayer[];

  selectedFirstDoubleHost: string[]; 
  selectedSecondDoubleHost: string[]; 

  hostDoubles: Doubles;
  guestDoubles: Doubles;

  submitted: boolean = false;

  constructor(private router: Router, private wizardService: WizardService) { }

  ngOnInit(): void {
    // this.hostDoubles = this.wizardService.getHostDoubles();
    var hostPlayers = this.wizardService.getHostPlayers();

    this.selectHostPlayers = [{
      label: hostPlayers.player1,
      value: hostPlayers.player1
    },{
      label: hostPlayers.player2,
      value: hostPlayers.player2
    },{
      label: hostPlayers.player3,
      value: hostPlayers.player3
    },{
      label: hostPlayers.player4,
      value: hostPlayers.player4
    }]
  }

  nextPage(): void{
    if (this.hostDoubles.firstDouble.player1 && this.hostDoubles.firstDouble.player2 &&
        this.hostDoubles.secondDouble.player1 && this.hostDoubles.secondDouble.player2 ){
      this.wizardService.setDoubles(this.hostDoubles, this.guestDoubles);
      this.router.navigate(['start-wizard/resumes']);
      return;
  }

  this.submitted = true;
  }
}
export class Doubles{
  firstDouble: DoubleCouple = new DoubleCouple()
  secondDouble: DoubleCouple = new DoubleCouple()
}

export class DoubleCouple{
  player1: string = ""
  player2: string = ""
}

export interface SelectPlayer{
  label: string
  value: string 
}