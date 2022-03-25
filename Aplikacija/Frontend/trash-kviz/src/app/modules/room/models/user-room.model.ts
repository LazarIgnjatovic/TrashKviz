import { User } from '../../user/models/user.model';

export interface UserRoom {
  isAdmin: boolean;
  isReady: boolean;
  user: User;
}
