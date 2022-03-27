import {
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import { BehaviorSubject, filter, map, Observable, Subscription } from 'rxjs';
import { Answer } from '../../models/answer.model';
import { GameState } from '../../models/game-state.model';

@Component({
  template: '',
})
export abstract class BaseGameComponent<AnswerType extends Answer>
  implements OnDestroy
{
  @Input() gameState!: Observable<GameState>;
  @Output() answerSubmitted: EventEmitter<AnswerType> = new EventEmitter();

  gameStateSubscription!: Subscription;
  constructor() {}
  ngOnDestroy(): void {
    this.gameStateSubscription?.unsubscribe();
  }

  
}
