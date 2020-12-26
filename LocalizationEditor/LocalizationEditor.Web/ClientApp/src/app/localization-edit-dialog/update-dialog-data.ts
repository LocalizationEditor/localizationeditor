import {LocalizationDataRowView} from "../localization-table/models/localization-data-row-view";

export interface UpdateDialogData {
    locales: string[];
    localizedString: LocalizationDataRowView;
}
