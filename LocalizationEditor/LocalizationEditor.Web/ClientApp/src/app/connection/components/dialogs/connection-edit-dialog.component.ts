import { Component, Inject, OnInit } from "@angular/core";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { ConnectionViewComponent } from "../view/connection-view.component";
import { IConnection } from "../../models/Connection/IConnection";
import { HttpRequestService, TypedRequestImpl } from "../../../base/http-request-service";
import { DbType } from "../../models/Connection/DbType";
import { BaseServerRoutes } from "../../../base/base-server-routes";
import { ConnectionDataService } from "../../connection-data.service";

@Component({
  styleUrls: ['./connection-edit-dialog.component.css'],
  templateUrl: "/connection-edit-dialog.component.html"
})
export class ConnectionEditDialogComponent implements OnInit {
  dialogData: IConnection;
  dbTypes: DbType[];


  constructor(
    private _httpService: HttpRequestService,
    public dialogRef: MatDialogRef<ConnectionViewComponent>,
    private _dataServce: ConnectionDataService,
    @Inject(MAT_DIALOG_DATA) public data: any) {
  }

  ngOnInit(): void {
    this.getConfig();
    this.dialogData = this.data.connection;
    if (!this.dialogData) {
      this.dialogData = {
        connectionName: "",
        dbName: "",
        password: "",
        dbType: { id: 0, name: "" },
        serverName: "",
        id: undefined,
        userName: ""
      };
    }
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  onOkClick(): void {
    this.dialogData.dbType = new DbType(0, this.dialogData.dbType.name);
    if (!this.dialogData.id)
      this.handleAdd(this.dialogData);
    else this.handleEdit(this.dialogData);

    this.dialogRef.close();
  }

  private getConfig() {
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

  private handleAdd(connection: IConnection) {
    connection
    this._dataServce.save(connection);
    this.dialogRef.close(connection);
  }

  private handleEdit(connection: IConnection) {
    this._dataServce.save(connection);
    this.dialogRef.close(connection);
  }
}
