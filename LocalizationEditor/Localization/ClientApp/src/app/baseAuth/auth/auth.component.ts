import { Component } from '@angular/core';
import {ACCESS_TOKEN_KEY, AuthService} from "../authService/authService";

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent{
  constructor(private authService: AuthService) {
  }

  isLogIn() : boolean{
    return this.authService.isAuthenticated();
  }

  login(email: string, password: string){
    this.authService.login(email, password)
      .subscribe(res => {
        alert("auth is done");
      }, error => {
        alert("not auth");
      });
  }

  logout(){
    this.authService.logout();
  }
}
