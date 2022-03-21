import { Component, Input, ViewChild } from '@angular/core';
import {
  ControlValueAccessor,
  FormControl,
  FormControlDirective,
  FormGroup,
  NG_VALUE_ACCESSOR,
} from '@angular/forms';
import { MatFormFieldAppearance } from '@angular/material/form-field';
import { FormControlComponent } from '../form-control/form-control.component';

@Component({
  template: '',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: FormControlComponent,
      multi: true,
    },
  ],
})
export default abstract class FormFieldComponent
  extends FormControlComponent
  implements ControlValueAccessor
{
  @ViewChild(FormControlDirective, { static: true })
  formControlDirective!: FormControlDirective;

  @Input() appearance: MatFormFieldAppearance = 'outline';
  @Input() label: string = '';

  @Input() autocomplete: string = 'off';

  constructor() {
    super();
  }

  writeValue(obj: any): void {
    this.formControlDirective.valueAccessor?.writeValue(obj);
  }

  registerOnChange(fn: any): void {
    this.formControlDirective.valueAccessor?.registerOnChange(fn);
  }

  registerOnTouched(fn: any): void {
    this.formControlDirective.valueAccessor?.registerOnTouched(fn);
  }
}
