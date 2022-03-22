import { Injectable } from '@angular/core';

enum FormInputTypes {
  text = 'text',
  password = 'password',
  textArea = 'textArea',
  number = 'number',
}

@Injectable({
  providedIn: 'root',
})
export class FormInputTypesProviderService {
  formInputTypes = FormInputTypes;
  constructor() {}
}
