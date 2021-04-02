import { Component, Inject, OnInit } from "@angular/core";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { BaseServerRoutes } from "../../base/base-server-routes";
import { HttpRequestService, TypedRequestImpl } from "../../base/http-request-service";
import { IUser } from "../usersList/IUser";
import { Role } from "../usersList/Role";
import { UsersDataService } from "../usersList/users-data.service";
import { UsersListComponent } from "../usersList/users-list.component";


@Component({
  styleUrls: ['./connection-edit-dialog.component.css'],
  templateUrl: "connection-edit-dialog.component.html"
})
export class UserEditDialogComponent implements OnInit {
  dialogData: IUser;
  dbTypes: Role[];


  constructor(
    private _httpService: HttpRequestService,
    public dialogRef: MatDialogRef<UsersListComponent>,
    private _dataServce: UsersDataService,
    @Inject(MAT_DIALOG_DATA) public data: any) {
  }

  ngOnInit(): void {
    this.getConfig();
    this.dialogData = this.data.connection;
    if (!this.dialogData) {
      this.dialogData = {
        password: "",
        role: { id: 0, name: "" },
        id: undefined,
        userName: ""
      };
    }
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

  onOkClick(): void {
    this.dialogData.role = new Role(0, this.dialogData.role.name);
    if (!this.dialogData.id)
      this.handleAdd(this.dialogData);
    else this.handleEdit(this.dialogData);

    this.dialogRef.close();
  }

  private getConfig() {
    const request = new TypedRequestImpl(
      `${BaseServerRoutes.Users}/config`,
      false,
      null,
      result => {
        this.dbTypes = result;
      }
    );
    this._httpService.get<Role[]>(request);
  }

  private handleAdd(user: IUser) {
    this._dataServce.save(user);
    this.dialogRef.close(user);
  }

  private handleEdit(user: IUser) {
    this._dataServce.save(user);
    this.dialogRef.close(user);
  }
}
