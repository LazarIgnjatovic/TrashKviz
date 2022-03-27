import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StepByStepGameComponent } from './step-by-step-game.component';

describe('StepByStepGameComponent', () => {
  let component: StepByStepGameComponent;
  let fixture: ComponentFixture<StepByStepGameComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StepByStepGameComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StepByStepGameComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
