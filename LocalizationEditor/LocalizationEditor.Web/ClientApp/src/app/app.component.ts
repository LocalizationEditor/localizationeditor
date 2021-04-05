import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material';
import { LoginComponent } from './login/login.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  title = 'app';
  public hasAccess: boolean = false;

  constructor(private _dialog: MatDialog) {
  }

  ngOnInit(): void {
    const role = localStorage.getItem("role");
    if (role) {
      this.hasAccess = true;
      return;
    }
      

    this._dialog.open(LoginComponent, {
      width: '360px',
      height: '460px'
    });
  }
}
