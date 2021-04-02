import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import {RouterModule} from '@angular/router';
import {AppComponent} from './app.component';
import {NavMenuComponent} from './nav-menu/nav-menu.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {LocalizationTable} from './localization-table/localization-table';
import {A11yModule} from '@angular/cdk/a11y';
import {DragDropModule} from '@angular/cdk/drag-drop';
import {PortalModule} from '@angular/cdk/portal';
import {ScrollingModule} from '@angular/cdk/scrolling';
import {CdkStepperModule} from '@angular/cdk/stepper';
import {CdkTableModule} from '@angular/cdk/table';
import {CdkTreeModule} from '@angular/cdk/tree';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import {MatBadgeModule} from '@angular/material/badge';
import {MatBottomSheetModule} from '@angular/material/bottom-sheet';
import {MatButtonModule} from '@angular/material/button';
import {MatButtonToggleModule} from '@angular/material/button-toggle';
import {MatCardModule} from '@angular/material/card';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {MatChipsModule} from '@angular/material/chips';
import {MatStepperModule} from '@angular/material/stepper';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatDialogModule} from '@angular/material/dialog';
import {MatDividerModule} from '@angular/material/divider';
import {MatExpansionModule} from '@angular/material/expansion';
import {MatGridListModule} from '@angular/material/grid-list';
import {MatIconModule} from '@angular/material/icon';
import {MatInputModule} from '@angular/material/input';
import {MatListModule} from '@angular/material/list';
import {MatMenuModule} from '@angular/material/menu';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import {MatRadioModule} from '@angular/material/radio';
import {MatSelectModule} from '@angular/material/select';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatSliderModule} from '@angular/material/slider';
import {MatSlideToggleModule} from '@angular/material/slide-toggle';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import {MatTableModule} from '@angular/material/table';
import {MatTabsModule} from '@angular/material/tabs';
import {MatToolbarModule} from '@angular/material/toolbar';
import {MatTooltipModule} from '@angular/material/tooltip';
import {MatTreeModule} from '@angular/material/tree';
import {OverlayModule} from '@angular/cdk/overlay';
import {MAT_FORM_FIELD_DEFAULT_OPTIONS, MatFormFieldModule} from "@angular/material/form-field";
import {MatSortModule} from '@angular/material/sort';
import {MatPaginatorModule} from '@angular/material/paginator';
import {MatNativeDateModule, MatRippleModule} from "@angular/material/core";
import {LocalizationEditDialog} from "./localization-edit-dialog/localization-edit-dialog";
import {MonacoEditorModule} from 'ngx-monaco-editor';
import {SpinnerOverlayComponent} from "./base/spinner/component/spinner-overlay.component";
import {SpinnerHttpInterceptor} from "./base/spinner/spinner-interceptor";
import {SafeHtmlPipe} from "./localization-edit-dialog/safeHtml-pipe";
import {SnackbarService} from "./base/snackbar-service";
import {ConnectionViewComponent} from "./connection/components/view/connection-view.component";
import {ConnectionEditDialogComponent} from "./connection/components/dialogs/connection-edit-dialog.component";
import {ConnectionComponent} from "./connection/components/base/connection.component";
import { LocalizationDataService } from './localization-edit-dialog/localization-data.service';
import { ConnectionWrapperComponent } from './connection/components/wrapper/connection-wrapper.component';
import { SyncronizeComponent } from './syncronize/components/syncronize.component';
import { TreeComponent } from './base/tree/tree-component';
import { ConnectionDataService } from './connection/connection-data.service';
import { UsersListComponent } from './admin/usersList/users-list.component';
import { LoginComponent } from './login/login.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    LocalizationTable,
    LocalizationEditDialog,
    SpinnerOverlayComponent,
    SafeHtmlPipe,
    ConnectionViewComponent,
    ConnectionEditDialogComponent,
    ConnectionComponent,
    ConnectionWrapperComponent,
    SyncronizeComponent,
    TreeComponent,
    UsersListComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      {path: '', component: LocalizationTable, pathMatch: 'full'},
      { path: 'connections', component: ConnectionViewComponent, pathMatch: 'full' },
      { path: 'sync', component: SyncronizeComponent, pathMatch: 'full' }
    ]),
    BrowserAnimationsModule,
    A11yModule,
    CdkStepperModule,
    CdkTableModule,
    CdkTreeModule,
    DragDropModule,
    MatAutocompleteModule,
    MatBadgeModule,
    MatBottomSheetModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatCardModule,
    MatCheckboxModule,
    MatChipsModule,
    MatStepperModule,
    MatDatepickerModule,
    MatDialogModule,
    MatDividerModule,
    MatExpansionModule,
    MatGridListModule,
    MatIconModule,
    MatInputModule,
    MatListModule,
    MatMenuModule,
    MatNativeDateModule,
    MatPaginatorModule,
    MatProgressBarModule,
    MatProgressSpinnerModule,
    MatRadioModule,
    MatRippleModule,
    MatSelectModule,
    MatSidenavModule,
    MatSliderModule,
    MatSlideToggleModule,
    MatSnackBarModule,
    MatSortModule,
    MatTableModule,
    MatTabsModule,
    MatToolbarModule,
    MatTooltipModule,
    MatTreeModule,
    OverlayModule,
    PortalModule,
    ScrollingModule,
    MatFormFieldModule,
    MonacoEditorModule.forRoot(),
    MatTableModule,
    ReactiveFormsModule,
  ],
  providers: [
    {provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: {appearance: 'fill'}},
    SpinnerHttpInterceptor,
    { provide: HTTP_INTERCEPTORS, useClass: SpinnerHttpInterceptor, multi: true },
    SnackbarService,
    LocalizationDataService,
    ConnectionDataService
  ],
  bootstrap: [AppComponent],
  entryComponents: [LocalizationEditDialog, ConnectionEditDialogComponent],
})
export class AppModule {
}
