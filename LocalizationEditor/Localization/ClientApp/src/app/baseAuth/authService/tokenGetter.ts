import {ACCESS_TOKEN_KEY} from "./authService";

export function tokenGetter() {
  return localStorage.getItem(ACCESS_TOKEN_KEY);
}
