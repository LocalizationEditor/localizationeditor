import { ChangeDetectorRef, Component, ViewChild } from "@angular/core";
import { DiffEditorModel } from "ngx-monaco-editor";
import { BaseServerRoutes } from "../../base/base-server-routes";
import { HttpRequestService, TypedRequestImpl } from "../../base/http-request-service";
import { ConnectionHelper } from "../../connection/components/wrapper/connection-wrapper.component";
import { ConnectionDataService } from "../../connection/connection-data.service";
import { IConnection } from "../../connection/models/Connection/IConnection";
import { LocalizationDataRowServerDto } from "../../localization-table/models/localization-data-row-server-dto";
import { LocalizationDataRowsServerDto } from "../../localization-table/models/localization-data-rows-server-dto";
import { DiffDto } from "../models/diffDto";
import { GroupedKeyNode } from "../models/groupedKeyNode";
import { IDiffDto } from "../models/iDiffDto";


@Component({
  selector: "syncronize",
  templateUrl: "syncronize.component.html",
  styleUrls: ['./syncronize.component.css']
})
export class SyncronizeComponent {
  private locales: string[];
  private readonly connectionHelper: ConnectionHelper = new ConnectionHelper();
  private diffModel: DiffDto;
  public shouldShowDiff: boolean = false;
  public editorOptions = { theme: 'vs-dark', language: 'html', automaticLayout: true, fontSize: "12px", renderSideBySide: false };
  private groupedKeys =  new Array<GroupedKeyNode>();
  public originalModel: DiffEditorModel = { code: "", language: "html" };
  public modifiedModel: DiffEditorModel = { code: "", language: "html" };
  private originalFoundModel: LocalizationDataRowServerDto;
  private modifiedFoundModel: LocalizationDataRowServerDto;
  private selectedKeys: Array<number> = new Array<number>();

  selectedConnection: IConnection;
  public connections: IConnection[] = new Array<IConnection>();
  selectedConnectionName: string;
  selectedSourceConnectionName: string;
  selectedConnectionId: number;

  constructor(
    private readonly _httpClient: HttpRequestService,
    private cdr: ChangeDetectorRef,
    private _dataServce: ConnectionDataService) {
  }

  private getConnections() {
    this._dataServce.connections.subscribe(
      connections => {
        this.connections = connections;
        let connectionId = localStorage.getItem("connectionId");
        let diff = this.connections.find(i => i.id.toString() != connectionId);
        this.updateSelected(diff.id.toString());
        this.setSourceConectionName(connectionId);
      });
    this._dataServce.initialize();
  }

  private getConfig() {
    let request = new TypedRequestImpl(`${BaseServerRoutes.Localization}/config`,
      false,
      null,
      result => {
        this.locales = result.locales
      });
    this._httpClient.get<LocalizationDataRowsServerDto>(request);
  }
  ngOnInit(): void {
    this.getConnections();
    this.getConfig()
  }

  icon: boolean = false;

  click() {
    this.icon = !this.icon;
  }

  merge(): void {
    const req = new TypedRequestImpl(`${BaseServerRoutes.Syncronize}/merge`,
      true,
      {
        "destinationId": this.selectedConnectionId,
        "localizationIds": this.selectedKeys
      },
      result => { });

    this._httpClient.post(req);
  }

  diff(): void {
    const req = new TypedRequestImpl(`${BaseServerRoutes.Syncronize}/diff?destinationId=${this.selectedConnectionId}`,
      true,
      null,
      this.setDiffModel.bind(this));

    this._httpClient.get<IDiffDto>(req);
  }

  setDiffModel(diff: IDiffDto): void {
    this.diffModel = diff;
    this.shouldShowDiff = this.diffModel.sources.length > 0;

    const groupArr = this.groupBy(this.diffModel.sources, i => i.group);

    this.groupedKeys = new Array<GroupedKeyNode>();
    if (groupArr) {

      for (let [key, value] of groupArr) {
        let group = new GroupedKeyNode(key, key + value.length, new Array<GroupedKeyNode>(), null);
        value.forEach(child => {
          group.keys.push(new GroupedKeyNode(child.key, child.id, null, group));
        });
        this.groupedKeys.push(group);
      }
      this.cdr.detectChanges();
    }
  }

  private groupBy(list, keyGetter) {
  const map = new Map();
  list.forEach((item) => {
    const key = keyGetter(item);
    const collection = map.get(key);
    if (!collection) {
      map.set(key, [item]);
    } else {
      collection.push(item);
    }
  });
  return map;
}

  getLocalization($event) {
    this.originalFoundModel = this.diffModel.destinations.find(elem => elem.id === $event.id);
    this.modifiedFoundModel = this.diffModel.sources.find(elem => elem.id === $event.id);

    if (this.originalFoundModel !== undefined)
      this.originalModel = { code: this.originalFoundModel.localizations[0].value, language: "html" };
    else this.originalModel.code = "";

    if (this.modifiedFoundModel !== undefined)
      this.modifiedModel = { code: this.modifiedFoundModel.localizations[0].value, language: "html" };
    else this.originalModel.code = "";
  }

  tabChanged($event) {
    const locale = $event.tab.textLabel;
    this.modifiedModel = { code: this.modifiedFoundModel.localizations.find(item => item.locale == locale).value, language: "html" };
    this.originalModel = { code: this.originalFoundModel.localizations.find(item => item.locale == locale).value, language: "html" };
  }

  select($event) {
    this.selectedKeys.push($event.id);
  }

  deselect($event) {
    const index = this.selectedKeys.findIndex(i=> i === $event.id);
    this.selectedKeys.slice(index, 1);
  }

  connectionSelected($event) {
    if (this.isKeyExistInLocalSotrage(this.connectionHelper.DESTINATION_KEY)) {
      this.shouldShowDiff = false;
      this.groupedKeys = null;
      this.diff();
    }
  }

  private isKeyExistInLocalSotrage(key: string): boolean {
    const value = localStorage.getItem(key);
    return value !== null && value !== undefined;
  }

  private selectSideBySide() {
    this.editorOptions = {
      ...this.editorOptions,
      renderSideBySide: !this.editorOptions.renderSideBySide
    };
  }

  private updateSelected(connectionId: string) {
    this.selectedConnection = this.connections.find(i => i.id.toString() == connectionId);
    if (this.selectedConnection) {
      this.shouldShowDiff = false;
      this.groupedKeys = null;
      this.selectedConnectionId = this.selectedConnection.id;
      this.selectedConnectionName = this.selectedConnection.connectionName;
      this.diff();
    }
  }

  private setSourceConectionName(connectionId: string) {
    let selectedConnection = this.connections.find(i => i.id.toString() == connectionId);
    if (selectedConnection) {
      this.selectedSourceConnectionName = selectedConnection.connectionName;
    }
  }

  public onchangedValue(value) {
    this.updateSelected(value.value);
  }
}
