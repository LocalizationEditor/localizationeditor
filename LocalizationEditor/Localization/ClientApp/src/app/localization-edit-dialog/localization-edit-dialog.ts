import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";

export interface UpdateDialogData {
  locales: string[];
}

@Component({
  selector: 'localization-edit-dialog',
  templateUrl: 'localization-edit-dialog.html',
  styleUrls: ['./localization-edit-dialog.css'],
})

export class LocalizationEditDialog {
  locales: string[];
  constructor(public dialogRef: MatDialogRef<LocalizationEditDialog>,
              @Inject(MAT_DIALOG_DATA) public data: UpdateDialogData) {
    this.locales = data.locales;
  }

  onCreateEditor(editor: monaco.editor.IStandaloneCodeEditor,languages : monaco.languages.LanguageConfiguration) {
    editor.setModel(monaco.editor.createModel('', 'html'));
  }

  save(): void {
    console.log("saved");
  }
}
