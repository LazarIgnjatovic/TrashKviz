import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  TemplateRef,
} from '@angular/core';

@Component({
  selector: 'app-dialog',
  templateUrl: './dialog.component.html',
  styleUrls: ['./dialog.component.scss'],
})
export class DialogComponent implements OnInit {
  @Input() contentTemplate!: TemplateRef<any>;
  @Output() dialogClosed: EventEmitter<any> = new EventEmitter();
  constructor() {}

  ngOnInit(): void {}
}
