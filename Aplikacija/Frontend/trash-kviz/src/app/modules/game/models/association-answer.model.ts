import { Answer } from './answer.model';

export interface AssociationAnswer extends Answer {
  column: number;
  field: number;
  isColumnAnswer: boolean;
  isFinalAnswer: boolean;
}
