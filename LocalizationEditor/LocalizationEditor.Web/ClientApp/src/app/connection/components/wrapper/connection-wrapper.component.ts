import {Component, EventEmitter, Input, OnInit, Output} from "@angular/core";
import {IConnection} from "../../models/Connection/IConnection";
import {FormControl, Validators} from "@angular/forms";
import {HttpRequestService, TypedRequestImpl} from "../../../base/http-request-service";
import {BaseServerRoutes} from "../../../base/base-server-routes";

@Component({
  styleUrls: ['connection-wrapper.component.css'],
  selector: 'connection-wrapper',
  templateUrl: '/connection-wrapper.component.html'
})
export class ConnectionWrapperComponent implements OnInit {
  connectionControl = new FormControl('', Validators.required);
  @Input() selectedConnection: IConnection;
  public connections: IConnection[] = new Array<IConnection>();
  @Output() selectedConnectionEmitter = new EventEmitter<IConnection>();

  constructor(private _httpClient: HttpRequestService) {
  }

  ngOnInit(): void {
    const setConnections = this.setConnections.bind(this);

    const request = new TypedRequestImpl<IConnection[]>(
      `${BaseServerRoutes.Connection}`,
      false,
      null,
      setConnections);

    this._httpClient.get<IConnection[]>(request);
  }

  private setConnections(result: IConnection[]): void {
    console.log(this);
    this.connections = result;
    this.selectedConnection = result[0];
  }

  private sendOnChange(): void {
    console.log("sendMessage");
    this.selectedConnectionEmitter.emit(this.selectedConnection);
  }
}
