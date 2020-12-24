import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {MatTabChangeEvent} from "@angular/material/tabs";
import {UpdateDialogData} from "./update-dialog-data";
import {HttpRequestService} from "../base/http-request-service";
import {BaseServerRoutes} from "../base/base-server-routes";
import {
  LocalizationDataRowServerDto,
  LocalizationDataRowView,
  LocalizationStringDto
} from "../localization-table/localization-data-row-view";
import {SnackbarService} from "../base/snackbar-service";

@Component({
  selector: 'localization-edit-dialog',
  templateUrl: 'localization-edit-dialog.html',
  styleUrls: ['./localization-edit-dialog.css'],
  providers: [SnackbarService]
})

export class LocalizationEditDialog {
  locales: string[];
  editorOptions = {theme: 'vs-dark', language: 'html', automaticLayout: true};
  code: string;
  localizationString: LocalizationDataRowView;
  localizationKey: string;
  private lastSelected: string;

  constructor(public dialogRef: MatDialogRef<LocalizationEditDialog>,
              @Inject(MAT_DIALOG_DATA) public data: UpdateDialogData,
              private _httpService: HttpRequestService,
              private _snackBar: SnackbarService) {
    this.locales = data.locales;
    this.localizationString = data.localizedString;
    this.localizationKey = data.localizedString.key;

    this.lastSelected = this.locales[0];
    this.code = this.localizationString[this.lastSelected];
  }

  save(): void {
    let obj = this.mapServerDto(this.localizationString);
    let promise;
    if (this.localizationString.id === 0)
      promise = this._httpService.post(`${BaseServerRoutes.Localization}`, obj)
    else
      promise = this._httpService.put(`${BaseServerRoutes.Localization}/${this.localizationString.id}`, obj);
    promise.subscribe(
      data => this._snackBar.success(),
      error => {
        this._snackBar.fail();
        console.log(error)
      })
  }

  private mapServerDto(localizationString: LocalizationDataRowView): LocalizationDataRowServerDto {
    let localizations: LocalizationStringDto[] = [];
    for (const localesKey in this.locales) {
      let localizationObject = {
        locale: this.locales[localesKey],
        value: localizationString[this.locales[localesKey]]
      };
      localizations.push(localizationObject);
    }

    return {
      group: localizationString.group,
      key: localizationString.key,
      id: localizationString.id,
      localizations: localizations
    };
  }


  tabChanged($event: MatTabChangeEvent): void {
    this.localizationString[this.lastSelected] = this.code
    this.code = this.localizationString[$event.tab.textLabel];
    this.lastSelected = $event.tab.textLabel;
  }
}

