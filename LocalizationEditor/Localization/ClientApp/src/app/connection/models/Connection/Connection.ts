import {IConnection} from "./IConnection";

export class Connection implements IConnection {
  connectionName: string;
  dbName: string;
  dbType: string;
  password: string;
  serverName: string;
  userName: string;

  constructor(
    connectionName: string,
    dbName: string,
    dbType: string,
    password: string,
    serverName: string,
    userName: string) {
    this.connectionName = connectionName;
    this.dbName = dbName;
    this.dbType = dbType;
    this.password = password;
    this.serverName = serverName;
    this.userName = userName;
  }
}
