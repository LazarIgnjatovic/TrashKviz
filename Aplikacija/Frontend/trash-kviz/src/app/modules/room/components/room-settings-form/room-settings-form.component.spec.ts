import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RoomSettingsFormComponent } from './room-settings-form.component';

describe('RoomSettingsFormComponent', () => {
  let component: RoomSettingsFormComponent;
  let fixture: ComponentFixture<RoomSettingsFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RoomSettingsFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RoomSettingsFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
