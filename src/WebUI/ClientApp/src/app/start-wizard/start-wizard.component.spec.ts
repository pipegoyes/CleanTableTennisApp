import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StartWizardComponent } from './start-wizard.component';

describe('StartWizardComponent', () => {
  let component: StartWizardComponent;
  let fixture: ComponentFixture<StartWizardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StartWizardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StartWizardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
