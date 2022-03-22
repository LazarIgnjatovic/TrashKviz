import { Component, Input, OnInit, TemplateRef } from '@angular/core';

@Component({
  selector: 'app-mat-card',
  templateUrl: './mat-card.component.html',
  styleUrls: ['./mat-card.component.scss'],
})
export class MatCardComponent implements OnInit {
  @Input() titleTemplate!: TemplateRef<any>;
  @Input() contentTemplate!: TemplateRef<any>;
  @Input() actionsTemplate!: TemplateRef<any>;

  constructor() {}

  ngOnInit(): void {}
}
