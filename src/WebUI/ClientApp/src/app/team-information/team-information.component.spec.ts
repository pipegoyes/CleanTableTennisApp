import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TeamInformationComponent } from './team-information.component';

describe('TeamInformationComponent', () => {
  let component: TeamInformationComponent;
  let fixture: ComponentFixture<TeamInformationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TeamInformationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TeamInformationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
