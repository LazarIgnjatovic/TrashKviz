import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LobbyButtonGroupComponent } from './lobby-button-group.component';

describe('LobbyButtonGroupComponent', () => {
  let component: LobbyButtonGroupComponent;
  let fixture: ComponentFixture<LobbyButtonGroupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ LobbyButtonGroupComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(LobbyButtonGroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
