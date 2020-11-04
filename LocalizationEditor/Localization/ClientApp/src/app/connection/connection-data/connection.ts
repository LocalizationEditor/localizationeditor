import {IConnection} from "./IConnection";
import {ConnectionState} from "./connection-state";

export class Connection implements IConnection{
  id: number;
  name: string;
  connectionString: string;
  state: ConnectionState;

  constructor(id: number, name: string, connection: string) {
    this.id = id;
    this.name = name;
    this.connectionString = connection;
  }

  isNewEntity():boolean {
    return this.id == 0;
  }

  setState(state: ConnectionState): void{
    this.state = state;
  }
}
