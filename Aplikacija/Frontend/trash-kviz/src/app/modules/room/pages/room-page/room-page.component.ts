import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { RoomState } from '../../models/room-state.model';
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

  getAllReady() {
    return this.roomService.getAllReady();
  }

  onLeaveRoom() {
    this.roomService.leaveRoom();
  }

  onMarkReady() {
    this.roomService.markReady();
  }

  onStartGame() {
    this.roomService.startGame();
  }

  onSettingsChanged(roomState: RoomState) {
    this.roomService.modifyRoom(roomState);
  }

  onUserKicked(username: string) {
    this.roomService.kickPlayer(username);
  }
}
