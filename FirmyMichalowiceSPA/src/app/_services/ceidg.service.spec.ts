import { TestBed } from '@angular/core/testing';

import { CEIDGService } from './ceidg.service';

describe('CEIDGService', () => {
  let service: CEIDGService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CEIDGService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
