import { GameState } from './game-state.model';

export interface ClockViewToggle extends GameState {
  isActive: boolean;
}

export function isClockViewToggle(state: GameState): state is ClockViewToggle {
  return (
    'isActive' in state
  );
}
