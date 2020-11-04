import {ConnectionState} from "./connection-state";

export interface IConnection {
  id: number;
  name: string;
  connectionString: string;
  state: ConnectionState;

  isNewEntity(): boolean;
}
