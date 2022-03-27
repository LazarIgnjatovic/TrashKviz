import { User } from '../../user/models/user.model';
import { PlayerColor } from '../enums/player-color.enum';

export interface Player {
  user: User;
  points: number;
  isConnected: boolean;
  color: PlayerColor;
}
