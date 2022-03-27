import { User } from '../../user/models/user.model';

export interface Player {
  user: User;
  points: number;
  isConnected: boolean;
}
