import { GameState } from './game-state.model';

export interface SingleWinner extends GameState {
  winner: number;
}

export function isSingleWinner(state: GameState): state is SingleWinner {
  return 'winner' in state;
}
