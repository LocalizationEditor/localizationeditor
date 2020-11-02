import {IConnection} from "./connection";

export class ConnectionDialogData implements IConnection{
  connectionString: string;
  id: number;
  name: string;
  message: string;
  link: string;
}
