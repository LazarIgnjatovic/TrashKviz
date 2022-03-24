import { Component, OnInit } from '@angular/core';
import { RoomService } from '../../service/room-service/room.service';

@Component({
  selector: 'app-room-page',
  templateUrl: './room-page.component.html',
  styleUrls: ['./room-page.component.scss'],
})
export class RoomPageComponent implements OnInit {
  constructor(private roomService: RoomService) {}

  ngOnInit(): void {
    this.roomService.initConnection();
  }

  isAdmin() {
    return this.roomService.getIsAdmin();
  }

  getRoomState() {
    return this.roomService.getRoomState();
  }

  onLeaveRoom() {
    this.roomService.leaveRoom();
  }

  onMarkReady() {
    this.roomService.markReady();
  }
}
