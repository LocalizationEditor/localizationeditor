import { Component, OnInit } from "@angular/core";
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from "@angular/material/table";
import { ConnectionEditDialogComponent } from "../../connection/components/dialogs/connection-edit-dialog.component";
import { IUser } from "./IUser";
import { UsersDataService } from "./users-data.service";

@Component({
  selector: 'users-list',
  templateUrl: 'users-list.component.html',
  styleUrls: ['./users-list.component.css']
})
export class UsersListComponent implements OnInit {
  public displayedColumns: string[];
  dataSource: MatTableDataSource<IUser>;

  constructor(public dialog: MatDialog, private _dataService: UsersDataService) {
    this.dataSource = new MatTableDataSource<IUser>([]);
  }

  ngOnInit(): void {
    this._dataService.users.subscribe(connections => {
      this.dataSource = new MatTableDataSource(connections);
    });
    this._dataService.initialize();
    this.displayedColumns = ["userName", "password", "role", "actions"];

  }
  public add() {
    let connection = {
      password: "",
      role: { id: 0, name: "" },
      id: undefined,
      userName: ""
    };

    this.save(connection);
  }

  public edit(connection: IUser) {
    this.save(connection);
  }

  private save(connection: IUser) {
    let dialogRef = this.dialog.open(ConnectionEditDialogComponent, {
      data: {
        connection
      },
    });
  }

  private handleRemove(connection: IUser) {
    this._dataService.deleteLocalizationKey(connection);
  }
}
