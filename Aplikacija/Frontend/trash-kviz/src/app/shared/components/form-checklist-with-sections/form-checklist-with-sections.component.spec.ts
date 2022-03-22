import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FormChecklistWithSectionsComponent } from './form-checklist-with-sections.component';

describe('FormChecklistWithSectionsComponent', () => {
  let component: FormChecklistWithSectionsComponent;
  let fixture: ComponentFixture<FormChecklistWithSectionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ FormChecklistWithSectionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(FormChecklistWithSectionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
