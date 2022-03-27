import {
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import { BehaviorSubject, from, map, Observable, Subscription } from 'rxjs';
import { RegexProviderService } from 'src/app/core/services/regex-provider/regex-provider.service';
import { AssociationAnswer } from '../../models/association-answer.model';
import { AssociationState } from '../../models/association-state.model';
import { GameState } from '../../models/game-state.model';
import { ColumnComponent } from '../column/column.component';

@Component({
  selector: 'app-association-column',
  templateUrl: './association-column.component.html',
  styleUrls: ['./association-column.component.scss'],
})
export class MultipleChoiceColumnComponent
  extends ColumnComponent
  implements OnInit, OnDestroy
{
  @Input() gameState!: Observable<GameState>;
  @Input() columnNumber!: number;
  private gameStateSubscription!: Subscription;
  constructor(regexProvider: RegexProviderService) {
    super(regexProvider);
  }
  override ngOnDestroy(): void {
    this.gameStateSubscription.unsubscribe();
  }

  override ngOnInit(): void {
    this.gameStateSubscription = this.gameState
      .pipe(map((state) => state as AssociationState))
      .subscribe((gameState) => {
        this.updateFields(
          gameState.columns[this.columnNumber],
          gameState.columnAnswers[this.columnNumber],
          gameState.openAllowed
        );
      });
  }

  onFieldClicked(index: number) {
    if (!this.clicked[index]) {
      this.fieldClicked.emit({
        text: '',
        isColumnAnswer: false,
        isFinalAnswer: false,
        column: this.columnNumber,
        field: index,
      });
    }
  }

  updateFields(hints: string[], columnAnswer: string, openAllowed: boolean) {
    from(hints)
      .pipe(map((val: string, index: number) => [val, index] as const))
      .subscribe((hint) => {
        this.clicked[hint[1]] = hint[0] != null;
      });

    if (columnAnswer !== null) {
      if (columnAnswer.startsWith('#')) {
        this.columnAnswer = columnAnswer.slice(1);
        this.columnAnswered = false;
      } else {
        this.columnAnswered = true;
        this.columnAnswer = columnAnswer;
      }
    } else if (columnAnswer === null) {
      this.columnAnswer = '';
      this.columnAnswered = false;
    }

    this.hints = hints;

    this.disableClickable = !openAllowed;
  }

  onAnswerEntered(answer: string) {
    this.fieldClicked.emit({
      text: answer,
      isColumnAnswer: true,
      isFinalAnswer: false,
      column: this.columnNumber,
      field: 0,
    });
  }
}
