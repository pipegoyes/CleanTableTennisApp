import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GuestPlayersComponent } from './guest-players.component';

describe('GuestPlayersComponent', () => {
  let component: GuestPlayersComponent;
  let fixture: ComponentFixture<GuestPlayersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GuestPlayersComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GuestPlayersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
