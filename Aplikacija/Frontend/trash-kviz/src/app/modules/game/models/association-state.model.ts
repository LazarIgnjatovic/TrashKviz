import { GameState } from './game-state.model';
import { Player } from './player.model';
import { TurnBased } from './turn-based-model';

export interface AssociationState extends TurnBased {
  columns: string[][];
  columnAnswers: string[];
  finalAnswer: string;
  openAllowed: boolean;
  timerValue: number;
  turnTimerValue: number;
}

export function isAssociationState(
  state: GameState
): state is AssociationState {
  return (
    'players' in state &&
    'columns' in state &&
    'columnAnswers' in state &&
    'finalAnswer' in state &&
    'onTurn' in state &&
    'openAllowed' in state &&
    'timerValue' in state &&
    'turnTimerValue' in state
  );
}
