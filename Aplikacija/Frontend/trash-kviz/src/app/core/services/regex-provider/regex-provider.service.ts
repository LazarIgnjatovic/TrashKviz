import { Injectable } from '@angular/core';

@Injectable()
export class RegexProviderService {
  public readonly onlyOneCapitalOneLowercaseLetterAndOneNumber =
    '^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{8,}$';

  constructor() {}
}
