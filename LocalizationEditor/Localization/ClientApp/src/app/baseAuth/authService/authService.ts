import {Inject, Injectable} from "@angular/core";
import {Observable} from "rxjs";
import {HttpClient} from "@angular/common/http";
import {JwtHelperService} from "@auth0/angular-jwt";
import {Router} from "@angular/router";
import {tap} from "rxjs/operators";
import {AuthToken} from "../modelAuth/authToken";
import {API_URL} from "../../app-injection-tokens";

export const ACCESS_TOKEN_KEY = "localizationEditor_token";

@Injectable({
  providedIn: "root"
})
export class AuthService {
  constructor(
    private httpClient: HttpClient,
    @Inject(API_URL) private api: string,
    private jwtHelper: JwtHelperService,
    private router: Router) {}

  login(email: string, password: string): Observable<AuthToken> {
    return this.httpClient.post<AuthToken>(`${this.api}auth`, {
      email, password})
      .pipe(
        tap(token => {
          localStorage.setItem(ACCESS_TOKEN_KEY, token.token);
        })
    );
  }

  isAuthenticated(): boolean{
    var token = localStorage.getItem((ACCESS_TOKEN_KEY));
    return token && !this.jwtHelper.isTokenExpired(token);
  }

  logout(): void{
    localStorage.removeItem(ACCESS_TOKEN_KEY);
    this.router.navigate(['']);
  }
}
