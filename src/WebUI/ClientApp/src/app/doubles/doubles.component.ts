import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { WizardService } from '../wizard.service';

@Component({
  selector: 'app-doubles',
  templateUrl: './doubles.component.html',
  styleUrls: ['./doubles.component.scss']
})
export class DoublesComponent implements OnInit {

  hostDoubles: Doubles;
  guestDoubles: Doubles;

  submitted: boolean = false;

  constructor(private router: Router, private wizardService: WizardService) { }

  ngOnInit(): void {
    this.hostDoubles = this.wizardService.getHostDoubles();
  }

  nextPage(): void{
    if (this.hostDoubles.firstDouble.player1 && this.hostDoubles.firstDouble.player2 &&
        this.hostDoubles.secondDouble.player1 && this.hostDoubles.secondDouble.player2 ){
      this.wizardService.setDoubles(this.hostDoubles, this.guestDoubles);
      this.router.navigate(['start-wizard/host-players']);
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