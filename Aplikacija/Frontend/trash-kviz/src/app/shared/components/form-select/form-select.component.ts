import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import FormFieldComponent from '../form-field/form-field.component';
import { FormSelectOption } from './form-select-option.interface';

@Component({
  selector: 'app-form-select',
  templateUrl: './form-select.component.html',
  styleUrls: ['./form-select.component.scss'],
})
export class FormSelectComponent extends FormFieldComponent implements OnInit {
  @Input() selectFormControlName: string = '';
  @Input() selectOptions: FormSelectOption<any>[] = [{viewValue: "AAA", value:"AAAA"}];
  @Input() placeholder: string = '';
  @Input() selectFormValidationErrorMessages: Record<string, string> = {};
  selectFormControl!: FormControl;
  constructor() {
    super();
  }

  ngOnInit(): void {
    this.selectFormControl = this.getControl(this.selectFormControlName);
  }
}
