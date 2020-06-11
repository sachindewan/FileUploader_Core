/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { ErrorInterceptorService } from './error.interceptor.service';

describe('Service: Error.interceptor', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ErrorInterceptorService],
    });
  });

  it('should ...', inject(
    [ErrorInterceptorService],
    (service: ErrorInterceptorService) => {
      expect(service).toBeTruthy();
    }
  ));
});
