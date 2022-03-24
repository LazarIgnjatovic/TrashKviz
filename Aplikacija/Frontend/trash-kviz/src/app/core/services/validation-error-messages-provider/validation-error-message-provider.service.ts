import { Injectable } from '@angular/core';

@Injectable()
export class ValidationErrorMessageProviderService {
  public readonly usernameErrorMessages: Record<string, string> = {
    required: 'Neophodno korisničko ime',
  };
  public readonly passwordErrorMessages: Record<string, string> = {
    required: 'Neophodna lozinka',
    minlength: 'Lozinka mora biti bar 8 karaktera dugačka',
    pattern: 'Lozinka mora da sadrži malo i veliko slovo, kao i cifru',
  };

  public readonly emailErrorMessages: Record<string, string> = {
    required: 'Neophodna e-mail adresa',
    email: 'E-mail adresa mora biti u poznatom formatu (john@doe.com)',
  };

  public readonly codeErrorMessages: Record<string, string> = {
    pattern: 'Kod je kombinacija od 4 slova i/ili cifara',
  };
}
