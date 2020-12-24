import {LocalizationDataRowView} from "../localization-table/localization-data-row-view";

export interface UpdateDialogData {
    locales: string[];
    localizedString: LocalizationDataRowView;
}
