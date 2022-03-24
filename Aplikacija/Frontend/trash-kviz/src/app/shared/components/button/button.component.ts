import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'app-button',
  templateUrl: './button.component.html',
  styleUrls: ['./button.component.scss'],
})
export class ButtonComponent implements OnInit {
  @Input() buttonDisabled: boolean = false;
  @Input() buttonText: string = '';
  @Input() buttonWidth: string = '100px';
  @Input() buttonHeight: string = 'fit-content';
  @Input() buttonType: string = 'button';
  @Input() buttonColor: string = 'primary';
  @Output() buttonClicked: EventEmitter<any> = new EventEmitter();
  constructor() {}

  ngOnInit(): void {}
}
