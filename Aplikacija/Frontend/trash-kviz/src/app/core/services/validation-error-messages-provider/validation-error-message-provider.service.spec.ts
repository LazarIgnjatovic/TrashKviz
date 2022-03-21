import { TestBed } from '@angular/core/testing';

import { ValidationErrorMessageProviderService } from './validation-error-message-provider.service';

describe('ValidationErrorMessageProviderService', () => {
  let service: ValidationErrorMessageProviderService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ValidationErrorMessageProviderService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
