import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from "rxjs";
import { SnackbarService } from "./snackbar-service";

@Injectable({
  providedIn: 'root',
  deps: [SnackbarService]
})

export class HttpRequestService {
  constructor(private httpClient: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private _snackBar: SnackbarService) {
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
        if (request.OnThenAction)
          request.OnThenAction(data);

        if (request.UseSuccessSnackBar)
          this._snackBar.success(request.SuccessSnackBarText);

        return data;
      })
      .catch(error => {
        this._snackBar.fail();
        console.log(error)
      });
  }
}

export interface IRequest {
  Uri: string,
  UseSuccessSnackBar: boolean,
  SuccessSnackBarText: string
}

export interface ITypedRequest<T> extends IRequest {
  OnThenAction: (result: T) => void,
  RequestObject: T
}

export class TypedRequestImpl<T> implements ITypedRequest<T> {
  constructor(uri: string,
    useSuccessSnackBar: boolean,
    requestObject?: T,
    onThenAction?: (result: T) => void,
    successSnackBarText?: string) {
    this.Uri = uri;
    this.UseSuccessSnackBar = useSuccessSnackBar;
    this.RequestObject = requestObject;
    this.OnThenAction = onThenAction;
    this.SuccessSnackBarText = successSnackBarText;
  }

  OnThenAction: (result: T) => void;
  RequestObject: T;
  Uri: string;
  UseSuccessSnackBar: boolean;
  SuccessSnackBarText: string;
}
