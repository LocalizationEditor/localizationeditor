import {Component, OnInit, ViewChild} from '@angular/core';
import {MatTableDataSource} from '@angular/material/table';
import {LocalizationEditDialog} from "../localization-edit-dialog/localization-edit-dialog";
import {MatDialog} from "@angular/material/dialog";
import {LocalizationDataRowView} from "./models/localization-data-row-view";
import {LocalizationConfig} from "./models/localization-config";
import {HttpRequestService, TypedRequestImpl} from "../base/http-request-service";
import {BaseServerRoutes} from "../base/base-server-routes";
import {LocalizationDataRowServerDto} from "./models/localization-data-row-server-dto";
import {LocalizationDataRowsServerDto} from "./models/localization-data-rows-server-dto";
import { MatPaginator } from '@angular/material';

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
  @ViewChild(MatPaginator) paginator: MatPaginator;

  start: number = 0;
  limit: number = 25;
  end: number = this.limit + this.start;
  searchValue: string = "";
  totalCount = 0;

  constructor(private _dialog: MatDialog,
              private _httpService: HttpRequestService) {
    this.dataSource = new MatTableDataSource();
  }

  ngOnInit() {
    this.getConfig();
    this.getList();
    this.dataSource.paginator = this.paginator;
  }

  private getConfig() {
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
    let request = new TypedRequestImpl(`${BaseServerRoutes.Localization}?limit=${this.limit}&offset=${this.start}`,
      false,
      null,
      result => {
        let rows = result.localizationStrings.map(LocalizationTable.mapRow);
        this.totalCount = result.count;
        this.dataSource = new MatTableDataSource(rows);
      });
    this._httpService.get<LocalizationDataRowsServerDto>(request);
  }

  private static mapRow(serverDto: LocalizationDataRowServerDto): LocalizationDataRowView {
    let row = {
      group: serverDto.group,
      key: serverDto.key,
      id: serverDto.id
    }
    return serverDto.localizations.reduce((obj, item) => {
      obj[item.locale] = item.value;
      return obj;
    }, row);
  }

  applyFilter(event: Event) {
     this.searchValue = (event.target as HTMLInputElement).value;
    let request = new TypedRequestImpl(`${BaseServerRoutes.Localization}?limit=${this.limit}&offset=${this.start}&search=${this.searchValue}`,
      false,
      null,
      result => {
        let rows = result.localizationStrings.map(LocalizationTable.mapRow);
        this.totalCount = result.count;
        this.dataSource = new MatTableDataSource(rows);
      });
    this._httpService.get<LocalizationDataRowsServerDto>(request);
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
  onTableScroll(e) {
    const tableViewHeight = e.target.offsetHeight // viewport
    const tableScrollHeight = e.target.scrollHeight // length of all table
    const scrollLocation = e.target.scrollTop; // how far user scrolled

    // If the user has scrolled within 200px of the bottom, add more data
    const buffer = 200;
    const limit = tableScrollHeight - tableViewHeight - buffer;
    if (scrollLocation > limit) {
      this.getTableData(this.end);;
      this.updateIndex();
    }
  }

  getTableData(end) {

    if (end <= this.totalCount) {
      let request = new TypedRequestImpl(`${BaseServerRoutes.Localization}/?limit=${this.limit}&offset=${end}&search=${this.searchValue}`,
        false,
        null,
        result => {
          let rows = result.localizationStrings.map(LocalizationTable.mapRow);
          this.totalCount = result.count;

          this.dataSource.data = this.dataSource.data.concat(rows);
        });
      this._httpService.get<LocalizationDataRowsServerDto>(request);
    }
  }

  
  updateIndex() {
    this.start = this.end;
    this.end = this.limit + this.start;
  }
}
