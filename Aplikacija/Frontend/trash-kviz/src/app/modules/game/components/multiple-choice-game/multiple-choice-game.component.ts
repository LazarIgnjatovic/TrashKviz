import { Component, Input, OnInit } from '@angular/core';
import { map } from 'rxjs';
import { Answer } from '../../models/answer.model';
import { MultipleChoiceState } from '../../models/multiple-choice-state.model';
import { BaseGameComponent } from '../base-game/base-game.component';

@Component({
  selector: 'app-multiple-choice-game',
  templateUrl: './multiple-choice-game.component.html',
  styleUrls: ['./multiple-choice-game.component.scss'],
})
export class MultipleChoiceGameComponent
  extends BaseGameComponent<Answer>
  implements OnInit
{
  question: string = '';
  answers: string[] = [];
  constructor() {
    super();
  }

  ngOnInit(): void {
    this.gameStateSubscription = this.gameState
      .pipe(map((gameState) => gameState as MultipleChoiceState))
      .subscribe((gameState) =>
        this.updateFields(gameState.question, gameState.answers)
      );
  }

  updateFields(question: string, answers: string[]) {
    this.question = question;
    this.answers = answers;
  }
}
