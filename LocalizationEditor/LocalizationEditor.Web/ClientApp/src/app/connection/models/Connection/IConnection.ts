import {DbType} from "./DbType";

export interface IConnection {
  id: number;
  connectionName: string;
  dbName: string;
  userName: string;
  serverName: string;
  password: string;
  dbType: DbType;
}
