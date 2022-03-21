import { TestBed } from '@angular/core/testing';

import { FormInputTypesProviderService } from './form-input-types-provider.service';

describe('FormInputTypesProviderService', () => {
  let service: FormInputTypesProviderService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(FormInputTypesProviderService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
