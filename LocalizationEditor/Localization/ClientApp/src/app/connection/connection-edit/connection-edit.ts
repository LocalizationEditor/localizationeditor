import {Component} from '@angular/core';
import {ActivatedRoute, Params} from '@angular/router';
import {Subscription} from "rxjs";
import {Connections} from '../connection-view/connection-view';
import {Connection} from "../connection-data/connection";

@Component({
  styleUrls: ['connection-edit.css', '../base/connection.css'],
  selector: 'connection-edit',
  templateUrl: 'connection-edit.html'
})
export class ConnectionEdit {
  private subscription: Subscription;

  connectionId: number;
  connection: Connection;

  constructor(private activateRoute: ActivatedRoute) {
    this.subscription = activateRoute.params
      .subscribe(params => this.subscriptionInvoke(params));
  }

  update(text:string):void{
    alert(text);
  }

  private subscriptionInvoke(params: Params): void {
    this.connectionId = params['id'];
    this.connection = Connections.find(value => value.id == this.connectionId);
  }
}
