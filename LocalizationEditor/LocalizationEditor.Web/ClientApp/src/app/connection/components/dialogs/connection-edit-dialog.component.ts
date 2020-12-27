import {Component, Inject, OnInit} from "@angular/core";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {ConnectionViewComponent} from "../view/connection-view.component";
import {IConnection} from "../../models/Connection/IConnection";
import {HttpRequestService, TypedRequestImpl} from "../../../base/http-request-service";
import {DbType} from "../../models/Connection/DbType";
import {BaseServerRoutes} from "../../../base/base-server-routes";

@Component({
  styleUrls: ['./connection-edit-dialog.component.css'],
  templateUrl: "/connection-edit-dialog.component.html"
})
export class ConnectionEditDialogComponent implements OnInit {
  dialogData: IConnection;
  handler: Function;
  dbTypes: DbType[];

  constructor(
    private _httpService: HttpRequestService,
    public dialogRef: MatDialogRef<ConnectionViewComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any)
  {
    this.dialogData = data.connection;
    this.handler = data.handler;
  }

  ngOnInit(): void {
    this.getConfig();
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  onOkClick(): void{
    this.data.connection.dbType = new DbType(0, this.data.connection.dbType);
    var result = this.handler(this.data.connection);
    this.dialogRef.close(result);
  }

  private getConfig(){
    const request = new TypedRequestImpl(
      `${BaseServerRoutes.Connection}/config`,
      false,
      null,
      result => {
        this.dbTypes = result;
      }
    );
    this._httpService.get<DbType[]>(request);
  }
}
