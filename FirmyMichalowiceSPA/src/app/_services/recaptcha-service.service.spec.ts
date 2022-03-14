import { TestBed } from '@angular/core/testing';

import { RecaptchaService } from './recaptcha-service.service';

describe('RecaptchaServiceService', () => {
  let service: RecaptchaService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RecaptchaService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
