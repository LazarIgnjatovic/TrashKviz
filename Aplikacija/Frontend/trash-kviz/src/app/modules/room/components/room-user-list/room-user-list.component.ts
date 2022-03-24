import { Component, Input, OnInit, Output } from '@angular/core';
import { BehaviorSubject, Observable, of, Subject } from 'rxjs';
import { EventEmitter } from '@angular/core';
import { UserRoom } from '../../models/user-room.model';

@Component({
  selector: 'app-room-user-list',
  templateUrl: './room-user-list.component.html',
  styleUrls: ['./room-user-list.component.scss'],
})
export class RoomUserListComponent implements OnInit {
  @Input() users: UserRoom[] = [];
  @Input() isAdmin!: Observable<boolean>;
  @Output() userKicked: EventEmitter<any> = new EventEmitter();
  constructor() {}

  ngOnInit(): void {}

  showKick(index: number) {
    if (index == 0) return of(false);
    return this.isAdmin;
  }
}
