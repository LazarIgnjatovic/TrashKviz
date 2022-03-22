import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RegexProviderService } from 'src/app/core/services/regex-provider/regex-provider.service';
import { ValidationErrorMessageProviderService } from 'src/app/core/services/validation-error-messages-provider/validation-error-message-provider.service';
import { FormInputTypesProviderService } from 'src/app/shared/services/form-input-types-provider/form-input-types-provider.service';

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.scss'],
})
export class LoginFormComponent implements OnInit {
  @Input() title: string = '';
  @Input() icon: string = 'account_circle';
  @Input() buttonText: string = '';
  @Input() routerLinkValue: string = '';
  @Input() routerText: string = '';
  @Input() usernamePlaceholder: string = 'Username';
  @Input() passwordPlaceholder: string = 'Password';
  @Input() usernameLabel: string = 'Username';
  @Input() passwordLabel: string = 'Password';
  @Input() usernameAutocomplete: string = 'username';
  @Input() passwordAutocomplete: string = 'current-password';
  @Input() usernameName: string = 'username';
  @Input() passwordName: string = 'password';
  @Input() fieldsRequired: boolean = true;
  @Output() userFormOnSubmitEvent: EventEmitter<FormGroup> = new EventEmitter();
  userForm!: FormGroup;

  constructor(
    protected formBuilder: FormBuilder,
    protected regexProvider: RegexProviderService,
    public validationErrorMessagesProvider: ValidationErrorMessageProviderService,
    public formInputTypesProvider: FormInputTypesProviderService
  ) {}

  ngOnInit(): void {
    this.userForm = this.formBuilder.group({
      username: ['', this.usernameValidators()],
      password: ['', this.passwordValidators()],
    });
  }

  private usernameValidators() {
    return this.fieldsRequired ? [Validators.required] : [];
  }

  private passwordValidators() {
    const passwordValidators = [
      Validators.minLength(8),
      Validators.pattern(
        this.regexProvider.onlyOneCapitalOneLowercaseLetterAndOneNumber
      ),
    ];
    if (this.fieldsRequired) passwordValidators.push(Validators.required);
    return passwordValidators;
  }
}
