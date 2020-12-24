import {AfterViewInit, Component, OnInit} from '@angular/core';
import {MatTableDataSource} from '@angular/material/table';
import {LocalizationEditDialog} from "../localization-edit-dialog/localization-edit-dialog";
import {MatDialog} from "@angular/material/dialog";
import {LocalizationDataRow} from "./localization-data-row";
import {LocalizationConfig} from "./localization-config";
import {HttpRequestService} from "../base/http-request-service";

const NAMES: string[] = [
  'Maia', 'Asher', 'Olivia', 'Atticus', 'Amelia', 'Jack', 'Charlotte', 'Theodore', 'Isla', 'Oliver',
  'Isabella', 'Jasper', 'Cora', 'Levi', 'Violet', 'Arthur', 'Mia', 'Thomas', 'Elizabeth'
];

@Component({
  selector: 'localization-table',
  styleUrls: ['./localization-table.css'],
  templateUrl: 'localization-table.html',
})

export class LocalizationTable implements OnInit, AfterViewInit {
  public columnsToDisplay = [];

  columns = ['group', 'key'];
  dataSource: MatTableDataSource<LocalizationDataRow>;
  rows: LocalizationDataRow[];
  public config: LocalizationConfig;

  constructor(private dialog: MatDialog, private httpService: HttpRequestService) {
  }

  ngOnInit() {
    this.httpService.get<LocalizationConfig>(`${BaseServerRoutes.Localization}/config`).subscribe(data => {
        this.config = data;
        this.rows = Array.from({length: 100}, (_, k) => this.createLocalizationRow(k + 1));
        this.dataSource = new MatTableDataSource(this.rows);
        this.config.locales.forEach(i => this.columns.push(i));
        this.columnsToDisplay = Array.from(this.columns);
        this.columnsToDisplay.push('actions');
      },
      error => console.log(error));
  }

  ngAfterViewInit(): void {
    console.log();
  }

  private createLocalizationRow(id: number): LocalizationDataRow {
    const name = NAMES[Math.round(Math.random() * (NAMES.length - 1))] + ' ' +
      NAMES[Math.round(Math.random() * (NAMES.length - 1))].charAt(0) + '.';

    let row = {
      group: id.toString(),
      key: name
    };

    for (const locale in this.config.locales) {
      row[this.config.locales[locale]] = this.config.locales[Math.round(Math.random() * (this.config.locales.length - 1))];
    }
    return row;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  editLocalization(localizedRow: LocalizationDataRow) {
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

enum BaseServerRoutes
{
  Localization = `localization`,
}
