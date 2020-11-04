import {IConnection} from "../connection-data/IConnection";
import {ConnectionState} from "../connection-data/connection-state";
import {IClientHandler} from "../../base/clinet-handler/client-handler";

export class ConnectionClientHandler implements IClientHandler<IConnection> {
  handle(entity: IConnection): IConnection {
    switch (entity.state) {
      case ConnectionState.Add:
        return this.create(entity);
      case ConnectionState.Update:
        return this.update(entity);
      default:
        return;
    }
  }

  private create(connection: IConnection): IConnection {
    alert(`create id:${connection.id}`);
    return connection;
  }

  private update(connection: IConnection): IConnection {
    alert(`update id:${connection.id}`);
    return connection;
  }

  delete(connectionId: number): void {
    alert("delete");
  }
}
