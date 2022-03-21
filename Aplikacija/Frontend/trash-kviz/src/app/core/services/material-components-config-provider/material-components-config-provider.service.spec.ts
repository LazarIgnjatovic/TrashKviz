import { TestBed } from '@angular/core/testing';

import { MaterialComponentsConfigProviderService } from './material-components-config-provider.service';

describe('MaterialCcomponentsConfigProviderService', () => {
  let service: MaterialComponentsConfigProviderService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MaterialComponentsConfigProviderService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
