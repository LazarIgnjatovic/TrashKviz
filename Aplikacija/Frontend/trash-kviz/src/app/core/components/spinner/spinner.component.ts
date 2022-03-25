import { Component, Inject, Input, OnInit } from '@angular/core';
import { COMPONENT_DATA } from 'src/app/core/services/overlay-service/overlay.service';

@Component({
  selector: 'app-spinner',
  templateUrl: './spinner.component.html',
  styleUrls: ['./spinner.component.scss'],
})
export class SpinnerComponent implements OnInit {
  constructor(@Inject(COMPONENT_DATA) public componentData: any) {}

  ngOnInit(): void {}
}
