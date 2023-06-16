import { TestBed } from '@angular/core/testing';

import { DoubleScoreService } from './double-score.service';

describe('DoubleScoreService', () => {
  let service: DoubleScoreService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DoubleScoreService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
