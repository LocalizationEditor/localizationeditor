import { BaseServerRoutes } from "../base/base-server-routes";
import { HttpRequestService, TypedRequestImpl } from "../base/http-request-service";
import { BehaviorSubject } from "rxjs";
import { Injectable } from "@angular/core";
import { IConnection } from "./models/Connection/IConnection";

@Injectable()
export class ConnectionDataService {

  public connections: BehaviorSubject<IConnection[]> = new BehaviorSubject([]);
  public totalCount: BehaviorSubject<number> = new BehaviorSubject(0);
  private _rows: IConnection[] = [];

  constructor(private _httpService: HttpRequestService) {
  }

  public initialize(): void {
    this.loadListMore();
  }

  public save(connection: IConnection): void {
    const request = new TypedRequestImpl(
      connection.id ?
        `${BaseServerRoutes.Connection}/${connection.id}` :
        `${BaseServerRoutes.Connection}`,
      true,
      connection,
      result => {
        if (result.id && !connection.id) {
          this._rows.push(result);
          this.connections.next(this._rows);
        }
      });
    if (connection.id)
      this._httpService.put(request);
    else
      this._httpService.post(request)
  }

  public loadListMore() {
    const request = new TypedRequestImpl<IConnection[]>(
      `${BaseServerRoutes.Connection}`,
      false,
      null,
      result => {
        this._rows = result;
        this.connections.next(this._rows);
      });

    this._httpService.get<IConnection[]>(request);
  }

  public deleteLocalizationKey(connection: IConnection) {
    const request = new TypedRequestImpl<number>(
      `${BaseServerRoutes.Connection}/${connection.id}`,
      true,
      null,
      result => {
        const index = this._rows.indexOf(connection);
        this._rows.splice(index, 1);
        this.connections.next(this._rows);
      });

    this._httpService.delete<number>(request);
  }
}
