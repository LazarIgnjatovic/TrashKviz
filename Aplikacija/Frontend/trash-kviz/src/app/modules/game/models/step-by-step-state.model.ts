import { Answer } from './answer.model';
import { ClockViewToggle } from './clock-view-toggle.model';
import { GameState } from './game-state.model';

export interface StepByStepState extends ClockViewToggle {
  steps: string[];
  finalAnswer: string;
  canAnswer: boolean[];
  answers: string[];
  winner: number;
}
