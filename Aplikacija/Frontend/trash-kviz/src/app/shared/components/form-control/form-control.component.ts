import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  template: '',
})
export abstract class FormControlComponent {
  @Input() parentFormGroup!: FormGroup;
  constructor() {}

  public getControl(formFieldFormControlName: string): FormControl {
    const formControl = this.parentFormGroup.get(formFieldFormControlName);
    return formControl != null
      ? (formControl as FormControl)
      : new FormControl();
  }

  public getValidationErrorMessage(
    formFieldValidationErrorMessages: Record<string, string>,
    formFieldFormControl: FormControl
  ): string {
    const errorKey = Object.keys(formFieldValidationErrorMessages).find(
      (errorKey) => formFieldFormControl.hasError(errorKey)
    );
    return errorKey ? formFieldValidationErrorMessages[errorKey] : '';
  }
}
