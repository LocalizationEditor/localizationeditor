import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { AfterViewInit, Component, EventEmitter, Input, Output } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';


@Component({
  selector: 'tree',
  templateUrl: 'tree-component.html',
  styleUrls: ['tree-component.css'],
})
export class TreeComponent implements AfterViewInit {
  treeControl = new NestedTreeControl<ITreeNode>(node => node.keys);
  dataSource = new MatTreeNestedDataSource<ITreeNode>();
  @Input() treeData: ITreeNode[];
  @Output() emitter = new EventEmitter<ITreeNode>(); // id

  checklistSelection = new SelectionModel<ITreeNode>(true);

  constructor() {
  }

  ngAfterViewInit(): void {
    this.dataSource.data = this.treeData;
  }

  hasChild = (_: number, node: ITreeNode) => !!node.keys && node.keys.length > 0;

  onClick(value: ITreeNode) {
    this.emitter.emit(value);
  }

  nodeChange(node: ITreeNode) {
    debugger;
    if (!this.checklistSelection.isSelected(node))
      this.selectNode(node);
    else this.deselectNode(node);

    this.sendChanges();
  }

  selectNode(node: ITreeNode) {
    this.checklistSelection.select(node);
    if (node.parent === null) {
      node.keys.forEach(item => this.checklistSelection.select(item));
    }
    else {
      this.checklistSelection.select(node.parent);
    }
  }

  deselectNode(node: ITreeNode) {
    this.checklistSelection.deselect(node);
    if (node.parent === null) {
      node.keys.forEach(item => this.checklistSelection.deselect(item));
    }
    else {
      if (!node.parent.keys.some(o => this.checklistSelection.isSelected(o)))
        this.checklistSelection.deselect(node.parent);
    }
  }
}

export interface ITreeNode {
  parent?: ITreeNode;
  keys?: ITreeNode[];
  key: string;
  id: number;
}
