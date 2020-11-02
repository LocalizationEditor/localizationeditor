export class Connection implements IConnection{
  id: number;
  name: string;
  connectionString: string;

  constructor(id: number, name: string, connection: string) {
    this.id = id;
    this.name = name;
    this.connectionString = connection;
  }
}

export interface IConnection {
  id: number;
  name: string;
  connectionString: string;
}
