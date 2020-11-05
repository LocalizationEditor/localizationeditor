import {IConnection} from "./IConnection";
import {EntityState} from "../../base/entity-state";

export class ConnectionDialogData implements IConnection{
  connectionString: string;
  id: number;
  name: string;
  message: string;
  link: string;
  state: EntityState;

  isNewEntity(): boolean {
    return this.id == 0;
  }
}
