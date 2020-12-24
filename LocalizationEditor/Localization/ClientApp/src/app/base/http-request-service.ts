import {Inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class HttpRequestService {
  constructor(private httpClient: HttpClient,
              @Inject('BASE_URL') private  baseUrl: string) {
  }

  public get<T>(uri: string) {
    return this.httpClient.get<T>(`${this.baseUrl}${uri}`);
  }

  public delete(uri: string) {
    return this.httpClient.delete(`${this.baseUrl}${uri}`);
  }

  public post<T>(uri: string, requestObject: T) {
    return this.httpClient.post(`${this.baseUrl}${uri}`, requestObject);
  }

  public put<T>(uri: string, requestObject: T) {
    return this.httpClient.put(`${this.baseUrl}${uri}`, requestObject);
  }
}
