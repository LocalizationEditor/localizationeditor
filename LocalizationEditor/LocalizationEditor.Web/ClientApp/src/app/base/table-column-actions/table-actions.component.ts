import {Component, Input} from "@angular/core";
import {MatDialog} from "@angular/material/dialog";
import {IClientHandler} from "../clinet-handler/client-handler";
import {ConnectionDeleteDialog} from "../../connection/connection-delete-dialog/connection-delete-dialog";

@Component({
  selector: 'edit-delete-action-comp',
  templateUrl: 'table-actions.component.html'
})
export class TableColumnActions {
  @Input() routeEditLink: string;
  @Input() routeDeleteLink: string;
  @Input() id: number;
  @Input() message: string;
  @Input() handler: IClientHandler<any>;

  constructor(public dialog: MatDialog) {
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(ConnectionDeleteDialog, {
      data: {id: this.id, message: this.message, link: this.routeDeleteLink}
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result != undefined) {
        console.log(`Delete by root, ${result.link}/${result.id}`);
        this.handler.delete(result.id);
      }
    });
  }
}
