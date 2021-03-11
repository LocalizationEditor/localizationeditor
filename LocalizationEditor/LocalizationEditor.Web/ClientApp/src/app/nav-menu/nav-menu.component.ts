import {MediaMatcher} from '@angular/cdk/layout';
import {ChangeDetectorRef, Component, OnDestroy} from '@angular/core';
import {CustomRoute} from "../base/route";
import { ConnectionHelper } from '../connection/components/wrapper/connection-wrapper.component';

const Routes: CustomRoute[] = [
  new CustomRoute(1, "Localization", ""),
  new CustomRoute(2, "Connection", "connections"),
  new CustomRoute(3, "Synchronize", "sync"),
];

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnDestroy{
  // @ts-ignore
  mobileQuery: MediaQueryList;
  routes: CustomRoute[] = Routes;
  private readonly _mobileQueryListener: () => void;

  constructor(changeDetectorRef: ChangeDetectorRef, media: MediaMatcher) {
    this.mobileQuery = media.matchMedia('(max-width: 600px)');
    this._mobileQueryListener = () => changeDetectorRef.detectChanges();
    this.mobileQuery.addListener(this._mobileQueryListener);
  }

  ngOnDestroy(): void {
    this.mobileQuery.removeListener(this._mobileQueryListener);
  }
}
