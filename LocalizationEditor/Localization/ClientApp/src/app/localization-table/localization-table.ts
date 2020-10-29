import {AfterViewInit, Component} from '@angular/core';
import {MatTableDataSource} from '@angular/material/table';

export interface UserData {
    id: string;
    name: string;
    progress: string;
    color: string;
}

/** Constants used to fill up our data base. */
const COLORS: string[] = [
    'maroon', 'red', 'orange', 'yellow', 'olive', 'green', 'purple', 'fuchsia', 'lime', 'teal',
    'aqua', 'blue', 'navy', 'black', 'gray'
];
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
    columnsToDisplay= [];
    columns= ['id', 'name', 'progress', 'color'];
    dataSource: MatTableDataSource<UserData>;
    users : UserData[];
    constructor() {
        // Create 100 users
         this.users = Array.from({length: 100}, (_, k) => createNewUser(k + 1));

        // Assign the data to the data source for the table to render
        this.dataSource = new MatTableDataSource(this.users);
         this.columnsToDisplay = Array.from(this.columns);
         this.columnsToDisplay.push('actions')
    }
    //
    ngAfterViewInit(): void {
    }

    applyFilter(event: Event) {
        const filterValue = (event.target as HTMLInputElement).value;
        this.dataSource.filter = filterValue.trim().toLowerCase();

        if (this.dataSource.paginator) {
            this.dataSource.paginator.firstPage();
        }
    }
}

/** Builds and returns a new User. */
function createNewUser(id: number): UserData {
    const name = NAMES[Math.round(Math.random() * (NAMES.length - 1))] + ' ' +
        NAMES[Math.round(Math.random() * (NAMES.length - 1))].charAt(0) + '.';

    return {
        id: id.toString(),
        name: name,
        progress: Math.round(Math.random() * 100).toString(),
        color: COLORS[Math.round(Math.random() * (COLORS.length - 1))]
    };
}
