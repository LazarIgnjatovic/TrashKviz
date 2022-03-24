import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { FormControlComponent } from '../form-control/form-control.component';
import FormFieldComponent from '../form-field/form-field.component';

@Component({
  selector: 'app-form-checkbox',
  templateUrl: './form-checkbox.component.html',
  styleUrls: ['./form-checkbox.component.scss'],
})
export class FormCheckboxComponent
  extends FormControlComponent
  implements OnInit
{
  @Input() checkboxFormControlName: string = '';
  @Input() checkboxText: string = '';
  @Input() checkboxColor: string = 'accent';
  @Output() checkboxChanged: EventEmitter<any> = new EventEmitter();
  checkboxFormControl!: FormControl;
  constructor() {
    super();
  }

  ngOnInit(): void {
    this.checkboxFormControl = this.getControl(this.checkboxFormControlName);
  }
}
