import { ClockViewToggle } from './clock-view-toggle.model';
export interface MultipleChoiceState extends ClockViewToggle {
  question: string;
  answers: string[];
  correctAnswer: number;
  first: number;
  secon: number;
  playerAnswers: string[];
  canAnswer: boolean[];
}
