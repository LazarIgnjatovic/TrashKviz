import {
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import { BehaviorSubject, from, map, Observable, Subscription } from 'rxjs';
import { AssociationAnswer } from '../../models/association-answer.model';
import { AssociationState } from '../../models/association-state.model';
import { GameState } from '../../models/game-state.model';

@Component({
  selector: 'app-association-column',
  templateUrl: './association-column.component.html',
  styleUrls: ['./association-column.component.scss'],
})
export class MultipleChoiceColumnComponent implements OnInit, OnDestroy {
  @Input() gameState!: Observable<GameState>;
  @Input() columnName!: string;
  @Input() answeAtBottom!: boolean;
  @Input() columnNumber!: number;
  @Output() fieldClicked: EventEmitter<AssociationAnswer> = new EventEmitter();
  @Input() disableAll: boolean = false;
  @Input() dontShowAll: boolean = true;

  clicked: boolean[] = [false, false, false, false];
  hints: string[] = ['', '', '', ''];
  columnAnswer: string = '';
  disableAnswerInput: boolean = false;
  disableFields: boolean = false;
  private gameStateSubscription!: Subscription;
  constructor() {}
  ngOnDestroy(): void {
    this.gameStateSubscription.unsubscribe();
  }

  ngOnInit(): void {
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

  emitAnswer(index: number) {
    if (!this.disableFields) {
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
      this.disableAnswerInput = true;
      this.columnAnswer = columnAnswer;
    } else if (columnAnswer === null) {
      this.columnAnswer = '';
      this.disableAnswerInput = false;
    }

    this.hints = hints;

    this.disableFields = !openAllowed;
  }

  getColumnAnswer(): AssociationAnswer {
    return {
      text: this.columnAnswer,
      isColumnAnswer: true,
      isFinalAnswer: false,
      column: this.columnNumber,
      field: 0,
    };
  }
}
