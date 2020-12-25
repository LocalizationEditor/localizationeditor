import {Component, OnInit} from '@angular/core';
import {MatTableDataSource} from '@angular/material/table';
import {LocalizationEditDialog} from "../localization-edit-dialog/localization-edit-dialog";
import {MatDialog} from "@angular/material/dialog";
import {LocalizationDataRowView} from "./models/localization-data-row-view";
import {LocalizationConfig} from "./models/localization-config";
import {HttpRequestService} from "../base/http-request-service";
import {SnackbarService} from "../base/snackbar-service";
import {BaseServerRoutes} from "../base/base-server-routes";
import {LocalizationDataRowServerDto} from "./models/localization-data-row-server-dto";
import {LocalizationDataRowsServerDto} from "./models/localization-data-rows-server-dto";

@Component({
  selector: 'localization-table',
  styleUrls: ['./localization-table.css'],
  templateUrl: 'localization-table.html',
  providers: [SnackbarService]
})

export class LocalizationTable implements OnInit {
  public columnsToDisplay = [];
  public columns = ['group', 'key'];
  public dataSource: MatTableDataSource<LocalizationDataRowView>;
  public config: LocalizationConfig;

  constructor(private _dialog: MatDialog,
              private _httpService: HttpRequestService,
              private _snackBar: SnackbarService) {
    this.dataSource = new MatTableDataSource();
  }

  ngOnInit() {
    this.getConfig();
    this.getList();
  }

  private getConfig() {
    this._httpService.get<LocalizationConfig>(`${BaseServerRoutes.Localization}/config`)
      .subscribe(data => {
          this.config = data;
          this.config.locales.forEach(i => this.columns.push(i));
          this.columnsToDisplay = Array.from(this.columns);
          this.columnsToDisplay.push('actions');
        },
        error => {
          this._snackBar.fail();
          console.log(error)
        });
  }

  private getList() {
    this._httpService.get<LocalizationDataRowsServerDto>(`${BaseServerRoutes.Localization}`)
      .subscribe(
        data => {
          if (data === null)
            return;
          let rows = data.localizationStrings.map(LocalizationTable.mapRow);
          this.dataSource = new MatTableDataSource(rows)
        },
        error => {
          this._snackBar.fail();
          console.log(error)
        });
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
    this._httpService.delete(`${BaseServerRoutes.Localization}/${localizedRow.id}`)
      .subscribe(
        data => this._snackBar.success(),
        error => {
          this._snackBar.fail();
          console.log(error)
        });
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
}
