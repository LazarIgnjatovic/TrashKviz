import { Component, Input, OnInit } from '@angular/core';
import { BehaviorSubject, filter, from, map } from 'rxjs';
import { PlayerColor } from '../../enums/player-color.enum';
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
  buttonColors: string[] = [
    'linear-gradient(90deg, #808080 0%, #808080 50%, rgb(40,36,36) 50%, rgb(40,36,36) 100%)',
    'linear-gradient(90deg, #808080 0%, #808080 50%, rgb(40,36,36) 50%, rgb(40,36,36) 100%)',
    'linear-gradient(90deg, #808080 0%, #808080 50%, rgb(40,36,36) 50%, rgb(40,36,36) 100%)',
    'linear-gradient(90deg, #808080 0%, #808080 50%, rgb(40,36,36) 50%, rgb(40,36,36) 100%)',
  ];
  buttonColorPosition: string = 'right bottom';
  buttonsDisabled: boolean = false;
  correctAnswer: number = -1;
  constructor() {
    super();
  }

  ngOnInit(): void {
    this.gameStateSubscription = this.gameState
      .pipe(map((gameState) => gameState as MultipleChoiceState))
      .subscribe((gameState) => {
        this.updateFields(gameState);
      });
  }

  updateFields(gameState: MultipleChoiceState) {
    this.question = gameState.question;
    this.answers = gameState.answers;

    if (gameState.isActive) {
      this.buttonsDisabled = false;
      this.buttonColors = [
        'linear-gradient(90deg, #808080 0%, #808080 50%, rgb(40,36,36) 50%, rgb(40,36,36) 100%)',
        'linear-gradient(90deg, #808080 0%, #808080 50%, rgb(40,36,36) 50%, rgb(40,36,36) 100%)',
        'linear-gradient(90deg, #808080 0%, #808080 50%, rgb(40,36,36) 50%, rgb(40,36,36) 100%',
        'linear-gradient(90deg, #808080 0%, #808080 50%, rgb(40,36,36) 50%, rgb(40,36,36) 100%',
      ];
      this.buttonColorPosition = 'right bottom';
      this.correctAnswer = -1;
    } else {
      this.buttonsDisabled = true;
      let answerColors: string[][] = [[], [], [], []];
      gameState.playerAnswers.forEach((val, index) => {
        if (val !== null) {
          answerColors[val].push(this.getColor(index));
        }
      });

      let gradients: string[] = ['', '', '', ''];

      answerColors.forEach((val, index) => {
        if (val.length > 0) {
          let step = 50 / val.length;
          val.forEach((val2, index2) => {
            gradients[index] = gradients[index].concat(
              `${val2} ${index2 * step}%, ${val2} ${index2 * step + step}%,`
            );
          });
        } else gradients[index] = '#808080 0%, #808080 50%,';
      });

      this.buttonColors = [
        `linear-gradient(90deg, ${gradients[0]} rgb(40,36,36) 50%, rgb(40,36,36) 100%)`,
        `linear-gradient(90deg, ${gradients[1]} rgb(40,36,36) 50%, rgb(40,36,36) 100%)`,
        `linear-gradient(90deg, ${gradients[2]} rgb(40,36,36) 50%, rgb(40,36,36) 100%)`,
        `linear-gradient(90deg, ${gradients[3]} rgb(40,36,36) 50%, rgb(40,36,36) 100%)`,
      ];
      this.buttonColorPosition = 'left bottom';

      this.correctAnswer = gameState.correctAnswer;
    }
  }

  private getColor(playerColor: PlayerColor) {
    switch (playerColor) {
      case PlayerColor.red:
        return '#ce3045';
      case PlayerColor.green:
        return '#8dc63f';
      case PlayerColor.blue:
        return '#007bbf';
      case PlayerColor.yellow:
        return '#ffcb05';
      default:
        return '#808080';
    }
  }

  onClick(answer: Answer) {
    this.answerSubmitted.emit(answer);
    this.buttonsDisabled = true;
  }
}
