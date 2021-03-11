import { ChangeDetectorRef, Component } from "@angular/core";
import { DiffEditorModel } from "ngx-monaco-editor";
import { BaseServerRoutes } from "../../base/base-server-routes";
import { HttpRequestService, TypedRequestImpl } from "../../base/http-request-service";
import { ConnectionHelper } from "../../connection/components/wrapper/connection-wrapper.component";
import { LocalizationDataRowServerDto } from "../../localization-table/models/localization-data-row-server-dto";
import { DiffDto } from "../models/diffDto";
import { GroupedKeyNode } from "../models/groupedKeyNode";
import { IDiffDto } from "../models/iDiffDto";


@Component({
  selector: "syncronize",
  templateUrl: "./syncronize.component.html",
  styleUrls: ['./syncronize.component.css']
})
export class SyncronizeComponent {
  private locales: string[];
  private readonly connectionHelper: ConnectionHelper = new ConnectionHelper();
  private diffModel: DiffDto;
  private shouldShowDiff: boolean = false;
  public editorOptions = { theme: 'vs-dark', language: 'html', automaticLayout: true, fontSize: "12px", renderSideBySide: false };
  private groupedKeys: GroupedKeyNode[] = new Array<GroupedKeyNode>();
  private originalModel: DiffEditorModel = { code: "", language: "html" };
  private modifiedModel: DiffEditorModel = { code: "", language: "html" };
  private originalFoundModel: LocalizationDataRowServerDto;
  private modifiedFoundModel: LocalizationDataRowServerDto;
  private selectedKeys: Set<number> = new Set<number>();

  constructor(
    private readonly _httpClient: HttpRequestService,
    private cdr: ChangeDetectorRef) {
  }

  isConnectionExist(): boolean {
    return localStorage.getItem(this.connectionHelper.SOURCE_KEY) !== null ||
      localStorage.getItem(this.connectionHelper.DESTINATION_KEY) !== null;
  }

  merge(): void {
    if (this.isConnectionExist() === false)
      return;

    const req = new TypedRequestImpl(`${BaseServerRoutes.Syncronize}/merge`,
      true,
      {
        "sourceId": localStorage.getItem(this.connectionHelper.SOURCE_KEY),
        "destinationId": localStorage.getItem(this.connectionHelper.DESTINATION_KEY),
        "localizationIds": this.selectedKeys
      },
      result => { });

    this._httpClient.post(req);
  }

  diff(): void {
    if (this.isConnectionExist() === false)
      return;

    const connections = {
      "sourceId": localStorage.getItem(this.connectionHelper.SOURCE_KEY),
      "destinationId": localStorage.getItem(this.connectionHelper.DESTINATION_KEY)
    }

    const req = new TypedRequestImpl(`${BaseServerRoutes.Syncronize}/diff?sourceId=${connections.sourceId}&destinationId=${connections.destinationId}`,
      true,
      null,
      this.setDiffModel.bind(this));

    this._httpClient.get<IDiffDto>(req);
  }

  setDiffModel(diff: IDiffDto): void {
    this.diffModel = diff;
    this.shouldShowDiff = true;

    const groupArr = this.diffModel.sources.reduce((r, { group }) => {
      if (!r.some(o => o.group == group)) {
        r.push({ group, groupItem: this.diffModel.sources.filter(v => v.group == group) });
      }
      return r;
    }, []);

    groupArr.forEach(item => {
      let group = new GroupedKeyNode(item.group, item.id, new Array<GroupedKeyNode>(), null);
      item.groupItem.forEach(child => {
        group.keys.push(new GroupedKeyNode(child.key, child.id, null, group));
      });
      this.groupedKeys.push(group);
    });

    this.locales = this.diffModel.sources[0].localizations.map(item => item.locale);
    this.cdr.detectChanges();
  }

  getLocalization($event) {
    this.originalFoundModel = this.diffModel.sources.find(elem => elem.id === $event.id);
    this.modifiedFoundModel = this.diffModel.destinations.find(elem => elem.id === $event.id);

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
    this.selectedKeys.add($event.id);
  }

  deselect($event) {
    this.selectedKeys.delete($event.id);
  }

  connectionSelected($event) {
    if (this.isKeyExistInLocalSotrage(this.connectionHelper.SOURCE_KEY) &&
      this.isKeyExistInLocalSotrage(this.connectionHelper.DESTINATION_KEY)) {
      this.diff();
    }
  }

  private isKeyExistInLocalSotrage(key: string): boolean {
    const value = localStorage.getItem(key);
    return value !== null && value !== undefined;
  }

  private selectSideBySide() {
    debugger;
    this.editorOptions = {
      ...this.editorOptions,
      renderSideBySide: !this.editorOptions.renderSideBySide
    };
  }
}
