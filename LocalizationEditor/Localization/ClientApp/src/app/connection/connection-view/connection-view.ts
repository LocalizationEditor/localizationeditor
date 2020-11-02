import {Component, NgModule} from '@angular/core';
import {Connection} from "../connection-data/connection";

export const Connections: Connection[] = [
  new Connection(1, "1 Connection", "connetion string bla bla bla bla blabla bla bla bla bla"),
  new Connection(2, "2 Connection", "connetion string bla bla bla bla blabla bla bla bla bla"),
  new Connection(3, "3 Connection", "connetion string bla bla bla bla blabla bla bla bla bla"),
  new Connection(4, "4 Connection", "connetion string bla bla bla bla blabla bla bla bla bla"),
];

@Component({
  templateUrl: 'connection-view.html',
  styleUrls: ['./connection-view.css', '../base/connection.css']
})
export class ConnectionStringView {
  connections: Connection[] = Connections;
}
