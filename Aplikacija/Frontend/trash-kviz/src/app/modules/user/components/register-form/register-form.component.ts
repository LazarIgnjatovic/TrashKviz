import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, Validators } from '@angular/forms';
import { RegexProviderService } from 'src/app/core/services/regex-provider/regex-provider.service';
import { ValidationErrorMessageProviderService } from 'src/app/core/services/validation-error-messages-provider/validation-error-message-provider.service';
import { FormInputTypesProviderService } from 'src/app/shared/services/form-input-types-provider/form-input-types-provider.service';
import { LoginFormComponent } from '../login-form/login-form.component';

@Component({
  selector: 'app-register-form',
  templateUrl: './register-form.component.html',
  styleUrls: ['../login-form/login-form.component.scss'],
})
export class RegisterFormComponent extends LoginFormComponent {
  @Input() emailPlaceholder: string = 'E-mail';
  @Input() emailLabel: string = 'E-mail';
  @Input() emailAutocomplete: string = 'email';
  @Input() emailName: string = 'email';
  constructor(
    formBuilder: FormBuilder,
    regexProvider: RegexProviderService,
    validationErrorMessagesProvider: ValidationErrorMessageProviderService,
    formInputTypesProvider: FormInputTypesProviderService
  ) {
    super(
      formBuilder,
      regexProvider,
      validationErrorMessagesProvider,
      formInputTypesProvider
    );
  }

  override ngOnInit(): void {
    super.ngOnInit();
    this.userForm.addControl(
      'email',
      this.formBuilder.control('', this.emailValidators())
    );
  }

  private emailValidators() {
    const emailValidators = [Validators.email];
    if (this.fieldsRequired) emailValidators.push(Validators.required);
    return emailValidators;
  }
}
