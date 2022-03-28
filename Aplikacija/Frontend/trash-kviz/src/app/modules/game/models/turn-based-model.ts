import { GameState } from './game-state.model';

export interface TurnBased extends GameState {
  onTurn: number;
  showTimer: boolean;
}
export function isTurnBased(state: GameState): state is TurnBased {
  return 'onTurn' in state && 'showTimer' in state;
}
