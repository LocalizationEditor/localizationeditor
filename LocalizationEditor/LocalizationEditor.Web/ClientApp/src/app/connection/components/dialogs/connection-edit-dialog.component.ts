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
  dbTypes: DbType[];
  private readonly matDialogStyle: object = {
    'display': 'flex',
    'justify-content': 'center',
    'flex-direction': 'column',
    'align-items': 'center'
  };

  constructor(
    private _httpService: HttpRequestService,
    public dialogRef: MatDialogRef<ConnectionViewComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any) {
    this.dialogData = data.connection;
  }

  ngOnInit(): void {
    this.getConfig();
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  onOkClick(): void {
    this.data.connection.dbType = new DbType(0, this.data.connection.dbType);
    if (this.data.connection.id === undefined)
      this.handleAdd(this.data.connection);
    else this.handleEdit(this.data.connection);

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
    const request = new TypedRequestImpl<IConnection>(
      `${BaseServerRoutes.Connection}`,
      true,
      connection,
      result => {
        this.dialogRef.close(result);
      });

    this._httpService.post<IConnection>(request);
  }

  private handleEdit(connection: IConnection) {
    const request = new TypedRequestImpl<IConnection>(
      `${BaseServerRoutes.Connection}/${connection.id}`,
      true,
      connection,
      result => {
        this.dialogRef.close(result);
      });

    this._httpService.put<IConnection>(request);
  }
}
