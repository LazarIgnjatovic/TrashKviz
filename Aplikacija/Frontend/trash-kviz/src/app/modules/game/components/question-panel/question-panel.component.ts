import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-question-panel',
  templateUrl: './question-panel.component.html',
  styleUrls: ['./question-panel.component.scss'],
})
export class QuestionPanelComponent implements OnInit {
  @Input() question: string = '';
  constructor() {}

  ngOnInit(): void {}
}
