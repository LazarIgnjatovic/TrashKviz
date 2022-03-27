import { GameState } from './game-state.model';

export interface InfoState extends GameState {
  infoText: string;
}

export function isInfoState(state: GameState): state is InfoState {
  return 'infoText' in state;
}
