import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RegexProviderService } from 'src/app/core/services/regex-provider/regex-provider.service';
import { ValidationErrorMessageProviderService } from 'src/app/core/services/validation-error-messages-provider/validation-error-message-provider.service';
import { FormInputTypesProviderService } from 'src/app/shared/services/form-input-types-provider/form-input-types-provider.service';

@Component({
  selector: 'app-join-form',
  templateUrl: './join-form.component.html',
  styleUrls: ['./join-form.component.scss'],
})
export class JoinFormComponent implements OnInit {
  codeForm!: FormGroup;
  @Output() codeSubmitEvent: EventEmitter<FormGroup> = new EventEmitter();

  constructor(
    private formBuilder: FormBuilder,
    private regexProvider: RegexProviderService,
    public formInputTypesProvider: FormInputTypesProviderService,
    public validationErrorMessagesProvider: ValidationErrorMessageProviderService
  ) {}

  ngOnInit(): void {
    this.codeForm = this.formBuilder.group({
      code: [
        '',
        Validators.pattern(
          this.regexProvider.onlyCapitalLettersAndNumbersLength4
        ),
      ],
    });
  }
}
