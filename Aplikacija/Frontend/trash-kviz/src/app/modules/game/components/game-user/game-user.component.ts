import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { BehaviorSubject, Observable, Subscription } from 'rxjs';
import { Player } from '../../models/player.model';

@Component({
  selector: 'app-game-user',
  templateUrl: './game-user.component.html',
  styleUrls: ['./game-user.component.scss'],
})
export class GameUserComponent implements OnInit, OnDestroy {
  @Input() onTurn!: Observable<boolean>;
  @Input() player!: Player;
  @Input() showTimer!: Observable<boolean>;
  @Input() timerLeft: boolean = true;
  @Input() timerTick!: BehaviorSubject<void>;
  @Input() timerSync!: Observable<number>;
  timerValue: number = 0;
  outline: string = 'none';
  onTurnSubscription!: Subscription;
  onTimerTickSubscription!: Subscription;
  onTimerSyncSubscription!: Subscription;
  constructor() {}
  ngOnDestroy(): void {
    this.onTurnSubscription.unsubscribe();
    this.onTimerTickSubscription.unsubscribe();
    this.onTimerSyncSubscription.unsubscribe();
  }
  ngOnInit(): void {
    this.onTurnSubscription = this.onTurn.subscribe(
      (onTurn) =>
        (this.outline = onTurn ? '2px solid rgba(105, 240, 174, 255)' : 'none')
    );

    this.onTimerTickSubscription = this.timerTick.subscribe(() => {
      if (this.timerValue > 0) this.timerValue--;
    });

    this.onTimerSyncSubscription = this.timerSync.subscribe((timerValue) => {
      this.timerValue = timerValue;
    });
  }
}
