import { HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';
import { map, Observable, tap } from 'rxjs';
import { Login } from 'src/app/modules/user/dtos/login.dto';
import { Register } from 'src/app/modules/user/dtos/register.dto';
import { HttpGeneralService } from '../http-general/http-general.service';
import { MaterialComponentsConfigProviderService } from '../material-components-config-provider/material-components-config-provider.service';

export interface Response {
  message: string;
}

@Injectable()
export class AuthService {
  snackbarSettings: MatSnackBarConfig;
  constructor(
    private httpGeneral: HttpGeneralService,
    private snackbar: MatSnackBar,
    private materialComponentConfigProvider: MaterialComponentsConfigProviderService
  ) {
    this.snackbarSettings = this.materialComponentConfigProvider.snackbarConfig(
      ['mat-toolbar', 'mat-accent', 'center-snackbar']
    );
  }

  isLoggedIn(): Observable<boolean> {
    return this.httpGeneral
      .get<boolean>('/auth/isloggedin')
      .pipe(
        map((response) =>
          response.body === null || !response.ok! ? false : response.body
        )
      );
  }

  login(user: Login): Observable<boolean> {
    return this.httpGeneral.post<Response>('/auth/login', user).pipe(
      tap((response) => this.showMessageOnSuccess(response)),
      map((response) => response.ok)
    );
  }

  register(user: Register): Observable<boolean> {
    return this.httpGeneral.post<Response>('/auth/register', user).pipe(
      tap((response) => this.showMessageOnSuccess(response)),
      map((response) => response.ok)
    );
  }

  logout(): Observable<boolean> {
    return this.httpGeneral.get<Response>('/auth/logout').pipe(
      tap((response) => this.showMessageOnSuccess(response)),
      map((response) => response.ok)
    );
  }

  private showMessageOnSuccess(response: HttpResponse<Response>): void {
    if (response.ok && response.body?.message) {
      this.snackbar.open(
        response.body?.message,
        undefined,
        this.snackbarSettings
      );
    }
  }
}
