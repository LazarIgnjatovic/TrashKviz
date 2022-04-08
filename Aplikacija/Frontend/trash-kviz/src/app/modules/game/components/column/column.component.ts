import {
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { RegexProviderService } from 'src/app/core/services/regex-provider/regex-provider.service';
import { WindowResizeDetectorService } from 'src/app/core/services/window-resize-detector/window-resize-detector.service';
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
  @Input() disableAnswer: boolean = false;
  @Input() disableClickable: boolean = false;
  @Input() columnIds: number[] = [0, 1, 2, 3];
  @Input() rippleDisabled: boolean = false;
  @Input() columnAnswerPlaceholder: string = '';
  @Input() clicked: boolean[] = [false, false, false, false];
  @Input() hints: string[] = ['', '', '', ''];
  @Input() columnAnswer: string = '';
  @Input() columnAnswered: boolean = false;
  @Output() fieldClicked: EventEmitter<any> = new EventEmitter();
  @Output() answerEntered: EventEmitter<any> = new EventEmitter();
  @Input() clearAnswer!: Observable<null>;
  @Input() answerLabel: string = '';
  private clearAnswerSubscription!: Subscription;
  private regExp!: RegExp;
  constructor(
    protected regexProvider: RegexProviderService,
    public resizeDetectService: WindowResizeDetectorService
  ) {}
  ngOnDestroy(): void {
    this.clearAnswerSubscription.unsubscribe();
  }
  ngOnInit(): void {
    this.clearAnswerSubscription = this.clearAnswer.subscribe(() => {
      if (!this.columnAnswered && this.columnAnswer != '') {
        this.columnAnswer = '';
      }
    });
    if (this.columnAnswerPlaceholder === '')
      this.columnAnswerPlaceholder = 'Rešenje kolone ' + this.columnName;
    if (this.answerLabel === '') this.answerLabel = this.columnName;
    this.regExp = new RegExp(this.regexProvider.onlyLettersAndNumbers);
  }

  onKeyPress(event: any) {
    return this.regExp.test(event.key);
  }
}
