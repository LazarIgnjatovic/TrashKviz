import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClassicListComponent } from './classic-list.component';

describe('ClassicListComponent', () => {
  let component: ClassicListComponent;
  let fixture: ComponentFixture<ClassicListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ClassicListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ClassicListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
