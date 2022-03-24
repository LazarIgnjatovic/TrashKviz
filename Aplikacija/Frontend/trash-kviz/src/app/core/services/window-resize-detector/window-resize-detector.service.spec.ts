import { TestBed } from '@angular/core/testing';

import { WindowResizeDetectorService } from './window-resize-detector.service';

describe('WindowResizeDetectorService', () => {
  let service: WindowResizeDetectorService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(WindowResizeDetectorService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
