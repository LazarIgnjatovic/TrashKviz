import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MultipleChoiceColumnComponent } from './association-column.component';

describe('MultipleChoiceColumnComponent', () => {
  let component: MultipleChoiceColumnComponent;
  let fixture: ComponentFixture<MultipleChoiceColumnComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MultipleChoiceColumnComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MultipleChoiceColumnComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
