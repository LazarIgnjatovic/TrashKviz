import {
  Component,
  ElementRef,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewChild,
} from '@angular/core';
import { FormControl, NG_VALUE_ACCESSOR } from '@angular/forms';
import { MatFormField } from '@angular/material/form-field';
import { BehaviorSubject, Observable } from 'rxjs';
import { WindowResizeDetectorService } from 'src/app/core/services/window-resize-detector/window-resize-detector.service';
import { FormInputTypesProviderService } from '../../services/form-input-types-provider/form-input-types-provider.service';
import FormFieldComponent from '../form-field/form-field.component';

@Component({
  selector: 'app-form-input',
  templateUrl: './form-input.component.html',
  styleUrls: ['./form-input.component.scss'],
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: FormInputComponent,
      multi: true,
    },
  ],
})
export class FormInputComponent extends FormFieldComponent implements OnInit {
  @Input() placeholder: string = '';
  @Input() hint: string = '';
  @Input() formInputType = this.formInputTypesProvider.formInputTypes.text;
  @Input() maxLength: string = 'none';
  @Input() inputFormControlName: string = '';
  @Input() inputValidationErrorMessages: Record<string, string> = {};
  @Input() textAreaRows: number = 15;
  @Input() name: string = '';
  @Output() inputChanged: EventEmitter<any> = new EventEmitter();
  inputFormControl!: FormControl;
  constructor(
    public formInputTypesProvider: FormInputTypesProviderService,
    public resizeDetectService: WindowResizeDetectorService
  ) {
    super();
  }

  ngOnInit(): void {
    this.inputFormControl = this.getControl(this.inputFormControlName);
  }

  onFocus(el: MatFormField) {
    this.resizeDetectService.setKeyboardOpen(true);

    // setTimeout(
    //   () =>
    //     el._elementRef.nativeElement.scrollIntoView({
    //       block: 'end',
    //       inline: 'nearest',
    //       behavior: 'smooth',
    //     }),
    //   200
    // );
  }

  onBlur() {
    this.resizeDetectService.setKeyboardOpen(false);
  }
}
