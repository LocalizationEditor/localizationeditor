import { ITreeNode } from "../../base/tree/iTreeNode";

export class GroupedKeyNode implements ITreeNode {
  public parent?: GroupedKeyNode;
  public keys?: GroupedKeyNode[];
  public key: string;
  public id: number;

  constructor(key: string, id: number, keys?: GroupedKeyNode[], parent?: GroupedKeyNode) {
    this.keys = keys;
    this.key = key;
    this.id = id;
    this.parent = parent;
  }
}