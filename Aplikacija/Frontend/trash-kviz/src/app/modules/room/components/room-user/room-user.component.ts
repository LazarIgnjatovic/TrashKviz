import { Component, Input, OnInit, Output } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from 'src/app/modules/user/models/user.model';
import { EventEmitter } from '@angular/core';
import { UserRoom } from '../../models/user-room.model';

@Component({
  selector: 'app-room-user',
  templateUrl: './room-user.component.html',
  styleUrls: ['./room-user.component.scss'],
})
export class RoomUserComponent implements OnInit {
  @Input() userRoom!: UserRoom;
  @Input() showKick!: Observable<boolean>;
  @Output() userKicked: EventEmitter<any> = new EventEmitter();
  constructor() {}

  ngOnInit(): void {}

  scaleWinrate() {
    if (this.userRoom.user.stats.winrate > 100) return 100;
    if (this.userRoom.user.stats.winrate < 0) return 0;
    return (this.userRoom.user.stats.winrate * 100).toFixed(2);
  }

  scaleExperience() {
    if (this.userRoom.user.stats.gamesPlayed < 0) return '0';
    if (this.userRoom.user.stats.gamesPlayed > 1000) return '1000+';
    return this.userRoom.user.stats.gamesPlayed.toString();
  }
}
