import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-association-column',
  templateUrl: './association-column.component.html',
  styleUrls: ['./association-column.component.scss'],
})
export class MultipleChoiceColumnComponent implements OnInit {
  clicked: boolean[] = [false, false, false, false];
  @Input() hints!: string[];
  @Input() columnName!: string;
  @Input() answeAtBottom!: boolean;
  matPadding: boolean = true;
  constructor() {}

  ngOnInit(): void {}

  registerClick(index: number) {
    if (!this.clicked[index]) {
      this.clicked[index] = true;
    }
  }
}
