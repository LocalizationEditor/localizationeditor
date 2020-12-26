﻿import {Inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
// import {SnackbarService} from "./snackbar-service";
import {Observable} from "rxjs";
// private _snackBar: SnackbarService
@Injectable({
  providedIn: 'root'
})
export class HttpRequestService {
  constructor(private httpClient: HttpClient,
              @Inject('BASE_URL') private  baseUrl: string
              ) {
  }

  public get<T>(requestObj: ITypedRequest<T>) {
    const request = this.httpClient.get<T>(this.getRequestString(requestObj));
    this.processRequest(request, requestObj);
  }

  public delete<T>(requestObj: ITypedRequest<T>) {
    const request = this.httpClient.delete<T>(this.getRequestString(requestObj));
    this.processRequest(request, requestObj);
  }

  public post<T>(requestConfig: ITypedRequest<T>) {
    const request = this.httpClient.post<T>(this.getRequestString(requestConfig), requestConfig.RequestObject)
    this.processRequest(request, requestConfig);
  }

  public put<T>(requestConfig: ITypedRequest<T>) {
    const request = this.httpClient.put<T>(this.getRequestString(requestConfig), requestConfig.RequestObject)
    this.processRequest(request, requestConfig)
  }

  private getRequestString(request: IRequest): string {
    return `${this.baseUrl}${request.Uri}`
  }

  private processRequest<T>(observable: Observable<T>, request: ITypedRequest<T>) {
    observable
      .toPromise()
      .then(data => {
        if (request.OnThenAction !== null)
          request.OnThenAction(data);

        if (request.UseSuccessSnackBar === true)
          // this._snackBar.success();

        return data;
      })
      .catch(error => {
        // this._snackBar.fail();
        console.log(error)
      });
  }
}

export interface IRequest {
  Uri: string,
  UseSuccessSnackBar: boolean
}

export interface ITypedRequest<T> extends IRequest {
  OnThenAction: (result: T) => void,
  RequestObject: T
}

export class TypedRequestImpl<T> implements ITypedRequest<T> {
  constructor(uri: string,
              useSuccessSnackBar: boolean,
              requestObject?: T,
              onThenAction?: (result: T) => void) {
    this.Uri = uri;
    this.UseSuccessSnackBar = useSuccessSnackBar;
    this.RequestObject = requestObject;
    this.OnThenAction = onThenAction;
  }

  OnThenAction: (result: T) => void;
  RequestObject: T;
  Uri: string;
  UseSuccessSnackBar: boolean;
}