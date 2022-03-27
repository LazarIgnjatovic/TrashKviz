import {
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { GameState } from '../../models/game-state.model';

@Component({
  selector: 'app-column',
  templateUrl: './column.component.html',
  styleUrls: ['./column.component.scss'],
})
export class ColumnComponent implements OnInit, OnDestroy {
  @Input() columnName!: string;
  @Input() answerAtBottom!: boolean;
  @Input() disableAll: boolean = false;
  @Input() disableClickable: boolean = false;
  @Input() columnIds: number[] = [0, 1, 2, 3];
  @Input() rippleDisabled: boolean = false;
  @Input() columnAnswerPlaceholder: string;
  @Input() clicked: boolean[] = [false, false, false, false];
  @Input() hints: string[] = ['', '', '', ''];
  @Input() columnAnswer: string = '';
  @Input() columnAnswered: boolean = false;
  @Output() fieldClicked: EventEmitter<any> = new EventEmitter();
  @Output() answerEntered: EventEmitter<any> = new EventEmitter();
  @Input() clearAnswer!: Observable<null>;
  private clearAnswerSubscription!: Subscription;
  constructor() {
    this.columnAnswerPlaceholder = 'Rešenje kolone ' + this.columnName;
  }
  ngOnDestroy(): void {
    this.clearAnswerSubscription.unsubscribe();
  }
  ngOnInit(): void {
    this.clearAnswerSubscription = this.clearAnswer.subscribe(() => {
      if (!this.columnAnswered && this.columnAnswer != '') {
        this.columnAnswer = '';
      }
    });
  }
}
