import {Component, OnInit} from "@angular/core";
import {IConnection} from "../../models/Connection/IConnection";
import {MatDialog} from '@angular/material/dialog';
import {ConnectionEditDialogComponent} from "../dialogs/connection-edit-dialog.component";
import {dialogConfig} from "../dialogs/dialog-config";
import {HttpRequestService, TypedRequestImpl} from "../../../base/http-request-service";
import {BaseServerRoutes} from "../../../base/base-server-routes";

@Component({
  selector: 'connection-view',
  templateUrl: '/connection-view.component.html',
  styleUrls: ['./connection-view.component.css']
})
export class ConnectionViewComponent implements OnInit {
  public connections: IConnection[] = new Array<IConnection>();
  private displayedColumns: string[];

  constructor(public dialog: MatDialog,
              private _httpService: HttpRequestService) {
  }

  ngOnInit(): void {
    this.getConnections();
    this.displayedColumns = ["connectionName", "dbName", "userName", "serverName", "password", "dbType", "actions"];
  }

  private onEdit(connection: IConnection) {
    const indexBefore = this.connections
      .findIndex(item => item.connectionName === connection.connectionName);

    let dialogRef = this.dialog.open(ConnectionEditDialogComponent, {
      ...dialogConfig,
      data: {
        connection},
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined) {
        console.log(this.connections);
        this.connections[indexBefore] = result;
      }
    });
  }

  private onAdd(connection: IConnection) {
    connection = <IConnection>{};

    let dialogRef = this.dialog.open(ConnectionEditDialogComponent, {
      ...dialogConfig,
      data: {
        connection},
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(result);
      if (result !== undefined)
        this.connections.push(result);
    });
  }

  private handleRemove(connection: IConnection) {
    const index = this.connections.findIndex(item => item.id === connection.id);
    const request = new TypedRequestImpl<number>(
      `${BaseServerRoutes.Connection}/${connection.id}`,
      true,
      null,
      result => {
        this.connections.splice(index, 1);
      });

    this._httpService.delete<number>(request);
  }

  private getConnections() {
    const request = new TypedRequestImpl<IConnection[]>(
      `${BaseServerRoutes.Connection}`,
      false,
      null,
      result => {
        this.connections = result;
      });

    this._httpService.get<IConnection[]>(request);
  }
}
