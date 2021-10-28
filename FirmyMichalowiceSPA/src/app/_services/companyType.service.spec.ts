/* tslint:disable:no-unused-variable */

import { TestBed, inject, waitForAsync } from '@angular/core/testing';
import { CompanyTypeService } from './companyType.service';

describe('Service: CompanyType', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CompanyTypeService]
    });
  });

  it('should ...', inject([CompanyTypeService], (service: CompanyTypeService) => {
    expect(service).toBeTruthy();
  }));
});
