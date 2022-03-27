import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ClosestNumberGameComponent } from './closest-number-game.component';

describe('ClosestNumberGameComponent', () => {
  let component: ClosestNumberGameComponent;
  let fixture: ComponentFixture<ClosestNumberGameComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ClosestNumberGameComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ClosestNumberGameComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
