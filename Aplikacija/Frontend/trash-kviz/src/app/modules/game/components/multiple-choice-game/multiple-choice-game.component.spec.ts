import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MultipleChoiceGameComponent } from './multiple-choice-game.component';

describe('MultipleChoiceGameComponent', () => {
  let component: MultipleChoiceGameComponent;
  let fixture: ComponentFixture<MultipleChoiceGameComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MultipleChoiceGameComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MultipleChoiceGameComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
