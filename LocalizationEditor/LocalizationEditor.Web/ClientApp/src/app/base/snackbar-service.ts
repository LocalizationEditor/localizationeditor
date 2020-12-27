import {Injectable, NgZone} from "@angular/core";
import {
  MatSnackBar,
  MatSnackBarConfig,
} from "@angular/material/snack-bar";


@Injectable()
export class SnackbarService {
  constructor(private _snackBar: MatSnackBar,
              private _zone: NgZone) {
  }

  private readonly _snackbarConfig: MatSnackBarConfig = {
    duration: 500,
    horizontalPosition: 'right',
    verticalPosition: 'top'
  };

  public success(): void {
    this._zone.run(() => {
      this._snackBar.open('Success',
        '',
        {
          ...this._snackbarConfig,
          panelClass: ['success-snackbar']
        });
    });
  }

  public fail(): void {
    this._zone.run(() => {
      this._snackBar.open('Failed', '', {
        ...this._snackbarConfig,
        panelClass: ['failed-snackbar']
      });
    });

  }
}

