import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { BehaviorSubject, Observable } from 'rxjs';
import FormFieldComponent from '../form-field/form-field.component';
import { FormSelectOption } from './form-select-option.interface';

@Component({
  selector: 'app-form-select',
  templateUrl: './form-select.component.html',
  styleUrls: ['./form-select.component.scss'],
})
export class FormSelectComponent extends FormFieldComponent implements OnInit {
  @Input() selectFormControlName: string = '';
  @Input() selectOptions: FormSelectOption<any>[] = [];
  @Input() placeholder: string = '';
  @Input() selectFormValidationErrorMessages: Record<string, string> = {};
  @Output() selectionChanged: EventEmitter<any> = new EventEmitter();
  
  selectFormControl!: FormControl;

  constructor() {
    super();
  }

  ngOnInit(): void {
    this.selectFormControl = this.getControl(this.selectFormControlName);
  }
}
