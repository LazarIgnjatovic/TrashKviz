import {
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import { BehaviorSubject, map, Observable, Subscription } from 'rxjs';
import { AssociationAnswer } from '../../models/association-answer.model';
import { AssociationState } from '../../models/association-state.model';
import { GameState } from '../../models/game-state.model';

@Component({
  selector: 'app-association-game',
  templateUrl: './association-game.component.html',
  styleUrls: ['./association-game.component.scss'],
})
export class AssociationGameComponent implements OnInit, OnDestroy {
  @Input() gameState!: Observable<GameState>;
  @Output() fieldClicked: EventEmitter<AssociationAnswer> = new EventEmitter();
  @Input() onTurn!: BehaviorSubject<boolean>;
  gameStateSubscription!: Subscription;
  finalAnswerValue: string = '';
  finalAnswerDisabled: boolean = false;
  constructor() {}
  ngOnDestroy(): void {
    this.gameStateSubscription.unsubscribe();
  }

  ngOnInit(): void {
    this.gameStateSubscription = this.gameState
      .pipe(map((state) => state as AssociationState))
      .subscribe((gameState) => {
        if (gameState.finalAnswer !== null) {
          this.finalAnswerValue = gameState.finalAnswer;
          this.finalAnswerDisabled = true;
        } else if (gameState.finalAnswer === null) {
          this.finalAnswerValue = '';
          this.finalAnswerDisabled = false;
        }
      });
  }

  getFinalAnswer() {
    return {
      text: this.finalAnswerValue,
      isColumnAnswer: false,
      isFinalAnswer: true,
      column: 0,
      field: 0,
    };
  }
}
