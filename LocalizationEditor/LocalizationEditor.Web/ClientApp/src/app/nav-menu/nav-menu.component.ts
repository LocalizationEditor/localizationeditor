import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';
import { UsersListComponent } from '../admin/usersList/users-list.component';
import { ConnectionViewComponent } from '../connection/components/view/connection-view.component';
import { ConnectionDataService } from '../connection/connection-data.service';
import { IConnection } from '../connection/models/Connection/IConnection';
import { LocalizationDataService } from '../localization-edit-dialog/localization-data.service';
import { SyncronizeComponent } from '../syncronize/components/syncronize.component';

@Component({
  selector: 'app-nav-menu',
  templateUrl: 'nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit {

  selectedConnection: IConnection;
  public connections: IConnection[] = new Array<IConnection>();
  selectedConnectionName: string;
  selectedConnectionId: number;
  isDev: boolean = +localStorage.getItem('role') === 2 || +localStorage.getItem('role') === 3;
  isAdmin: boolean = +localStorage.getItem('role') === 3;

  constructor(
    private _dialog: MatDialog,
    private _dataServce: ConnectionDataService,
    private _localizationDataService: LocalizationDataService ) {
  }

  private getConnections() {
    this._dataServce.connections.subscribe(
      connections => {
        this.connections = connections;
        
        let connectionId = localStorage.getItem("connectionId");
        if (connectionId) {
          this.updateSelected(connectionId);
          this._localizationDataService.initialize();
        }
        else {
          if (this.connections.length > 0) {
            this.updateSelected(this.connections[0].id.toString());
            this._localizationDataService.initialize();
          }
        }
      });
    this._dataServce.initialize();
  }

  ngOnInit(): void {
    this.getConnections();
  }

  public onchangedValue(value) {
    this.updateSelected(value.value);
    location.reload();
  }

  syncLocalization() {
    const dialogRef = this._dialog.open(SyncronizeComponent, {
      width: '90%',
    });
    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }

  showUserManagment() {
    const dialogRef = this._dialog.open(UsersListComponent, {
      width: '90%',
    });
    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }

  

  showConnections() {
    const dialogRef = this._dialog.open(ConnectionViewComponent, {
      width: '90%'
    });
    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }

  private updateSelected(connectionId: string) {
    this.selectedConnection = this.connections.find(i => i.id.toString() == connectionId);
    if (this.selectedConnection) {
      this.selectedConnectionId = this.selectedConnection.id;
      this.selectedConnectionName = this.selectedConnection.connectionName;
      localStorage.setItem("connectionId", this.selectedConnectionId.toString());
    }
  }
}
