import {LocalizationDataRow} from "../localization-table/localization-data-row";

export interface UpdateDialogData {
    locales: string[];
    localizedString: LocalizationDataRow;
}
