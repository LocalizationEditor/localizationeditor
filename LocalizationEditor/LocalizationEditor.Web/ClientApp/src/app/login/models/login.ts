import { ILogin } from "./iLogin";

export class Login implements ILogin {
  password: string;
  login: string;

  constructor(login: string, password: string) {
    this.login = login;
    this.password = password;
  }
}
