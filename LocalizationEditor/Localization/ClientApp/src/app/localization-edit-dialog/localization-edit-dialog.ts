import {Component, Inject, ViewChild} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {NgeMonacoThemeService} from "nge-monaco";

export interface UpdateDialogData {
  locales: string[];
}

@Component({
  selector: 'localization-edit-dialog',
  templateUrl: 'localization-edit-dialog.html',
  styleUrls: ['./localization-edit-dialog.css'],
})

export class LocalizationEditDialog {
  themes = this.theming.themesChanges;
  locales: string[];
  langs: any;
  flag: boolean = false;

  constructor(public dialogRef: MatDialogRef<LocalizationEditDialog>,
              @Inject(MAT_DIALOG_DATA) public data: UpdateDialogData,
              private readonly theming: NgeMonacoThemeService) {
    this.locales = data.locales;
    // this.langs = monaco.languages.getLanguages().map(({ id }) => id);
  }

  onCreateEditor(editor: monaco.editor.IStandaloneCodeEditor, languages: monaco.languages.LanguageConfiguration) {

    editor.setModel(monaco.editor.createModel('print("Hello world")', 'python'));
    this.langs = monaco.languages.getLanguages().map(({id}) => id);
  }

  async switchTheme(theme: string) {
    debugger;
    this.theming.setTheme(theme);
  }

  save(): void {

    console.log("saved");
  }

  helper(): void {
    if (this.flag === false) {
      const data = document.getElementsByClassName("mat-tab-body-wrapper")[0];
      console.log(data)
      data.setAttribute("style","height:100%");
      //data.
    }
  }
}
