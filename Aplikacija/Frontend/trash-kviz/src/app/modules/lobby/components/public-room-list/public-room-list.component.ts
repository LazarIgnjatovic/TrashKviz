import { Component, Input, OnInit } from '@angular/core';
import { Observable, of } from 'rxjs';
import { WindowResizeDetectorService } from 'src/app/core/services/window-resize-detector/window-resize-detector.service';
import { Room } from '../../models/room.model';

@Component({
  selector: 'app-public-room-list',
  templateUrl: './public-room-list.component.html',
  styleUrls: ['./public-room-list.component.scss'],
})
export class PublicRoomListComponent implements OnInit {
  @Input() rooms: Observable<Room[]> = of([]);
  constructor(
    public windowResizeDetectionService: WindowResizeDetectorService
  ) {}

  ngOnInit(): void {}

  onSelect() {}
}
