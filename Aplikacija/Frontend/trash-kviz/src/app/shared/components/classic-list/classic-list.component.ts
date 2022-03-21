import { Component, Input, OnInit, TemplateRef } from '@angular/core';
import { Observable, of } from 'rxjs';
import { VirtualScrollComponent } from '../virtual-scroll/virtual-scroll.component';

@Component({
  selector: 'app-classic-list',
  templateUrl: './classic-list.component.html',
  styleUrls: ['./classic-list.component.scss'],
})
export class ClassicListComponent
  extends VirtualScrollComponent
  implements OnInit
{
  @Input() items: Observable<any[]> = of([]);
  @Input() listItemTemplate!: TemplateRef<any>;

  constructor() {
    super();
  }

  ngOnInit(): void {}
}
