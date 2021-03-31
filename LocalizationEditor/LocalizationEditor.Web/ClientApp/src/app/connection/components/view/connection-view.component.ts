import { Component, OnInit } from "@angular/core";
import { IConnection } from "../../models/Connection/IConnection";
import { MatDialog } from '@angular/material/dialog';
import { ConnectionEditDialogComponent } from "../dialogs/connection-edit-dialog.component";
import { ConnectionDataService } from "../../connection-data.service";
import { MatTableDataSource } from "@angular/material/table";

@Component({
  selector: 'connection-view',
  templateUrl: '/connection-view.component.html',
  styleUrls: ['./connection-view.component.css']
})
export class ConnectionViewComponent implements OnInit {
  public dataSource: MatTableDataSource<IConnection>;
  private displayedColumns: string[];

  constructor(public dialog: MatDialog,
    private _dataServce: ConnectionDataService) {
    this.dataSource = new MatTableDataSource();
  }

  ngOnInit(): void {
    this._dataServce.connections.subscribe(connections => {
      this.dataSource = new MatTableDataSource(connections);
    });
    this._dataServce.initialize();
    this.displayedColumns = ["connectionName", "dbName", "userName", "serverName", "password", "dbType", "actions"];

  }

  private save(connection: IConnection) {
    let dialogRef = this.dialog.open(ConnectionEditDialogComponent, {
      data: { connection },
    });
  }

  private handleRemove(connection: IConnection) {
    this._dataServce.deleteLocalizationKey(connection);
  }
}
