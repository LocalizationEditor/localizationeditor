import {IConnection} from "../connection-data/IConnection";
import {IClientHandler} from "../../base/clinet-handler/client-handler";
import {EntityState} from "../../base/entity-state";

export class ConnectionClientHandler implements IClientHandler<IConnection> {
  handle(entity: IConnection): IConnection {
    switch (entity.state) {
      case EntityState.Add:
        return this.create(entity);
      case EntityState.Update:
        return this.update(entity);
      default:
        return;
    }
  }

  private create(connection: IConnection): IConnection {
    console.log(`create id:${connection.id}`);
    return connection;
  }

  private update(connection: IConnection): IConnection {
    console.log(`update id:${connection.id}`);
    return connection;
  }

  delete(connectionId: number): void {
    console.log("delete");
  }
}
