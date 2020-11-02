import {Component, Input} from "@angular/core";
import {MatDialog} from "@angular/material/dialog";
import {ConnectionDeleteDialog} from "../../connection/connection-delete-dialog/connection-delete-dialog";

@Component({
  selector: 'edit-delete-action-comp',
  templateUrl: 'actions.html'
})
export class Actions{
  @Input() routeEditLink: string;
  @Input() routeDeleteLink: string;
  @Input() id: number;
  @Input() message: string;

  constructor(public dialog: MatDialog) {}

  openDialog(): void {
    const dialogRef = this.dialog.open(ConnectionDeleteDialog,{
      data: {id: this.id, message: this.message, link: this.routeDeleteLink}
    });

    dialogRef.afterClosed().subscribe(result => {
      if(result != undefined){
        alert(`Delete by root, ${result.link}/${result.id}`);
      }
    });
  }
}
