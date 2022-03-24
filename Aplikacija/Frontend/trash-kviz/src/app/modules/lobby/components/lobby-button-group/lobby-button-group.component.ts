import { Component, EventEmitter, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-lobby-button-group',
  templateUrl: './lobby-button-group.component.html',
  styleUrls: ['./lobby-button-group.component.scss'],
})
export class LobbyButtonGroupComponent implements OnInit {
  @Output() onRandomJoin: EventEmitter<any> = new EventEmitter();
  @Output() onCreateRoom: EventEmitter<any> = new EventEmitter();

  constructor() {}

  ngOnInit(): void {}
}
