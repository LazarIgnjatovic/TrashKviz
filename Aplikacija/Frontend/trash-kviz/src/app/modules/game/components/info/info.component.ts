import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { BehaviorSubject, map, Observable, Subscription } from 'rxjs';
import { GameState } from '../../models/game-state.model';
import { InfoState } from '../../models/info-state.model';

@Component({
  selector: 'app-info',
  templateUrl: './info.component.html',
  styleUrls: ['./info.component.scss'],
})
export class InfoComponent implements OnInit, OnDestroy {
  @Input() timerTick!: BehaviorSubject<void>;
  @Input() gameState!: Observable<GameState>;
  timerValue: number = 0;
  infoText: string = '';
  private timerTickSubscription!: Subscription;
  private gameStateSubscription!: Subscription;

  constructor() {}
  ngOnDestroy(): void {
    this.timerTickSubscription.unsubscribe();
    this.gameStateSubscription.unsubscribe();
  }

  ngOnInit(): void {
    this.timerTickSubscription = this.timerTick.subscribe(() => {
      if (this.timerValue > 0) {
        this.timerValue--;
      }
    });

    this.gameStateSubscription = this.gameState
      .pipe(map((gameState) => gameState as InfoState))
      .subscribe((gameState) => {
        this.infoText = gameState.infoText;
        this.timerValue = gameState.timerValue;
        console.log(gameState);
      });
  }
}
