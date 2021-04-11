/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { UploadPhotoService } from './uploadPhoto.service';

describe('Service: UploadPhoto', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [UploadPhotoService]
    });
  });

  it('should ...', inject([UploadPhotoService], (service: UploadPhotoService) => {
    expect(service).toBeTruthy();
  }));
});
