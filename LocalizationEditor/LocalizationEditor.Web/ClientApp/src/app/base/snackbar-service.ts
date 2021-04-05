import { Injectable, NgZone } from "@angular/core";
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
    duration: 3000,
    horizontalPosition: 'right',
    verticalPosition: 'top'
  };

  public success(text?: string): void {
    this._zone.run(() => {
      this._snackBar.open(!text ? 'Success' : text,
        '',
        {
          ...this._snackbarConfig,
        });
    });
  }

  public fail(): void {
    this._zone.run(() => {
      this._snackBar.open('Request failed', '', {
        ...this._snackbarConfig,
      });
    });

  }
}

