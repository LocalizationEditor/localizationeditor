import {Component, OnInit, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {ConnectionDialogData} from "../connection-data/connectionDialogData";

@Component({
  selector: 'connection-delete-dialog',
  templateUrl: './connection-delete-dialog.html'
})
export class ConnectionDeleteDialog implements OnInit {

  constructor(public dialogRef: MatDialogRef<ConnectionDeleteDialog>,
              @Inject(MAT_DIALOG_DATA) public data: ConnectionDialogData) {
  }

  ngOnInit() {
  }

  onNoClick() {
    this.dialogRef.close();
  }
}
