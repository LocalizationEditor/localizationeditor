import { Role } from "./Role";

export interface IUser {
  userName: string;
  password: string;
  role: Role;
  id: number;
}
