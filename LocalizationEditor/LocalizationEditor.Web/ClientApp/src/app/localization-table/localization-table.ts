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
import { LocalizationDataService } from '../localization-edit-dialog/localization-data.service';

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
    private _httpService: HttpRequestService,
    private _localizationDataService: LocalizationDataService ) {
    this.dataSource = new MatTableDataSource();
  }

  ngOnInit() {

    this.getConfig();
    this._localizationDataService.localizationRows.subscribe(localizationRows => {
      this.dataSource = new MatTableDataSource(localizationRows);
    });
    this._localizationDataService.totalCount.subscribe(count => {
      this.totalCount = count;
    });
    this._localizationDataService.initialize();
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
    this._localizationDataService.loadListMore(this.limit, this.start, this.searchValue);
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
    this._localizationDataService.deleteLocalizationKey(localizedRow);
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
      this._localizationDataService.loadListMore(this.limit, end, this.searchValue);
    }
  }

  
  updateIndex() {
    this.start = this.end;
    this.end = this.limit + this.start;
  }
}
