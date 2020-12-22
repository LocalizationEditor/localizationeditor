import {AfterViewInit, Component} from '@angular/core';
import {MatTableDataSource} from '@angular/material/table';
import {LocalizationEditDialog} from "../localization-edit-dialog/localization-edit-dialog";
import {MatDialog} from "@angular/material/dialog";
import {LocalizationDataRow} from "./localization-data-row";

const LOCALES: string[] = ['ru', 'ua', 'enlksjdfglhksdjf.xcm,vb.xcmvnb.xc,mvnblifgolihjdclksjdhfgpsdiuyfhjkxcvb;xcvbhlfdglkjdshflkgjhdsfg'];
const NAMES: string[] = [
  'Maia', 'Asher', 'Olivia', 'Atticus', 'Amelia', 'Jack', 'Charlotte', 'Theodore', 'Isla', 'Oliver',
  'Isabella', 'Jasper', 'Cora', 'Levi', 'Violet', 'Arthur', 'Mia', 'Thomas', 'Elizabeth'
];

@Component({
  selector: 'localization-table',
  styleUrls: ['./localization-table.css'],
  templateUrl: 'localization-table.html',
})
export class LocalizationTable implements AfterViewInit {
  columnsToDisplay = [];
  columns = ['group', 'key'];
  dataSource: MatTableDataSource<LocalizationDataRow>;
  rows: LocalizationDataRow[];

  constructor(public dialog: MatDialog) {
    this.rows = Array.from({length: 100}, (_, k) => createLocalizationRow(k + 1));

    this.dataSource = new MatTableDataSource(this.rows);
    LOCALES.forEach(i => this.columns.push(i));
    this.columnsToDisplay = Array.from(this.columns);
    this.columnsToDisplay.push('actions')
  }

  ngAfterViewInit(): void {
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
      data: {locales: LOCALES, localizedString: localizedRow}
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
      data: {locales: LOCALES, localizedString: {}}
    });
    dialogRef.afterClosed().subscribe(result => {
      // todo process dialog closing
    });
  }
}

function createLocalizationRow(id: number): any {
  const name = NAMES[Math.round(Math.random() * (NAMES.length - 1))] + ' ' +
    NAMES[Math.round(Math.random() * (NAMES.length - 1))].charAt(0) + '.';

  let row = {
    group: id.toString(),
    key: name
  };

  for (const locale in LOCALES) {
    row[LOCALES[locale]] = LOCALES[Math.round(Math.random() * (LOCALES.length - 1))];
  }
  return row;
}
