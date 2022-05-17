import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PlayersConfigComponent } from './players-config.component';

describe('PlayersConfigComponent', () => {
  let component: PlayersConfigComponent;
  let fixture: ComponentFixture<PlayersConfigComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PlayersConfigComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PlayersConfigComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
