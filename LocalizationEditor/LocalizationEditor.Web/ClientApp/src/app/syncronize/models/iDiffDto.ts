import { LocalizationDataRowServerDto } from "../../localization-table/models/localization-data-row-server-dto";

export interface IDiffDto {
  sources: LocalizationDataRowServerDto[];
  destinations: LocalizationDataRowServerDto[];
}