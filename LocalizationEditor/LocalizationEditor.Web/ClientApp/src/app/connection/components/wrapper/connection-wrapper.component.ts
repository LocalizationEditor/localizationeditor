import { AfterViewInit, Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import { IConnection } from "../../models/Connection/IConnection";
import { HttpRequestService, TypedRequestImpl } from "../../../base/http-request-service";
import { BaseServerRoutes } from "../../../base/base-server-routes";
import { FormControl, Validators } from "@angular/forms";

export class ConnectionHelper {
  public DESTINATION_KEY: string = "destination";
  public SOURCE_KEY: string = "source";
}

@Component({
  styleUrls: ['connection-wrapper.component.css'],
  selector: 'connection-wrapper',
  templateUrl: '/connection-wrapper.component.html'
})
export class ConnectionWrapperComponent implements OnInit, AfterViewInit {
  @Input() name: string;
  @Input() key: string;
  @Output() connectionSelected = new EventEmitter<string>();

  public connections: IConnection[] = new Array<IConnection>();

  selectControl = new FormControl('', Validators.required);

  constructor(private _httpClient: HttpRequestService) {
  }

  ngAfterViewInit(): void {
    localStorage.removeItem(this.name);
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
  }

  private sendOnChange($event): void {
    var dbConnectionId = $event.value.toString();
    localStorage.removeItem(this.key);
    localStorage.setItem(this.key, dbConnectionId);
    this.connectionSelected.emit(dbConnectionId);
  }
}
