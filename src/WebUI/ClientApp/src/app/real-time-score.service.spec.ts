import { TestBed } from '@angular/core/testing';

import { RealTimeScoreService } from './real-time-score.service';

describe('RealTimeScoreService', () => {
  let service: RealTimeScoreService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RealTimeScoreService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
