import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {MatTabChangeEvent} from "@angular/material/tabs";
import {UpdateDialogData} from "./update-dialog-data";
import {HttpRequestService, TypedRequestImpl} from "../base/http-request-service";
import {BaseServerRoutes} from "../base/base-server-routes";
import {
  LocalizationDataRowView
} from "../localization-table/models/localization-data-row-view";
import {LocalizationDataRowServerDto} from "../localization-table/models/localization-data-row-server-dto";
import {Observable} from "rxjs";
import {FormControl} from "@angular/forms";
import {map, startWith} from "rxjs/operators";

@Component({
  selector: 'localization-edit-dialog',
  templateUrl: 'localization-edit-dialog.html',
  styleUrls: ['./localization-edit-dialog.css'],
})

export class LocalizationEditDialog implements OnInit {
  public locales: string[];
  public editorOptions = {theme: 'vs-dark', language: 'html', automaticLayout: true};
  public code: string;
  public localizationString: LocalizationDataRowView;
  localizationKey: string;
  private lastSelected: string;
  isPreview: boolean;

  myControl = new FormControl();
  options: string[] = ['One', 'Two', 'Three'];
  filteredOptions: Observable<string[]>;


  constructor(public dialogRef: MatDialogRef<LocalizationEditDialog>,
              @Inject(MAT_DIALOG_DATA) public data: UpdateDialogData,
              private _httpService: HttpRequestService) {
    this.locales = data.locales;
    this.localizationString = data.localizedString;
    this.localizationKey = data.localizedString.key;

    this.lastSelected = this.locales[0];
    this.code = this.localizationString[this.lastSelected];
  }

  ngOnInit() {
    this.filteredOptions = this.myControl.valueChanges
      .pipe(
        startWith(''),
        map(value => this._filter(value))
      );
  }

  private _filter(value: string): string[] {
    const filterValue = value.toLowerCase();

    return this.options.filter(option => option.toLowerCase().includes(filterValue));
  }

  save(): void {
    this.localizationString[this.lastSelected] = this.code
    let obj = this.mapServerDto(this.localizationString);
    let request = new TypedRequestImpl(
      this.localizationString.id === 0 ?
        `${BaseServerRoutes.Localization}` :
        `${BaseServerRoutes.Localization}/${this.localizationString.id}`,
      true,
      obj);
    if (this.localizationString.id === 0)
      this._httpService.post(request)
    else
      this._httpService.put(request);
  }

  private mapServerDto(localizationString: LocalizationDataRowView): LocalizationDataRowServerDto {
    return {
      group: localizationString.group,
      key: localizationString.key,
      id: localizationString.id,
      localizations: this.locales.map(i => {
        return {
          locale: i,
          value: localizationString[i]
        }
      })
    };
  }

  tabChanged($event: MatTabChangeEvent): void {
    this.localizationString[this.lastSelected] = this.code
    this.code = this.localizationString[$event.tab.textLabel];
    this.lastSelected = $event.tab.textLabel;
  }
}

