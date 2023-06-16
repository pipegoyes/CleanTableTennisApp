import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OnGoingTeamMatchesComponent } from './on-going-team-matches.component';

describe('OnGoingTeamMatchesComponent', () => {
  let component: OnGoingTeamMatchesComponent;
  let fixture: ComponentFixture<OnGoingTeamMatchesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OnGoingTeamMatchesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OnGoingTeamMatchesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
