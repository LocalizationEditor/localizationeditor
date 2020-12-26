import {Component, OnInit} from "@angular/core";
import {IConnection} from "../../models/Connection/IConnection";
import {MatDialog} from '@angular/material/dialog';
import {ConnectionEditDialogComponent} from "../dialogs/connection-edit-dialog.component";
import {dialogConfig} from "../dialogs/dialog-config";

@Component({
  selector: 'connection-view',
  templateUrl: '/connection-view.component.html',
  styleUrls: ['./connection-view.component.css']
})
export class ConnectionViewComponent implements OnInit {
  connections: IConnection[];
  private displayedColumns: string[];

  constructor(public dialog: MatDialog) {
  }

  ngOnInit(): void { // todo add request to server
    this.connections = [{
      userName: "asd",
      serverName: "asd",
      password: "asd",
      dbType: "asd",
      dbName: "asd",
      connectionName: "asd"
    }, {
      userName: "asd",
      serverName: "asd",
      password: "asd",
      dbType: "asd",
      dbName: "asd",
      connectionName: "asd"
    }, {
      userName: "asd",
      serverName: "asd",
      password: "asd",
      dbType: "asd",
      dbName: "asd",
      connectionName: "asd"
    },
      {
        userName: "asd",
        serverName: "asd",
        password: "asd",
        dbType: "asd",
        dbName: "asd",
        connectionName: "asd"
      }, {
        userName: "asd",
        serverName: "asd",
        password: "asd",
        dbType: "asd",
        dbName: "asd",
        connectionName: "asd"
      }, {
        userName: "asd",
        serverName: "asd",
        password: "asd",
        dbType: "asd",
        dbName: "asd",
        connectionName: "asd"
      }, {
        userName: "asd",
        serverName: "asd",
        password: "asd",
        dbType: "asd",
        dbName: "asd",
        connectionName: "asd"
      },
      {
        userName: "asd",
        serverName: "asd",
        password: "asd",
        dbType: "asd",
        dbName: "asd",
        connectionName: "asd"
      }, {
        userName: "asd",
        serverName: "asd",
        password: "asd",
        dbType: "asd",
        dbName: "asd",
        connectionName: "asd"
      }, {
        userName: "asd",
        serverName: "asd",
        password: "asd",
        dbType: "asd",
        dbName: "asd",
        connectionName: "asd"
      }, {
        userName: "asd",
        serverName: "asd",
        password: "asd",
        dbType: "asd",
        dbName: "asd",
        connectionName: "asd"
      },
      {
        userName: "asd",
        serverName: "asd",
        password: "asd",
        dbType: "asd",
        dbName: "asd",
        connectionName: "asd"
      }, {
        userName: "asd",
        serverName: "asd",
        password: "asd",
        dbType: "asd",
        dbName: "asd",
        connectionName: "asd"
      }, {
        userName: "asd",
        serverName: "asd",
        password: "asd",
        dbType: "asd",
        dbName: "asd",
        connectionName: "asd"
      }, {
        userName: "asd",
        serverName: "asd",
        password: "asd",
        dbType: "asd",
        dbName: "asd",
        connectionName: "asd"
      },
      {
        userName: "asd",
        serverName: "asd",
        password: "asd",
        dbType: "asd",
        dbName: "asd",
        connectionName: "asd"
      }, {
        userName: "asd",
        serverName: "asd",
        password: "asd",
        dbType: "asd",
        dbName: "asd",
        connectionName: "asd"
      }, {
        userName: "asd",
        serverName: "asd",
        password: "asd",
        dbType: "asd",
        dbName: "asd",
        connectionName: "asd"
      }, {
        userName: "asd",
        serverName: "asd",
        password: "asd",
        dbType: "asd",
        dbName: "asd",
        connectionName: "asd"
      },
      {
        userName: "asd",
        serverName: "asd",
        password: "asd",
        dbType: "asd",
        dbName: "asd",
        connectionName: "asd"
      }, {
        userName: "asd",
        serverName: "asd",
        password: "asd",
        dbType: "asd",
        dbName: "asd",
        connectionName: "asd"
      }, {
        userName: "asd",
        serverName: "asd",
        password: "asd",
        dbType: "asd",
        dbName: "asd",
        connectionName: "asd"
      }, {
        userName: "asd",
        serverName: "asd",
        password: "asd",
        dbType: "asd",
        dbName: "asd",
        connectionName: "asd"
      },
      {
        userName: "asd",
        serverName: "asd",
        password: "asd",
        dbType: "asd",
        dbName: "asd",
        connectionName: "asd"
      }, {
        userName: "asd",
        serverName: "asd",
        password: "asd",
        dbType: "asd",
        dbName: "asd",
        connectionName: "asd"
      }, {
        userName: "asd",
        serverName: "asd",
        password: "asd",
        dbType: "asd",
        dbName: "asd",
        connectionName: "asd"
      }];

    this.displayedColumns = [...Object.keys(this.connections[0]).reverse(), "actions"];

    console.log(this.displayedColumns)
  }

  private onEdit(connection: IConnection) {
    const indexBefore = this.connections
      .findIndex(item => item.connectionName === connection.connectionName);

    let dialogRef = this.dialog.open(ConnectionEditDialogComponent, {
      ...dialogConfig,
      data: {
        connection,
        handler: this.handleEdit
      },
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result !== undefined)
        this.connections[indexBefore] = result;
    });
  }

  private onAdd(connection: IConnection)
  {
    console.log("IN addd");
    connection = <IConnection>{};

    let dialogRef = this.dialog.open(ConnectionEditDialogComponent, {
      ...dialogConfig,
      data: {
        connection,
        handler: this.handleAdd
      },
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result !== undefined)
        this.connections.push(result);
    });
  }

  private handleRemove(connection: IConnection) {

  }

  private handleEdit(connection: IConnection): IConnection {
    //todo send put to server
    return null;
  }

  private handleAdd(connection: IConnection): IConnection{
    //todo send post to server
    return null;
  }
}
