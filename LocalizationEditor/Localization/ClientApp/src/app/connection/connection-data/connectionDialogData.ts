import {IConnection} from "./IConnection";
import {ConnectionState} from "./connection-state";

export class ConnectionDialogData implements IConnection{
  connectionString: string;
  id: number;
  name: string;
  message: string;
  link: string;
  state: ConnectionState;

  isNewEntity(): boolean {
    return this.id == 0;
  }
}
