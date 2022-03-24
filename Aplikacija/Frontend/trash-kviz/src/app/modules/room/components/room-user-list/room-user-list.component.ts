import { Component, Input, OnInit } from '@angular/core';
import { BehaviorSubject, Observable, of, Subject } from 'rxjs';
import { User } from 'src/app/modules/user/models/user.model';
import { UserRoom } from '../../models/user-room.model';

@Component({
  selector: 'app-room-user-list',
  templateUrl: './room-user-list.component.html',
  styleUrls: ['./room-user-list.component.scss'],
})
export class RoomUserListComponent implements OnInit {
  @Input() users: UserRoom[] = []; //Observable<UserRoom[]> = of([]);
  @Input() isAdmin!: Observable<boolean>;
  constructor() {}

  ngOnInit(): void {}

  showKick(index: number) {
    if (index == 0) return of(false);
    return this.isAdmin;
  }
}
