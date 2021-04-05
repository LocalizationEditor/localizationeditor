import { ILogin } from "./iLogin";

export class Login implements ILogin {
  password: string;
  email: string;
  role?: number;

  constructor(login: string, password: string, role?: number) {
    this.email = login;
    this.password = password;
    this.role = role;
  }  
}
