import { Injectable } from '@angular/core';
import { MatDialogConfig } from '@angular/material/dialog';
import { MatSnackBarConfig } from '@angular/material/snack-bar';

@Injectable()
export class MaterialComponentsConfigProviderService {
  private defaultSnackbarConfig: MatSnackBarConfig = {
    horizontalPosition: 'center',
    verticalPosition: 'top',
    duration: 3000,
  };

  private defaultMatDialogConfig: MatDialogConfig = {
    panelClass: 'custom-dialog',
    maxWidth: '80%',
    maxHeight: '80%',
    minWidth: '300px',
  };

  constructor() {}

  snackbarConfig(panelClass: string[]): MatSnackBarConfig {
    return { ...this.defaultSnackbarConfig, panelClass: panelClass };
  }

  matDialogConfig(width: string): MatDialogConfig {
    return { ...this.defaultMatDialogConfig, width: width };
  }
}
