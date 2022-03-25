import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AssociationGameComponent } from './association-game.component';

describe('AssociationGameComponent', () => {
  let component: AssociationGameComponent;
  let fixture: ComponentFixture<AssociationGameComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AssociationGameComponent],
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AssociationGameComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
