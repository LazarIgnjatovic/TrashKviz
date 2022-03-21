import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import { MatSelectionList } from '@angular/material/list';
import { ClassicListComponent } from '../classic-list/classic-list.component';

@Component({
  selector: 'app-select-list',
  templateUrl: './select-list.component.html',
  styleUrls: ['./select-list.component.scss'],
})
export class SelectListComponent
  extends ClassicListComponent
  implements OnInit
{
  @Input() multupleSelect: boolean = false;
  @Output() selectionEvent: EventEmitter<any> = new EventEmitter();
  @ViewChild('list') list!: MatSelectionList;

  constructor() {
    super();
  }

  clearSelect(): void {
    this.list.deselectAll();
  }
  get selectedOption() {
    return this.list.selectedOptions.selected[0];
  }
}
