import {Component} from '@angular/core';
import {Connection} from "../connection-data/connection";

@Component({
  styleUrls: ['../base/connection.css'],
  selector: 'connection-create',
  templateUrl: 'connection-create.html'
})
export class ConnectionCreate {
  connection: Connection;

  constructor() {
    this.connection = new Connection(0, "", "");
  }

  create(name: string, connectionStr: string) {
    this.connection.name = name;
    this.connection.connectionString = connectionStr;
    alert("create");
  }
}
