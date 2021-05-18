import {IConnection} from "./IConnection";
import {DbType} from "./DbType";

export class Connection implements IConnection {
  id: number;
  connectionName: string;
  dbName: string;
  dbType: DbType;
  password: string;
  serverName: string;
  userName: string;
  forAll: boolean;

  constructor(
    id: number,
    connectionName: string,
    dbName: string,
    dbType: DbType,
    password: string,
    serverName: string,
    userName: string,
    forAll: boolean) {
    this.id = id;
    this.connectionName = connectionName;
    this.dbName = dbName;
    this.dbType = dbType;
    this.password = password;
    this.serverName = serverName;
    this.userName = userName;
    this.forAll = forAll;
  }
}
