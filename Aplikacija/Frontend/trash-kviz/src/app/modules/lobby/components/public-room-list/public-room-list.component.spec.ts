import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PublicRoomListComponent } from './public-room-list.component';

describe('PublicRoomListComponent', () => {
  let component: PublicRoomListComponent;
  let fixture: ComponentFixture<PublicRoomListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PublicRoomListComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PublicRoomListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
