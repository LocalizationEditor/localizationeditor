import { SelectionModel } from "@angular/cdk/collections";
import { ChangeDetectorRef, Component } from "@angular/core";
import { DiffEditorModel } from "ngx-monaco-editor";
import { log } from "util";
import { BaseServerRoutes } from "../../base/base-server-routes";
import { HttpRequestService, TypedRequestImpl } from "../../base/http-request-service";
import { ITreeNode } from "../../base/tree/tree-component";
import { ConnectionHelper } from "../../connection/components/wrapper/connection-wrapper.component";
import { LocalizationDataRowServerDto } from "../../localization-table/models/localization-data-row-server-dto";
import { DiffDto, IDiffDto } from "../models/diffDto";


@Component({
  selector: "syncronize",
  templateUrl: "./syncronize.component.html",
  styleUrls: ['./syncronize.component.css']
})
export class SyncronizeComponent {
  private locales: string[];
  private readonly connectionHelper: ConnectionHelper = new ConnectionHelper();
  private diffModel: DiffDto;
  private showDiff: boolean = false;
  public editorOptions = { theme: 'vs-dark', language: 'html', automaticLayout: true, fontSize: "12px" };
  private groupedKeys: GroupedKeyNode[] = new Array<GroupedKeyNode>();
  private originalModel: DiffEditorModel = { code: "", language: "html" };
  private modifiedModel: DiffEditorModel = { code: "", language: "html" };
  private originalFoundModel: LocalizationDataRowServerDto;
  private modifiedFoundModel: LocalizationDataRowServerDto;
  private selectedKeys: SelectionModel<ITreeNode>;

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
        "destinationId": localStorage.getItem(this.connectionHelper.DESTINATION_KEY)
      },
      result => { });

    this._httpClient.post(req);
  }

  diff(): void {
    debugger;
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
    this.showDiff = true;

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
    console.log(this.groupedKeys);
    this.cdr.detectChanges();
  }

  private hasChild = (_: number, node: GroupedKeyNode) => !!node.keys && node.keys.length > 0;

  getLocalization($event) {
    this.originalFoundModel = this.diffModel.sources.find(elem => elem.id === $event.id);
    this.modifiedFoundModel = this.diffModel.destinations.find(elem => elem.id === $event.id);
    this.modifiedModel = { code: this.originalFoundModel.localizations[0].value, language: "html" };
    this.originalModel = { code: this.modifiedFoundModel.localizations[0].value, language: "html" };
  }

  tabChanged($event) {
    const locale = $event.tab.textLabel;
    this.modifiedModel = { code: this.modifiedFoundModel.localizations.find(item => item.locale == locale).value, language: "html" };
    this.originalModel = { code: this.originalFoundModel.localizations.find(item => item.locale == locale).value, language: "html" };
  }
}

export class GroupedKeyNode implements ITreeNode {
  public parent?: GroupedKeyNode;
  public keys?: GroupedKeyNode[];
  public key: string;
  public id: number;

  constructor(key: string, id: number, keys?: GroupedKeyNode[], parent?: GroupedKeyNode) {
    this.keys = keys;
    this.key = key;
    this.id = id;
    this.parent = parent;
  }
}
