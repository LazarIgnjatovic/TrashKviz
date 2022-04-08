import { ClockViewToggle } from './clock-view-toggle.model';
import { DisconnectToggle } from './disconnect-toggle.model';

export interface ClosestNumberState extends DisconnectToggle {
  isWinner: boolean[];
  question: string;
  answer: number;
}
