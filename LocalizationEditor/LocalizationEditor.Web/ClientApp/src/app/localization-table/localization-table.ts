import {Component, OnInit, Output} from '@angular/core';
import {MatTableDataSource} from '@angular/material/table';
import {LocalizationEditDialog} from "../localization-edit-dialog/localization-edit-dialog";
import {MatDialog} from "@angular/material/dialog";
import {LocalizationDataRowView} from "./models/localization-data-row-view";
import {LocalizationConfig} from "./models/localization-config";
import {HttpRequestService, TypedRequestImpl} from "../base/http-request-service";
import {BaseServerRoutes} from "../base/base-server-routes";
import {LocalizationDataRowServerDto} from "./models/localization-data-row-server-dto";
import {LocalizationDataRowsServerDto} from "./models/localization-data-rows-server-dto";
import {IConnection} from "../connection/models/Connection/IConnection";

@Component({
  selector: 'localization-table',
  styleUrls: ['./localization-table.css'],
  templateUrl: 'localization-table.html',
})

export class LocalizationTable implements OnInit {
  public columnsToDisplay = [];
  public columns = ['group', 'key'];
  public dataSource: MatTableDataSource<LocalizationDataRowView>;
  public config: LocalizationConfig;
  @Output() connection: IConnection;

  constructor(private _dialog: MatDialog,
              private _httpService: HttpRequestService) {
    this.dataSource = new MatTableDataSource();
  }

  ngOnInit() {
    this.getConfig();
    this.getList();
  }

  private getConfig() {
    //.subscribe(data => {
    let request = new TypedRequestImpl(`${BaseServerRoutes.Localization}/config`,
      false,
      null,
      result => {
        this.config = {...result};
        this.config.locales.forEach(i => this.columns.push(i));
        this.columnsToDisplay = Array.from(this.columns);
        this.columnsToDisplay.push('actions');
      });
    this._httpService.get<LocalizationDataRowsServerDto>(request);
  }

  private getList() {
    let request = new TypedRequestImpl(`${BaseServerRoutes.Localization}`,
      false,
      null,
      result => {
        let rows = result.localizationStrings.map(LocalizationTable.mapRow);
        this.dataSource = new MatTableDataSource(rows)
      });
    this._httpService.get<LocalizationDataRowsServerDto>(request);
  }

  private static mapRow(serverDto: LocalizationDataRowServerDto): LocalizationDataRowView {
    let row = {
      group: serverDto.group.name,
      key: serverDto.key,
      id: serverDto.id
    }

    console.log(serverDto);

    return serverDto.localizations.reduce((obj, item) => {
      obj[item.locale] = item.value;
      return obj;
    }, row);
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  editLocalization(localizedRow: LocalizationDataRowView) {
    const dialogRef = this._dialog.open(LocalizationEditDialog, {
      height: '99%',
      width: '90%',
      data: {locales: this.config.locales, localizedString: localizedRow}
    });
    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }

  deleteLocalizationKey(localizedRow: LocalizationDataRowView) {
    let request = new TypedRequestImpl(`${BaseServerRoutes.Localization}/${localizedRow.id}`,
      true);
    this._httpService.delete(request);
  }

  addLocalizationString() {
    const dialogRef = this._dialog.open(LocalizationEditDialog, {
      height: '99%',
      width: '99%',
      data: {locales: this.config.locales, localizedString: {}}
    });
    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }

  reciveConnection($event){
    console.log($event);
    this.connection = $event;
  }
}
