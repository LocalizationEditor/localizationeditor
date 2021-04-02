import { Component, Input } from "@angular/core";
import { FormControl, Validators } from "@angular/forms";
import { MatDialogRef } from "@angular/material";
import { Router } from "@angular/router";
import { BaseServerRoutes } from "../base/base-server-routes";
import { HttpRequestService, TypedRequestImpl } from "../base/http-request-service";
import { LoginStateMatcher } from "./loginStateMatcher";
import { ILogin } from "./models/iLogin";
import { Login } from "./models/login";

@Component({
  selector: "login",
  templateUrl: "login.component.html",
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  login: string;
  password: string;

  constructor(private dialogRef: MatDialogRef<LoginComponent>, private _httpService: HttpRequestService, private router: Router) {
    dialogRef.disableClose = true;
  }

  emailFormControl = new FormControl('', [
    Validators.required,
    Validators.email,
  ]);

  passwordFormControl = new FormControl('', [
    Validators.required
  ]);

  onLogin(): void {
    const login = new Login(this.login, this.password);
    const request = new TypedRequestImpl<ILogin>(
      BaseServerRoutes.Login,
      true,
      login,
      res => {
        this.router.navigateByUrl('/');
      }, "Success");

    this._httpService.post(request);
  }

  matcher = new LoginStateMatcher();
}
