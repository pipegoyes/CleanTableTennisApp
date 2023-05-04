import { TestBed } from '@angular/core/testing';

import { TeamMatchService } from './team-match.service';

describe('TeamMatchService', () => {
  let service: TeamMatchService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TeamMatchService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
