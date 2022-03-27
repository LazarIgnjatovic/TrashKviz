import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { BehaviorSubject, filter, from, map, Observable, toArray } from 'rxjs';
import { Answer } from '../../models/answer.model';
import { GameState } from '../../models/game-state.model';
import { StepByStepState } from '../../models/step-by-step-state.model';
import { BaseGameComponent } from '../base-game/base-game.component';

@Component({
  selector: 'app-step-by-step-game',
  templateUrl: './step-by-step-game.component.html',
  styleUrls: ['./step-by-step-game.component.scss'],
})
export class StepByStepGameComponent
  extends BaseGameComponent<Answer>
  implements OnInit
{
  steps: string[] = [];
  fieldsOpened: boolean[] = [false, false, false, false, false, false, false];
  finalAnswer: string = '';
  columnAnswered: boolean = false;
  disableAnswer: boolean = false;
  constructor() {
    super();
  }

  ngOnInit(): void {
    this.gameStateSubscription = this.gameState
      .pipe(map((gameState) => gameState as StepByStepState))
      .subscribe((gameState) => {
        this.updateFields(
          gameState.steps,
          gameState.finalAnswer,
          gameState.isActive
        );
      });
  }

  onAnswerEntered(answer: string) {
    this.disableAnswer = true;
    this.answerSubmitted.emit({ text: answer });
  }

  updateFields(steps: string[], finalAnswer: string, isActive: boolean) {
    from(steps)
      .pipe(
        filter((step) => step !== null),
        toArray()
      )
      .subscribe((steps) => (this.steps = steps));

    from(steps)
      .pipe(
        filter((step) => step !== null),
        map(() => true),
        toArray()
      )
      .subscribe((steps) => (this.fieldsOpened = steps));

    if (finalAnswer !== null) {
      this.finalAnswer = finalAnswer;
      this.columnAnswered = true;
    } else if (finalAnswer === null) {
      this.finalAnswer = '';
      this.columnAnswered = false;
    }

    this.disableAnswer = !isActive;
  }

  getClearAnswer() {
    return this.gameState.pipe(
      map((gamestate) => gamestate as StepByStepState),
      filter((gamestate) => !gamestate.isActive),
      map(() => null)
    );
  }
}
