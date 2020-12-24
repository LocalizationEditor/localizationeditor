import {Injectable, NgZone} from "@angular/core";
import {
  MatSnackBar,
} from "@angular/material/snack-bar";


@Injectable()
 export class SnackbarService {
  constructor( private _snackBar: MatSnackBar,
               private _zone: NgZone) {
  }

  public success():void{
    this._zone.run(() => {
      this._snackBar.open('Success', '', {
        duration: 500,
        horizontalPosition: 'right',
        verticalPosition: 'top',
        panelClass: ['success-snackbar']
      });
    });
  }

  public fail():void{
    this._zone.run(() => {
      this._snackBar.open('Failed', '', {
        duration: 500,
        horizontalPosition: 'right',
        verticalPosition: 'top',
        panelClass: ['failed-snackbar']
      });
    });

  }
}

