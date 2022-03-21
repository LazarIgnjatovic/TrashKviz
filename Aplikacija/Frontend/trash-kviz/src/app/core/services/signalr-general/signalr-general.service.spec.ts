import { TestBed } from '@angular/core/testing';

import { SignalrGeneralService } from './signalr-general.service';

describe('SignalrGeneralService', () => {
  let service: SignalrGeneralService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SignalrGeneralService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
