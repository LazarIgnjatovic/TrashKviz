import { Injectable } from '@angular/core';

@Injectable()
export class RegexProviderService {
  public readonly onlyOneCapitalOneLowercaseLetterAndOneNumberLenght8Plus =
    '^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,}$';
  public readonly onlyCapitalLettersAndNumbersLength4 = '^[A-Za-z0-9]{4}$';
  public readonly onlyLettersAndNumbers = '^[A-Za-z0-9 ]*$';
  constructor() {}
}
