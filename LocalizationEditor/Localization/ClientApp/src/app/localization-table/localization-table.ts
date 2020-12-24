import {Component, OnInit} from '@angular/core';
import {MatTableDataSource} from '@angular/material/table';
import {LocalizationEditDialog} from "../localization-edit-dialog/localization-edit-dialog";
import {MatDialog} from "@angular/material/dialog";
import {
  LocalizationDataRowView,
  LocalizationDataRowServerDto,
  LocalizationDataRowsServerDto
} from "./localization-data-row-view";
import {LocalizationConfig} from "./localization-config";
import {HttpRequestService} from "../base/http-request-service";
import {catchError, map, startWith, switchMap} from "rxjs/operators";
import {merge, of as observableOf} from 'rxjs';

@Component({
  selector: 'localization-table',
  styleUrls: ['./localization-table.css'],
  templateUrl: 'localization-table.html',
})

export class LocalizationTable implements OnInit {
  public columnsToDisplay = [];
  columns = ['group', 'key'];
  dataSource: MatTableDataSource<LocalizationDataRowView>;
  public config: LocalizationConfig;
  private isLoadingResults: boolean;

  constructor(private dialog: MatDialog, private httpService: HttpRequestService) {
    this.dataSource = new MatTableDataSource();
  }

  ngOnInit() {
    this.getConfig();
    this.getList();
  }

  private getConfig() {
    this.httpService.get<LocalizationConfig>(`${BaseServerRoutes.Localization}/config`)
      .subscribe(data => {
          this.config = data;
          this.config.locales.forEach(i => this.columns.push(i));
          this.columnsToDisplay = Array.from(this.columns);
          this.columnsToDisplay.push('actions');
        },
        error => console.log(error));
  }

  private getList() {
    this.httpService.get<LocalizationDataRowsServerDto>(`${BaseServerRoutes.Localization}`)
      .subscribe(data => {
        if(data === null)
          return;
        let rows = data.localizationStrings.map(LocalizationTable.mapRow);
        this.dataSource = new MatTableDataSource(rows)
      }, i => console.log(i));
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
    const dialogRef = this.dialog.open(LocalizationEditDialog, {
      height: '99%',
      width: '90%',
      data: {locales: this.config.locales, localizedString: localizedRow}
    });
    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }

  deleteLocalizationKey() {
    // todo add logic to delete and server route
  }

  addLocalizationString() {
    const dialogRef = this.dialog.open(LocalizationEditDialog, {
      height: '99%',
      width: '99%',
      data: {locales: this.config.locales, localizedString: {}}
    });
    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }
}

enum BaseServerRoutes {
  Localization = `localization`,
}
