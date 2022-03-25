import { GameState } from './game-state.model';

export interface TurnBased extends GameState {
  onTurn: number;
}
