import { Component, OnInit } from "@angular/core";
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from "@angular/material/table";
import { ConnectionEditDialogComponent } from "../../connection/components/dialogs/connection-edit-dialog.component";
import { ConnectionDataService } from "../../connection/connection-data.service";
import { IConnection } from "../../connection/models/Connection/IConnection";

@Component({
  selector: 'users-list',
  templateUrl: 'users-list.component.html',
  styleUrls: ['./users-list.component.css']
})
export class UsersListComponent implements OnInit {
  public displayedColumns: string[];
    dataSource: MatTableDataSource<IConnection>;

  constructor(public dialog: MatDialog, private _dataService: ConnectionDataService) {
    this.dataSource = new MatTableDataSource<IConnection>([]);
  }

  ngOnInit(): void {
    this._dataService.connections.subscribe(connections => {
      this.dataSource = new MatTableDataSource(connections);
    });
    this._dataService.initialize();
    this.displayedColumns = ["userName", "password", "role", "actions"];

  }
  public add() {
    let connection = {
      connectionName: "",
      dbName: "",
      password: "",
      dbType: { id: 0, name: "" },
      serverName: "",
      id: undefined,
      userName: ""
    };

    this.save(connection);
  }

  public edit(connection: IConnection) {
    this.save(connection);
  }

  private save(connection: IConnection) {
    let dialogRef = this.dialog.open(ConnectionEditDialogComponent, {
      data: {
        connection
      },
    });
  }

  private handleRemove(connection: IConnection) {
    this._dataService.deleteLocalizationKey(connection);
  }
}
