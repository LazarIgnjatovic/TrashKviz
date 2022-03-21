import { Component, Input, OnInit, TemplateRef } from '@angular/core';

@Component({
  template: '',
})
export abstract class VirtualScrollComponent {
  @Input() itemSize: number = 50;
  @Input() height: number = 200;
  constructor() {}
}
