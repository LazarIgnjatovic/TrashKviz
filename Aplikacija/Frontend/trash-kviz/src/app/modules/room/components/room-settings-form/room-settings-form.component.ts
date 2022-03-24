import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ValidationErrorMessageProviderService } from 'src/app/core/services/validation-error-messages-provider/validation-error-message-provider.service';
import { FormSelectOption } from 'src/app/shared/components/form-select/form-select-option.interface';
import { FormInputTypesProviderService } from 'src/app/shared/services/form-input-types-provider/form-input-types-provider.service';

@Component({
  selector: 'app-room-settings-form',
  templateUrl: './room-settings-form.component.html',
  styleUrls: ['./room-settings-form.component.scss'],
})
export class RoomSettingsFormComponent implements OnInit {
  roomSettingsForm!: FormGroup;
  @Output() roomSettingsFormOnSubmit: EventEmitter<any> = new EventEmitter();
  @Input() numberOfGames: number = 3;
  @Input() gameOptions: FormSelectOption<number>[] = [
    { value: 0, viewValue: 'Asocijacije' },
    { value: 1, viewValue: 'Najbliži broj' },
    { value: 2, viewValue: 'Ko zna zna' },
    { value: 3, viewValue: 'Korak po korak' },
  ];
  @Input() defaultRoomName: string = '';
  @Input() game1: number = 0;
  @Input() game2: number = 0;
  @Input() game3: number = 0;
  @Input() roomId: string = '';
  
  constructor(
    private formBuilder: FormBuilder,
    public validationErrorMessageProvider: ValidationErrorMessageProviderService,
    public formInputTypesProvider: FormInputTypesProviderService
  ) {}

  ngOnInit(): void {
    this.roomSettingsForm = this.formBuilder.group({
      Prvaigra: [this.game1, Validators.required],
      Drugaigra: [this.game2, Validators.required],
      Trećaigra: [this.game3, Validators.required],
      roomname: [this.defaultRoomName, Validators.required],
    });
  }
}
