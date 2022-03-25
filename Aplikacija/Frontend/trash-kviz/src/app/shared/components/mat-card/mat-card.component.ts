import { Component, Input, OnInit, TemplateRef } from '@angular/core';

@Component({
  selector: 'app-mat-card',
  templateUrl: './mat-card.component.html',
  styleUrls: ['./mat-card.component.scss'],
})
export class MatCardComponent implements OnInit {
  @Input() titleTemplate: TemplateRef<any> | null = null;
  @Input() contentTemplate: TemplateRef<any> | null = null;
  @Input() actionsTemplate: TemplateRef<any> | null = null;
  @Input() outline: string = 'none';
  constructor() {}

  ngOnInit(): void {}
}
