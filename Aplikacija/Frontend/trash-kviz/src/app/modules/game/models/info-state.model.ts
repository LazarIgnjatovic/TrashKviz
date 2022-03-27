import { GameState } from './game-state.model';

export interface InfoState extends GameState {
  infoText: string;
  timerValue: number;
}
