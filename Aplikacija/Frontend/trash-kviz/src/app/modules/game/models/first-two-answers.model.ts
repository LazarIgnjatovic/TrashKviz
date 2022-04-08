import { ClockViewToggle, isClockViewToggle } from './clock-view-toggle.model';
import { GameState } from './game-state.model';
import { isDisconnectToggle, DisconnectToggle } from './disconnect-toggle.model';

export interface FirstTwoAnswers extends DisconnectToggle {
  first: number;
  second: number;
}

export function isFirstTwoAnswers(state: GameState): state is FirstTwoAnswers {
  return isDisconnectToggle(state) && 'first' in state && 'second' in state;
}
