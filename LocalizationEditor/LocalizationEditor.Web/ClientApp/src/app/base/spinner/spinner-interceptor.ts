import {Observable, Subscription} from 'rxjs';
import { Injectable } from '@angular/core';
import { HttpInterceptor } from '@angular/common/http';
import { HttpRequest } from '@angular/common/http';
import { HttpHandler } from '@angular/common/http';
import { HttpEvent } from '@angular/common/http';
import {finalize, } from 'rxjs/operators';
import { SpinnerOverlayService } from './spinner-overlay-service';

@Injectable()
export class SpinnerHttpInterceptor implements HttpInterceptor {

  constructor(private spinnerService: SpinnerOverlayService) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const spinnerSubscription: Subscription = this.spinnerService.spinner$.subscribe();
    return next.handle(req).pipe(finalize(() => spinnerSubscription.unsubscribe()));
  }


}
