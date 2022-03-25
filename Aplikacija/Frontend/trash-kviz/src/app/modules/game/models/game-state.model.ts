import { GameType } from '../enums/game-type.enum';
import { Player } from './player.model';

export interface GameState {
  type: GameType;
  players: Player[];
}
