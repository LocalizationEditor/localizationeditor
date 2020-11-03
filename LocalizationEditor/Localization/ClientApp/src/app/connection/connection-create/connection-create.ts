import {Component} from '@angular/core';
import {Connection} from "../connection-data/connection";
import {Validator} from "../connection-validator/validator";

@Component({
  styleUrls: ['../base/connection.css'],
  selector: 'connection-create',
  templateUrl: 'connection-create.html'
})
export class ConnectionCreate {
  connection: Connection;
  validator: Validator;

  constructor() {
    this.connection = new Connection(0, "", "");
    this.validator = new Validator();
  }

  create(name: string, connectionStr: string) {
    this.connection.name = name;
    this.connection.connectionString = connectionStr;
    alert("create");
  }
}
