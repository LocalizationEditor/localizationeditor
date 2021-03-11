import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { MatTabChangeEvent } from "@angular/material/tabs";
import { UpdateDialogData } from "./update-dialog-data";
import { HttpRequestService, TypedRequestImpl } from "../base/http-request-service";
import { BaseServerRoutes } from "../base/base-server-routes";
import {
  LocalizationDataRowView
} from "../localization-table/models/localization-data-row-view";
import { LocalizationDataRowServerDto } from "../localization-table/models/localization-data-row-server-dto";
import { Observable, of, Subscription } from "rxjs";
import { FormControl } from "@angular/forms";
import { LocalizationDataService } from './localization-data.service';

@Component({
  selector: 'localization-edit-dialog',
  templateUrl: 'localization-edit-dialog.html',
  styleUrls: ['./localization-edit-dialog.css'],
})

export class LocalizationEditDialog implements OnInit {
  public locales: string[];
  public editorOptions = { theme: 'vs-dark', language: 'html', automaticLayout: true };
  public code: string;
  public localizationString: LocalizationDataRowView;
  localizationKey: string;
  private lastSelected: string;
  isPreview: boolean;
  public groupKey: string;

  myControl = new FormControl();
  options: string[];
  filteredOptions: Observable<string[]> = new Observable<string[]>();
  inputSubscription: Subscription;

  constructor(public dialogRef: MatDialogRef<LocalizationEditDialog>,
    @Inject(MAT_DIALOG_DATA) public data: UpdateDialogData,
    private _httpService: HttpRequestService,
    private _localizationService: LocalizationDataService) {
    this.localizationString = data.localizedString;
    this.localizationKey = data.localizedString.key;
    this.groupKey = data.localizedString.group;
  }

  ngOnInit() {
    this.getEditorConfig();

  }

  ngAfterViewInit() {
    this.myControl.setValue(this.groupKey);
    this.inputSubscription = this.myControl.valueChanges.subscribe(i => this.filter(i));
  }

  ngOnDestroy() {
    if (this.inputSubscription) {
      this.inputSubscription.unsubscribe();
    }
  }

  public filter(value: string): void {
    const filterValue = value.toLowerCase();
    this.filteredOptions = of(this.options.filter(option => option.toLowerCase().includes(filterValue)));
    this.groupKey = value;
  }

  public onSelectedValue(value: string) {
    this.groupKey = value;
  }

  public onValueChanged(value: string) {
    this.isPreview = JSON.parse(value);
    console.log(value);
  }

  public getEditorConfig(): void {
    let request = new TypedRequestImpl(
      `${BaseServerRoutes.Localization}/editor/config`,
      false,
      null,
      result => {
        this.locales = result.locales;
        this.options = result.groups;
        this.lastSelected = this.locales[0];
        this.code = this.localizationString[this.lastSelected];
      });
    this._httpService.get(request)
  }

  public save(): void {
    this.localizationString[this.lastSelected] = this.code
    this.localizationString.group = this.groupKey;
    this.localizationString.key = this.localizationKey;
    let obj = this.mapServerDto(this.localizationString);
    this._localizationService.save(obj);
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
