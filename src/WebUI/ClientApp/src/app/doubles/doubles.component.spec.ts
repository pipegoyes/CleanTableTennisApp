import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DoublesComponent } from './doubles.component';

describe('DoublesComponent', () => {
  let component: DoublesComponent;
  let fixture: ComponentFixture<DoublesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DoublesComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DoublesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
