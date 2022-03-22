import { Component, OnInit } from '@angular/core';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { SignalrGeneralService } from 'src/app/core/services/signalr-general/signalr-general.service';
import { Room } from '../../models/room.model';

@Component({
  selector: 'app-lobby-page',
  templateUrl: './lobby-page.component.html',
  styleUrls: ['./lobby-page.component.scss'],
})
export class LobbyPageComponent implements OnInit {
  rooms: Room[] = [
    //   { roomId: 'asf', roomName: 'AAAAAAAAAA', numberOfPlayersJoined: 3 },
    //   { roomId: 'asf', roomName: 'Prva', numberOfPlayersJoined: 3 },
    //   { roomId: 'asf', roomName: 'Prva', numberOfPlayersJoined: 3 },
    //   { roomId: 'asf', roomName: 'Prva', numberOfPlayersJoined: 3 },
    //   { roomId: 'asf', roomName: 'Prva', numberOfPlayersJoined: 3 },
    //   { roomId: 'asf', roomName: 'Prva', numberOfPlayersJoined: 3 },
    //   { roomId: 'asf', roomName: 'Prva', numberOfPlayersJoined: 3 },
    //   { roomId: 'asf', roomName: 'Prva', numberOfPlayersJoined: 3 },
    //   { roomId: 'asf', roomName: 'Prva', numberOfPlayersJoined: 3 },
  ];
  roomObser: BehaviorSubject<Room[]> = new BehaviorSubject<Room[]>([]);
  constructor(private signalRService: SignalrGeneralService) {}

  ngOnInit(): void {
    this.signalRService.createConnection('/lobby');
    this.signalRService.startConnection();
    this.signalRService.addOnServerMethodHandler(
      { methodName: 'UpdateRooms', args: [] },
      (a) => this.roomObser.next(a)
    );
  }

  onCreateRoom() {
    this.signalRService.sendMessageToServer({
      methodName: 'CreateRoom',
      args: [],
    });
  }

  onRandomJoin() {
    this.signalRService.sendMessageToServer({
      methodName: 'GetRooms',
      args: [],
    });
  }
}
