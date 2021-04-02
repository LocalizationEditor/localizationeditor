import { IUser } from "./IUser";
import { Role } from "./Role";


export class User implements IUser {
  id: number;
  userName: string;
  password: string;
  role: Role;

  constructor(
    id: number,
    userName: string,
    password: string,
    role: Role) {
    this.id = id;
    this.password = password;
    this.userName = userName;
    this.role = role;
  }
}

