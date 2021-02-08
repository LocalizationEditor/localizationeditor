import { SelectionModel } from '@angular/cdk/collections';
import { NestedTreeControl } from '@angular/cdk/tree';
import { AfterViewInit, Component, EventEmitter, Input, Output } from '@angular/core';
import { MatTreeNestedDataSource } from '@angular/material/tree';
import { ITreeNode } from './iTreeNode';


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
  @Output() selectEmitter = new EventEmitter<ITreeNode>();
  @Output() deselectEmitter = new EventEmitter<ITreeNode>();

  checklistSelection = new SelectionModel<ITreeNode>(true);

  ngAfterViewInit(): void {
    this.dataSource.data = this.treeData;
  }

  hasChild = (_: number, node: ITreeNode) => !!node.keys && node.keys.length > 0;

  onClick(value: ITreeNode) {
    this.emitter.emit(value);
  }

  nodeChange(node: ITreeNode) {
    if (!this.checklistSelection.isSelected(node)) {
      this.selectNode(node);
      if (node.parent !== null)
        this.selectEmitter.emit(node);
    }
    else {
      this.deselectNode(node);
      if (node.parent !== null)
        this.deselectEmitter.emit(node);
    }
  }

  selectNode(node: ITreeNode) {
    this.checklistSelection.select(node);
    if (node.parent === null) {
      node.keys.forEach(item => {
        this.checklistSelection.select(item);
        this.selectEmitter.emit(item);
      });
    }
    else {
      this.checklistSelection.select(node.parent);
    }
  }

  deselectNode(node: ITreeNode) {
    this.checklistSelection.deselect(node);
    if (node.parent === null) {
      node.keys.forEach(item => {
        this.checklistSelection.deselect(item);
        this.deselectEmitter.emit(item);
      });
    }
    else {
      if (!node.parent.keys.some(o => this.checklistSelection.isSelected(o)))
        this.checklistSelection.deselect(node.parent);
    }
  }
}
