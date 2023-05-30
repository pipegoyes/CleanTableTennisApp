import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SetScoreComponent } from './set-score.component';

describe('SetScoreComponent', () => {
  let component: SetScoreComponent;
  let fixture: ComponentFixture<SetScoreComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SetScoreComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SetScoreComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
