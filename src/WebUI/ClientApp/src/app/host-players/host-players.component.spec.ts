import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HostPlayersComponent } from './host-players.component';

describe('HostPlayersComponent', () => {
  let component: HostPlayersComponent;
  let fixture: ComponentFixture<HostPlayersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HostPlayersComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HostPlayersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
