/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { VehiclePhotosService } from './vehicle-Photos.service';

describe('Service: VehiclePhotos', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [VehiclePhotosService]
    });
  });

  it('should ...', inject([VehiclePhotosService], (service: VehiclePhotosService) => {
    expect(service).toBeTruthy();
  }));
});
