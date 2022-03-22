import { Component, Inject, Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import {
  MatSnackBar,
  MatSnackBarConfig,
  MAT_SNACK_BAR_DATA,
} from '@angular/material/snack-bar';
import { MaterialComponentsConfigProviderService } from '../../services/material-components-config-provider/material-components-config-provider.service';

@Injectable()
export class ErrorHandlerInterceptor implements HttpInterceptor {
  constructor(
    private snackBar: MatSnackBar,
    private materialComponentConfigProvider: MaterialComponentsConfigProviderService
  ) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        let errorMsg = '';

        const snackbarSettings: MatSnackBarConfig =
          this.materialComponentConfigProvider.snackbarConfig([
            'mat-toolbar',
            'mat-primary',
          ]);

        if (error instanceof Error) {
          errorMsg = `Developer dunđer nešto stikao, prenesi mu ovo: ${error}`;
          this.snackBar.open(errorMsg, undefined, snackbarSettings);
        } else {
          errorMsg = `Error Code: ${error.status};\nMessage: ${error.message}`;
          this.snackBar.openFromComponent(NotificationComponent, {
            ...snackbarSettings,
            data: {
              firstLine: 'Server kaže:',
              secondLine:
                Object.keys(error.error).length == 1 &&
                Object.keys(error.error).includes('message')
                  ? error.error.message
                  : error.error,
            },
          });
        }

        return throwError(() => new Error(errorMsg));
      })
    );
  }
}

@Component({
  template:
    '<div> <span>{{data.firstLine}}</span>  <span>{{data.secondLine}} </span></div>',
  styles: [
    'div {display:flex; flex-direction: column; align-items:center} span{text-align: center}',
  ],
})
class NotificationComponent {
  constructor(@Inject(MAT_SNACK_BAR_DATA) public data: any) {}
}
