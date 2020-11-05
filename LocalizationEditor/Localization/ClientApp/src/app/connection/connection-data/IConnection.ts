import {EntityState} from "../../base/entity-state";

export interface IConnection {
  id: number;
  name: string;
  connectionString: string;
  state: EntityState;

  isNewEntity(): boolean;
}
