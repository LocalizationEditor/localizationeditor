import { LocalizationDataRowServerDto } from "../../localization-table/models/localization-data-row-server-dto";
import { IDiffDto } from "./iDiffDto";

export class DiffDto implements IDiffDto {
  sources: LocalizationDataRowServerDto[];
  destinations: LocalizationDataRowServerDto[];

  constructor(sources: LocalizationDataRowServerDto[], destinations: LocalizationDataRowServerDto[]) {
    this.sources = sources;
    this.destinations = destinations;
  }
}
