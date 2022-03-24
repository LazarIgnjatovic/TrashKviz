import {
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  OnInit,
  Output,
} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {
  BehaviorSubject,
  debounceTime,
  map,
  Observable,
  Subject,
  Subscription,
} from 'rxjs';
import { ValidationErrorMessageProviderService } from 'src/app/core/services/validation-error-messages-provider/validation-error-message-provider.service';
import { FormSelectOption } from 'src/app/shared/components/form-select/form-select-option.interface';
import { FormInputTypesProviderService } from 'src/app/shared/services/form-input-types-provider/form-input-types-provider.service';
import { RoomState } from '../../models/room-state.model';

@Component({
  selector: 'app-room-settings-form',
  templateUrl: './room-settings-form.component.html',
  styleUrls: ['./room-settings-form.component.scss'],
})
export class RoomSettingsFormComponent implements OnInit, OnDestroy {
  roomSettingsForm!: FormGroup;
  @Output() roomSettingsFormOnSubmit: EventEmitter<any> = new EventEmitter();
  @Output() roomSettingsChanged: EventEmitter<any> = new EventEmitter();
  @Input() numberOfGames: number = 3;
  @Input() gameOptions: FormSelectOption<number>[] = [
    { value: 0, viewValue: 'Asocijacije' },
    { value: 1, viewValue: 'Najbliži broj' },
    { value: 2, viewValue: 'Ko zna zna' },
    { value: 3, viewValue: 'Korak po korak' },
  ];

  @Input() roomState!: BehaviorSubject<RoomState>;
  @Input() isAdmin!: BehaviorSubject<boolean>;
  @Input() startEnabled!: boolean;
  nameChangedSubject: Subject<any> = new Subject();

  private isAdminSubscription!: Subscription;
  private roomStateSubscription!: Subscription;
  private nameChangedSubscription!: Subscription;

  constructor(
    private formBuilder: FormBuilder,
    public validationErrorMessageProvider: ValidationErrorMessageProviderService,
    public formInputTypesProvider: FormInputTypesProviderService
  ) {}
  ngOnDestroy(): void {
    this.isAdminSubscription.unsubscribe();
    this.roomStateSubscription.unsubscribe();
    this.nameChangedSubscription.unsubscribe();
  }

  ngOnInit(): void {
    this.roomSettingsForm = this.formBuilder.group({
      Prvaigra: [this.roomState.value.game1, Validators.required],
      Drugaigra: [this.roomState.value.game2, Validators.required],
      Trećaigra: [this.roomState.value.game3, Validators.required],
      roomname: [this.roomState.value.roomName, Validators.required],
      privateroom: [!this.roomState.value.isPublic],
    });
    this.isAdminSubscription = this.isAdmin.subscribe((isAdmin) => {
      if (isAdmin) {
        this.roomSettingsForm.controls['Prvaigra'].enable();
        this.roomSettingsForm.controls['Drugaigra'].enable();
        this.roomSettingsForm.controls['Trećaigra'].enable();
        this.roomSettingsForm.controls['roomname'].enable();
        this.roomSettingsForm.controls['privateroom'].enable();
      } else {
        this.roomSettingsForm.controls['Prvaigra'].disable();
        this.roomSettingsForm.controls['Drugaigra'].disable();
        this.roomSettingsForm.controls['Trećaigra'].disable();
        this.roomSettingsForm.controls['roomname'].disable();
        this.roomSettingsForm.controls['privateroom'].disable();
      }
    });

    this.roomStateSubscription = this.roomState.subscribe(
      (roomState: RoomState) => {
        this.roomSettingsForm.controls['Prvaigra'].patchValue(roomState.game1);
        this.roomSettingsForm.controls['Drugaigra'].patchValue(roomState.game2);
        this.roomSettingsForm.controls['Trećaigra'].patchValue(roomState.game3);
        this.roomSettingsForm.controls['roomname'].patchValue(
          roomState.roomName
        );
        this.roomSettingsForm.controls['privateroom'].patchValue(
          !roomState.isPublic
        );
      }
    );

    this.nameChangedSubscription = this.nameChangedSubject
      .pipe(debounceTime(1000))
      .subscribe(() => this.roomSettingsChanged.emit(this.getFormValues()));
  }
  getFormValues(): RoomState {
    return {
      roomId: this.roomState.value.roomId,
      roomName: this.roomSettingsForm.controls['roomname'].value,
      game1: this.roomSettingsForm.controls['Prvaigra'].value,
      game2: this.roomSettingsForm.controls['Drugaigra'].value,
      game3: this.roomSettingsForm.controls['Trećaigra'].value,
      isPublic: !this.roomSettingsForm.controls['privateroom'].value,
      users: [],
    };
  }
}
