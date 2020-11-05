import {AuthService} from "../authService/authService";
import {Inject} from "@angular/core";
import {Router} from "@angular/router";

export class AuthProtected {
  protected authService: AuthService;

  constructor(
    @Inject(AuthService) authService: AuthService,
    private router: Router) {
    this.authService = authService;
  }

  hasAccess(): boolean {
    if(this.authService.isAuthenticated())
      return true;

    this.router.navigate(["auth"]);
    return false;
  }
}
