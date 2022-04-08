import { Component, OnInit } from '@angular/core';
import { map } from 'rxjs';
import { Answer } from '../../models/answer.model';
import { ClosestNumberState } from '../../models/closest-number-state.model';
import { BaseGameComponent } from '../base-game/base-game.component';

@Component({
  selector: 'app-closest-number-game',
  templateUrl: './closest-number-game.component.html',
  styleUrls: ['./closest-number-game.component.scss'],
})
export class ClosestNumberGameComponent
  extends BaseGameComponent<Answer>
  implements OnInit
{
  inputDisabled: boolean = false;
  question: string = '';
  answer!: number | null;
  constructor() {
    super();
  }

  ngOnInit(): void {
    this.gameStateSubscription = this.gameState
      .pipe(map((gameState) => gameState as ClosestNumberState))
      .subscribe((gameState) => {
        this.updateFields(gameState.question, gameState.answer);
        this.inputDisabled = !gameState.isActive;
        if (gameState.isActive) this.answer = null;
      });
  }

  updateFields(question: string, answer: number) {
    this.question = question;
    if (answer !== null) {
      this.answer = answer;
    }
  }
}
