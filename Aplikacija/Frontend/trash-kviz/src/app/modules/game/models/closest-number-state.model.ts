import { ClockViewToggle } from './clock-view-toggle.model';

export interface ClosestNumberState extends ClockViewToggle {
  canAnswer: boolean[];
  isWinner: boolean[];
  question: string;
  answer: number;
}
