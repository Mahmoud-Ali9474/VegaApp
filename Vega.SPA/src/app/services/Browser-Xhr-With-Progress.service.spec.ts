/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { BrowserXhrWithProgressService } from './Browser-Xhr-With-Progress.service';

describe('Service: BrowserXhrWithProgress', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [BrowserXhrWithProgressService]
    });
  });

  it('should ...', inject([BrowserXhrWithProgressService], (service: BrowserXhrWithProgressService) => {
    expect(service).toBeTruthy();
  }));
});
