import {Component} from '@angular/core';
import {ActivatedRoute, Params} from '@angular/router';
import {Subscription} from "rxjs";
import {Connections} from '../connection-view/connection-view';
import {Connection} from "../connection-data/connection";
import {ConnectionValidator} from "../connection-validator/connectionValidator";
import {ConnectionClientHandler} from "../connection-client-handler/connection-client-handler";
import {EntityState} from "../../base/entity-state";

@Component({
  styleUrls: ['connection-edit.css', '../base/connection.css'],
  selector: 'connection-edit',
  templateUrl: '../connection-create/connection-create.html'
})
export class ConnectionEdit {
  private subscription: Subscription;
  title:string = "Edit page";
  connectionId: number;
  connection: Connection;
  validator: ConnectionValidator;
  handler: ConnectionClientHandler;

  constructor(private activateRoute: ActivatedRoute) {
    this.handler = new ConnectionClientHandler();
    this.validator = new ConnectionValidator(256, 1);
    this.subscription = activateRoute.params
      .subscribe(params => this.subscriptionInvoke(params));
  }

  handle(name: string, connectionStr: string): void {
    this.connection = new Connection(this.connectionId, name, connectionStr);
    this.connection.setState(EntityState.Update);
    this.handler.handle(this.connection);
  }

  private subscriptionInvoke(params: Params): void {
    this.connectionId = params['id'];
    this.connection = Connections.find(value => value.id == this.connectionId);
  }
}
