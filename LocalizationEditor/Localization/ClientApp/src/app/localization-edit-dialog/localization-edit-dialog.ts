import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {MatTabChangeEvent} from "@angular/material/tabs";

export interface UpdateDialogData {
  locales: string[];
  localizedString: object;
}

@Component({
  selector: 'localization-edit-dialog',
  templateUrl: 'localization-edit-dialog.html',
  styleUrls: ['./localization-edit-dialog.css'],
})

export class LocalizationEditDialog {
  locales: string[];
  editorOptions = {theme: 'vs-dark', language: 'html', automaticLayout: true};
  code: string;
  localizationString = {};
  private lastSelected: string;

  constructor(public dialogRef: MatDialogRef<LocalizationEditDialog>,
              @Inject(MAT_DIALOG_DATA) public data: UpdateDialogData) {
    this.locales = data.locales;
    this.localizationString = data.localizedString;

    this.lastSelected = this.locales[0];
    this.code = this.localizationString[this.lastSelected];
  }

  save(): void {
    // todo implement save logic to server
  }

  tabChanged($event: MatTabChangeEvent): void {
    this.localizationString[this.lastSelected] = this.code
    this.code = this.localizationString[$event.tab.textLabel];
    this.lastSelected = $event.tab.textLabel;
  }
}

