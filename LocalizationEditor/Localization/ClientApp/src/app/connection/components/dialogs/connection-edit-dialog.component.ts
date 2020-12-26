import {Component, Inject} from "@angular/core";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {ConnectionViewComponent} from "../view/connection-view.component";
import {IConnection} from "../../models/Connection/IConnection";

@Component({
  styleUrls: ['./connection-edit-dialog.component.css'],
  templateUrl: "/connection-edit-dialog.component.html"
})
export class ConnectionEditDialogComponent {
  dialogData: IConnection;
  handler: Function;

  constructor(
    public dialogRef: MatDialogRef<ConnectionViewComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any)
  {
    console.log(data);
    this.dialogData = data.connection;
    this.handler = data.handler;
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  onOkClick(): void{
    var result = this.handler(this.data);
    this.dialogRef.close(result);
  }
}
