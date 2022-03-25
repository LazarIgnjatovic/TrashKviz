import { UserRoom } from './user-room.model';

export interface RoomState {
  game1: number;
  game2: number;
  game3: number;
  isPublic: boolean;
  roomId: string;
  roomName: string;
  users: UserRoom[];
}
