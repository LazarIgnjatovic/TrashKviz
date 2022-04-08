import { ClockViewToggle } from './clock-view-toggle.model';
import { FirstTwoAnswers } from './first-two-answers.model';
export interface MultipleChoiceState extends FirstTwoAnswers {
  question: string;
  answers: string[];
  correctAnswer: number;
  playerAnswers: number[];
}
