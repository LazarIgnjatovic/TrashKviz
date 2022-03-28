import {
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import { BehaviorSubject, filter, map, Observable, Subscription } from 'rxjs';
import { AssociationAnswer } from '../../models/association-answer.model';
import { AssociationState } from '../../models/association-state.model';
import { GameState } from '../../models/game-state.model';
import { BaseGameComponent } from '../base-game/base-game.component';

@Component({
  selector: 'app-association-game',
  templateUrl: './association-game.component.html',
  styleUrls: ['./association-game.component.scss'],
})
export class AssociationGameComponent
  extends BaseGameComponent<AssociationAnswer>
  implements OnInit
{
  @Input() onTurn!: BehaviorSubject<boolean>;

  finalAnswerValue: string = '';
  associationAnswered: boolean = false;
  finalAnswerDisabled: boolean = false;
  constructor() {
    super();
  }

  ngOnInit(): void {
    this.gameStateSubscription = this.gameState
      .pipe(map((state) => state as AssociationState))
      .subscribe((gameState) => {
        if (gameState.finalAnswer !== null) {
          if (gameState.finalAnswer.startsWith('#')) {
            this.finalAnswerValue = gameState.finalAnswer.slice(1);
            this.associationAnswered = false;
          } else {
            this.finalAnswerValue = gameState.finalAnswer;
            this.associationAnswered = true;
          }
        } else if (gameState.finalAnswer === null) {
          this.finalAnswerValue = '';
          this.associationAnswered = false;
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

  getClearAnswer() {
    return this.onTurn.pipe(
      filter((onTurn) => !onTurn),
      map(() => null)
    );
  }
}
