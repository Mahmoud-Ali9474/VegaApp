/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { AppErrorHandler } from './AppErrorHandler.service';

describe('Service: AppErrorHandler', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AppErrorHandler],
    });
  });

  it('should ...', inject([AppErrorHandler], (service: AppErrorHandler) => {
    expect(service).toBeTruthy();
  }));
});
