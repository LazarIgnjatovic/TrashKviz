import { Injectable, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { OverlayService } from 'src/app/core/services/overlay-service/overlay.service';
import { SignalrGeneralService } from 'src/app/core/services/signalr-general/signalr-general.service';
import { SpinnerComponent } from 'src/app/core/components/spinner/spinner.component';
import { Room } from '../models/room.model';

@Injectable()
export class LobbyService {
  private rooms: BehaviorSubject<Room[]> = new BehaviorSubject<Room[]>([]);
  private underSpinner!: TemplateRef<any>;
  constructor(
    private signalRService: SignalrGeneralService,
    private router: Router,
    private overlayService: OverlayService
  ) {}

  initConnection() {
    this.signalRService.createConnection('/lobby');

    this.addServerMethodHandlers();

    this.signalRService.addOnReconnectedHandler(() => this.getRooms());

    this.signalRService.startConnection().subscribe(() => this.getRooms());
  }

  private addServerMethodHandlers() {
    this.signalRService.addOnServerMethodHandler(
      { methodName: 'UpdateRooms', args: [] },
      (newRooms: Room[]) => this.rooms.next(newRooms)
    );

    this.signalRService.addOnServerMethodHandler(
      { methodName: 'RoomFull', args: [] },
      () => console.log('ROOM FULL')
    );

    this.signalRService.addOnServerMethodHandler(
      { methodName: 'RoomNotFound', args: [] },
      () => console.log('ROOMNOTFOUND')
    );

    this.signalRService.addOnServerMethodHandler(
      { methodName: 'RoomFound', args: [] },
      () => {
        this.signalRService.endConnection().subscribe(() => {
          this.overlayService.hide();
          this.router.navigate(['/room']);
        });
      }
    );

    this.signalRService.addOnServerMethodHandler(
      { methodName: 'Reconnect', args: [] },
      () => {
        this.signalRService.endConnection().subscribe(() => {
          this.router.navigate(['/game']);
        });
      }
    );
  }

  private getRooms() {
    this.signalRService.sendMessageToServer({
      methodName: 'GetRooms',
      args: [],
    });
  }

  findRoom() {
    this.signalRService
      .invokeServerMethod({
        methodName: 'FindRoom',
        args: [],
      })
      .subscribe(() => {
        this.overlayService.overlayConfig.backdropClass = 'dark-backdrop';
        this.overlayService.show(SpinnerComponent, {
          message: 'Traženje sobe',
          underSpinner: this.underSpinner,
        });
      });
  }

  createRoom() {
    this.signalRService.sendMessageToServer({
      methodName: 'CreateRoom',
      args: [],
    });
  }

  cancelSearch() {
    this.signalRService
      .invokeServerMethod({
        methodName: 'CancelSearch',
        args: [],
      })
      .subscribe(() => this.overlayService.hide());
  }

  joinRoom(code: string) {
    this.signalRService.sendMessageToServer({
      methodName: 'JoinRoom',
      args: [code],
    });
  }

  setUnderSpinner(underSpinner: TemplateRef<any>) {
    this.underSpinner = underSpinner;
  }

  getRoomsObservable() {
    return this.rooms;
  }
}
