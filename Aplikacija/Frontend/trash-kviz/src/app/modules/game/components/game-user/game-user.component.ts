import {
  animate,
  state,
  style,
  transition,
  trigger,
} from '@angular/animations';
import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { BehaviorSubject, Observable, Subscription } from 'rxjs';
import { slideInOutWithFade } from 'src/app/core/animations/animations';
import { Player } from '../../models/player.model';

@Component({
  selector: 'app-game-user',
  templateUrl: './game-user.component.html',
  styleUrls: ['./game-user.component.scss'],
  animations: [],
})
export class GameUserComponent implements OnInit, OnDestroy {
  @Input() onTurn: Observable<boolean> = new Observable<boolean>();
  @Input() onOutline: Observable<number> = new Observable<number>();
  @Input() onDisconnect: Observable<boolean> = new Observable<boolean>();
  @Input() player!: Player;
  @Input() showTimer: Observable<boolean> = new Observable<boolean>();
  @Input() timerLeft: boolean = true;
  @Input() timerTick: BehaviorSubject<void> = new BehaviorSubject<void>(
    undefined
  );
  @Input() timerSync: Observable<number> = new Observable<number>();
  timerValue: number = 0;
  outline: string = 'none';

  icon: string = 'account_circle';
  onTurnSubscription!: Subscription;
  onDisconnectSubscription!: Subscription;
  onOutlineSubscription!: Subscription;
  onTimerTickSubscription!: Subscription;
  onTimerSyncSubscription!: Subscription;
  constructor() {}
  ngOnDestroy(): void {
    this.onOutlineSubscription.unsubscribe();
    this.onDisconnectSubscription.unsubscribe();
    this.onTurnSubscription.unsubscribe();
    this.onTimerTickSubscription.unsubscribe();
    this.onTimerSyncSubscription.unsubscribe();
  }
  ngOnInit(): void {
    this.onTurnSubscription = this.onTurn.subscribe(
      (onTurn) =>
        (this.outline = onTurn ? '2px solid rgba(105, 240, 174, 255)' : 'none')
    );

    this.onOutlineSubscription = this.onOutline.subscribe((onOutline) => {
      switch (onOutline) {
        case 0:
          this.outline = '2px solid green';
          break;
        case 1:
          this.outline = '2px solid yellow';
          break;
        default:
          break;
      }
    });

    this.onDisconnectSubscription = this.onDisconnect.subscribe(
      // (disconnected) =>
      //   (this.icon = disconnected ? 'wifi_off' : 'account_circle')
    );
    this.onTimerTickSubscription = this.timerTick.subscribe(() => {
      if (this.timerValue > 0) this.timerValue--;
    });

    this.onTimerSyncSubscription = this.timerSync.subscribe((timerValue) => {
      this.timerValue = timerValue;
    });
  }
}
