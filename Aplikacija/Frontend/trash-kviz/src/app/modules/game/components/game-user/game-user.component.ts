import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { Player } from '../../models/player.model';

@Component({
  selector: 'app-game-user',
  templateUrl: './game-user.component.html',
  styleUrls: ['./game-user.component.scss'],
})
export class GameUserComponent implements OnInit, OnDestroy {
  @Input() onTurn!: Observable<boolean>;
  @Input() player!: Player;
  outline: string = 'none';
  onTurnSubscription!: Subscription;

  constructor() {}
  ngOnDestroy(): void {
    this.onTurnSubscription.unsubscribe();
  }
  ngOnInit(): void {
    this.onTurnSubscription = this.onTurn.subscribe(
      (onTurn) => (this.outline = onTurn ? '2px solid rgba(105, 240, 174, 255)' : 'none')
    );
  }
}
