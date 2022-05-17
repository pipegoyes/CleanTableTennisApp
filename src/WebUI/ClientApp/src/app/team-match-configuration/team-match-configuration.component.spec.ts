import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TeamMatchConfigurationComponent } from './team-match-configuration.component';

describe('TeamMatchConfigurationComponent', () => {
  let component: TeamMatchConfigurationComponent;
  let fixture: ComponentFixture<TeamMatchConfigurationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TeamMatchConfigurationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TeamMatchConfigurationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
