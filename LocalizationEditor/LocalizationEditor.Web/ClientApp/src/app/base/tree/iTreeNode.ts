export interface ITreeNode {
  parent?: ITreeNode;
  keys?: ITreeNode[];
  key: string;
  id: number;
}
