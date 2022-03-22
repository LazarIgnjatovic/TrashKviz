import { TestBed } from '@angular/core/testing';

import { RegexProviderService } from './regex-provider.service';

describe('RegexProviderService', () => {
  let service: RegexProviderService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RegexProviderService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
