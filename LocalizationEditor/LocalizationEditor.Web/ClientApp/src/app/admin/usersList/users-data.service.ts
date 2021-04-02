import { BehaviorSubject } from "rxjs";
import { Injectable } from "@angular/core";
import { IUser } from "./IUser";
import { HttpRequestService, TypedRequestImpl } from "../../base/http-request-service";
import { BaseServerRoutes } from "../../base/base-server-routes";

@Injectable()
export class UsersDataService {

  public users: BehaviorSubject<IUser[]> = new BehaviorSubject([]);
  public totalCount: BehaviorSubject<number> = new BehaviorSubject(0);
  private _rows: IUser[] = [];

  constructor(private _httpService: HttpRequestService) {
  }

  public initialize(): void {
    this.loadListMore();
  }

  public save(user: IUser): void {
    const request = new TypedRequestImpl(
      user.id ?
        `${BaseServerRoutes.Users}/${user.id}` :
        `${BaseServerRoutes.Users}`,
      true,
      user,
      result => {
        if (result.id && !user.id) {
          this._rows.push(result);
          this.users.next(this._rows);
        }
      });
    if (user.id)
      this._httpService.put(request);
    else
      this._httpService.post(request)
  }

  public loadListMore() {
    const request = new TypedRequestImpl<IUser[]>(
      `${BaseServerRoutes.Users}`,
      false,
      null,
      result => {
        this._rows = result;
        this.users.next(this._rows);
      });

    this._httpService.get<IUser[]>(request);
  }

  public deleteLocalizationKey(connection: IUser) {
    const request = new TypedRequestImpl<number>(
      `${BaseServerRoutes.Users}/${connection.id}`,
      true,
      null,
      result => {
        const index = this._rows.indexOf(connection);
        this._rows.splice(index, 1);
        this.users.next(this._rows);
      });

    this._httpService.delete<number>(request);
  }
}
