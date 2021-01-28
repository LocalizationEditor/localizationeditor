import { LocalizationDataRowServerDto } from "../localization-table/models/localization-data-row-server-dto";
import { BaseServerRoutes } from "../base/base-server-routes";
import { HttpRequestService, TypedRequestImpl } from "../base/http-request-service";
import { LocalizationDataRowView } from "../localization-table/models/localization-data-row-view";
import { BehaviorSubject } from "rxjs";
import { LocalizationDataRowsServerDto } from "../localization-table/models/localization-data-rows-server-dto";
import { Injectable } from "@angular/core";

@Injectable()
export class LocalizationDataService {

  public localizationRows: BehaviorSubject<LocalizationDataRowView[]> = new BehaviorSubject([]);
  public totalCount: BehaviorSubject<number> = new BehaviorSubject(0);
  private _rows: LocalizationDataRowView[] = [];
  constructor(private _httpService: HttpRequestService) {
  }

  public initialize(): void {
    this.loadListMore(25, 0, "");
  }

  public save(localizationString: LocalizationDataRowServerDto): void {
    let request = new TypedRequestImpl(
      localizationString.id ?
        `${BaseServerRoutes.Localization}/${localizationString.id}` :
        `${BaseServerRoutes.Localization}`,
      true,
      localizationString,
      result => {
        let view = LocalizationDataService.mapRow(result);
        this._rows.push(view);
        this.localizationRows.next(this._rows);
      });
    if (localizationString.id)
      this._httpService.put(request);
    else
      this._httpService.post(request)
  }

  public loadListMore(limit: number, offset: number, search: string) {
    let enabledSearch = ``;
    if (search)
      enabledSearch = `&search=${search}`
    let request = new TypedRequestImpl(`${BaseServerRoutes.Localization}/?limit=${limit}&offset=${offset}${enabledSearch}`,
      false,
      null,
      result => {
        this._rows = this._rows.concat(result.localizationStrings.map(LocalizationDataService.mapRow));
        this.localizationRows.next(this._rows);
        this.totalCount.next(result.count);
      });
    this._httpService.get<LocalizationDataRowsServerDto>(request);
  }

  public deleteLocalizationKey(localizedRow: LocalizationDataRowView) {
    let request = new TypedRequestImpl(`${BaseServerRoutes.Localization}/${localizedRow.id}`,
      true,
      null,
      result => {
        const index = this._rows.indexOf(localizedRow);
        this._rows.splice(index, 1);
        this.localizationRows.next(this._rows);
      });
    this._httpService.delete(request);
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
}
