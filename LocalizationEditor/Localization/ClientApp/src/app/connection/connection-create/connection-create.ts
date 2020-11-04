import {Component} from '@angular/core';
import {Connection} from "../connection-data/connection";
import {ConnectionValidator} from "../connection-validator/connectionValidator";
import {ConnectionState} from "../connection-data/connection-state";
import {ConnectionClientHandler} from "../connection-client-handler/connection-client-handler";

@Component({
  styleUrls: ['../base/connection.css'],
  selector: 'connection-create',
  templateUrl: 'connection-create.html'
})
export class ConnectionCreate {
  title:string = "Create page";
  connection: Connection;
  validator: ConnectionValidator;
  handler: ConnectionClientHandler;


  constructor() {
    this.handler = new ConnectionClientHandler();
    this.connection = new Connection(0, "", "");
    this.validator = new ConnectionValidator(256, 1);
  }

  handle(name: string, connectionStr: string): void {
    alert("click");
    this.connection.name = name;
    this.connection.connectionString = connectionStr;
    this.connection.setState(ConnectionState.Add);
    this.handler.handle(this.connection);
  }
}
