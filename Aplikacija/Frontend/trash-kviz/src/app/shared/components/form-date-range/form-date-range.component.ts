import { Component, Input, OnInit } from '@angular/core';
import { FormControl, NG_VALUE_ACCESSOR } from '@angular/forms';
import FormFieldComponent from '../form-field/form-field.component';

@Component({
  selector: 'app-form-date-range',
  templateUrl: './form-date-range.component.html',
  styleUrls: ['./form-date-range.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: FormDateRangeComponent,
      multi: true,
    },
  ],
})
export class FormDateRangeComponent
  extends FormFieldComponent
  implements OnInit
{
  @Input() endDateFormControlName: string = '';
  @Input() startDateFormControlName: string = '';
  @Input() startDatePlaceholder: string = '';
  @Input() endDatePlaceholder: string = '';
  @Input() minDate: Date;
  @Input() inputDisabled: boolean = true;

  startDateFormControl!: FormControl;
  endDateFormControl!: FormControl;
  constructor() {
    super();
    this.minDate = new Date(Date.now());
  }

  ngOnInit(): void {
    this.startDateFormControl = this.getControl(this.startDateFormControlName);
    this.endDateFormControl = this.getControl(this.endDateFormControlName);
  }
}
