import { IUser } from "./IUser";
import { Role } from "./Role";


export class User implements IUser {
  userName: string;
  password: string;
  role: Role;

  constructor(
    userName: string,
    password: string,
    role: Role) {
    this.password = password;
    this.userName = userName;
    this.role = role;
  }
}

