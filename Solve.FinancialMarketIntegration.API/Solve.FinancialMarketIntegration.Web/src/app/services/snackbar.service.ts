import { Injectable } from '@angular/core';
import { NgxUiLoaderConfig, NgxUiLoaderService } from 'ngx-ui-loader';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material';


@Injectable()
export class SnackbarService {

  private config: MatSnackBarConfig;

  constructor(private snackBar: MatSnackBar) {
    this.config = {
        verticalPosition: 'top',
        horizontalPosition: 'right',
        panelClass: 'snackbar-theme',
        duration: 5000
    };
  }

  /**
   * @description Open angular material snackbar
   * @param message to be display
   * @param action of button
   * @param config snackbar configuration
   */
  open(message: string, action: string, config: MatSnackBarConfig = this.config) {
    const configuration = Object.assign(this.config, config);
    return this.snackBar.open(message, action, configuration);
  }
}
