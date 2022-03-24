import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, concatMap, switchMap } from 'rxjs';
import { SignalrGeneralService } from 'src/app/core/services/signalr-general/signalr-general.service';
import { RoomState } from '../../models/room-state.model';
import { UserRoom } from '../../models/user-room.model';

@Injectable()
export class RoomService {
  private isAdmin: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(
    false
  );
  private allReady: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(
    false
  );

  private roomState: BehaviorSubject<RoomState> =
    new BehaviorSubject<RoomState>({
      game1: 0,
      game2: 0,
      game3: 0,
      isPublic: true,
      roomId: '',
      roomName: '',
      users: [],
    });

  constructor(
    private signalRService: SignalrGeneralService,
    private router: Router
  ) {}

  initConnection() {
    this.isAdmin.next(false);
    this.signalRService.createConnection('/room');

    this.addServerMethodHandlers();

    this.signalRService.startConnection();
  }

  private addServerMethodHandlers() {
    this.signalRService.addOnServerMethodHandler(
      {
        methodName: 'RoomUpdate',
        args: [],
      },
      (room: RoomState) => this.roomState.next(room)
    );

    this.signalRService.addOnServerMethodHandler(
      {
        methodName: 'PromoteToAdmin',
        args: [],
      },
      () => this.isAdmin.next(true)
    );

    this.signalRService.addOnServerMethodHandler(
      {
        methodName: 'RoomFull',
        args: [],
      },
      () => {
        this.signalRService
          .endConnection()
          .subscribe((_) => this.router.navigate(['/lobby']));
      }
    );

    this.signalRService.addOnServerMethodHandler(
      {
        methodName: 'Kicked',
        args: [],
      },
      () => {
        this.signalRService
          .endConnection()
          .subscribe((_) => this.router.navigate(['/lobby']));
      }
    );

    this.signalRService.addOnServerMethodHandler(
      {
        methodName: 'AllReady',
        args: [],
      },
      () => {
        this.allReady.next(true);
      }
    );

    this.signalRService.addOnServerMethodHandler(
      {
        methodName: 'NotAllReady',
        args: [],
      },
      () => {
        this.allReady.next(false);
      }
    );

    this.signalRService.addOnServerMethodHandler(
      {
        methodName: 'GameStarted',
        args: [],
      },
      () => {
        this.signalRService
          .endConnection()
          .subscribe((_) => this.router.navigate(['/game']));
      }
    );
  }

  getIsAdmin() {
    return this.isAdmin;
  }

  getRoomState() {
    return this.roomState;
  }

  getAllReady() {
    return this.allReady;
  }

  leaveRoom() {
    this.signalRService
      .invokeServerMethod({
        methodName: 'LeaveRoom',
        args: [this.roomState.value.roomId],
      })
      .pipe(concatMap((_) => this.signalRService.endConnection()))
      .subscribe((_) => this.router.navigate(['/lobby']));
  }

  markReady() {
    this.signalRService.sendMessageToServer({
      methodName: 'MarkReady',
      args: [this.roomState.value.roomId],
    });
  }

  modifyRoom(roomState: RoomState) {
    this.signalRService.sendMessageToServer({
      methodName: 'ModifyRoom',
      args: [roomState],
    });
  }

  kickPlayer(username: string) {
    this.signalRService.sendMessageToServer({
      methodName: 'Kick',
      args: [this.roomState.value.roomId, username],
    });
  }

  startGame() {
    console.log('STARTTT');
    this.signalRService.sendMessageToServer({
      methodName: 'StartGame',
      args: [this.roomState.value.roomId],
    });
  }
}
