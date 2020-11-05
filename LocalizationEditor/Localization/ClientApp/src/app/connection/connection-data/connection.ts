import {IConnection} from "./IConnection";
import {EntityState} from "../../base/entity-state";

export class Connection implements IConnection{
  id: number;
  name: string;
  connectionString: string;
  state: EntityState;

  constructor(id: number, name: string, connection: string) {
    this.id = id;
    this.name = name;
    this.connectionString = connection;
  }

  isNewEntity():boolean {
    return this.id == 0;
  }

  setState(state: EntityState): void{
    this.state = state;
  }
}
