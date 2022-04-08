import { Answer } from './answer.model';
import { ClockViewToggle } from './clock-view-toggle.model';
import { DisconnectToggle } from './disconnect-toggle.model';
import { GameState } from './game-state.model';
import { SingleWinner } from './single-winner.model';

export interface StepByStepState extends DisconnectToggle, SingleWinner {
  steps: string[];
  finalAnswer: string;
  answers: string[];
}
