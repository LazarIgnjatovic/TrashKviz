import { ClockViewToggle, isClockViewToggle } from './clock-view-toggle.model';
import { GameState } from './game-state.model';

export interface DisconnectToggle extends ClockViewToggle {
  canAnswer: boolean[];
}

export function isDisconnectToggle(state: GameState): state is DisconnectToggle {
  return isClockViewToggle(state) && 'canAnswer' in state;
}
