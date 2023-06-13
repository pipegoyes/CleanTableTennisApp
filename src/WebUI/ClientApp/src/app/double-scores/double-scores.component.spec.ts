import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DoubleScoresComponent } from './double-scores.component';

describe('DoubleScoresComponent', () => {
  let component: DoubleScoresComponent;
  let fixture: ComponentFixture<DoubleScoresComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DoubleScoresComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DoubleScoresComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
