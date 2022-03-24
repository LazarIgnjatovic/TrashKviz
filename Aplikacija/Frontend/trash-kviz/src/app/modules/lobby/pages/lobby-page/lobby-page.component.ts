import {
  AfterViewInit,
  Component,
  OnInit,
  TemplateRef,
  ViewChild,
} from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Room } from '../../models/room.model';
import { LobbyService } from '../../services/lobby.service';

@Component({
  selector: 'app-lobby-page',
  templateUrl: './lobby-page.component.html',
  styleUrls: ['./lobby-page.component.scss'],
})
export class LobbyPageComponent implements OnInit, AfterViewInit {
  @ViewChild('underSpinner') underSpinner!: TemplateRef<any>;

  constructor(private lobbyService: LobbyService) {}
  ngAfterViewInit(): void {
    this.lobbyService.setUnderSpinner(this.underSpinner);
  }

  ngOnInit(): void {
    this.lobbyService.initConnection();
  }

  getRoomsObservable(): BehaviorSubject<Room[]> {
    return this.lobbyService.getRoomsObservable();
  }

  onRandomJoin() {
    this.lobbyService.findRoom();
  }

  onCodeJoin(code: string) {
    this.lobbyService.joinRoom(code.toUpperCase());
  }

  onCreateRoom() {
    this.lobbyService.createRoom();
  }

  onSearchCanceled() {
    this.lobbyService.cancelSearch();
  }
}
